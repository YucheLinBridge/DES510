using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueMgr:MonoBehaviour
{
    [SerializeField] private Transform dialogueParent,dialogueHUD,optionsParent,imagesParent;
    [SerializeField] private Transform optionsMask;
    [SerializeField] private GameObject dialogueObj,dialogueOption,dialogueImage;
    [SerializeField] private CharactersData charactersData;
    [SerializeField] private OptionStyle optionStyle;


    public UnityEvent OnDialogueStart=new UnityEvent(), OnDialogueEnd=new UnityEvent();

    private DialogueObj dialogueObj_ins;
    private List<DialogueOption> dialogueOptions = new List<DialogueOption>();
    private DialogueImage dialogueImage_ins;

    private Story story;

    private const string SPEAKER_TAG = "speaker";
    private const string PORTRAIT_TAG = "portrait";
    private const string LAYOUT_TAG = "layout";
    private const string EVENT_TAG = "event";
    private const string SPEED_TAG = "speed";
    private const string IMG_TAG = "img";
    private const string IMGOFF_TAG = "imgoff";

    private bool finished = false;
    private bool auto=false;

    private UnityEvent onLineEnd=new UnityEvent();

    private GameObject dialogueObj_custom = null;

    public static DialogueMgr Instance;
    private void Awake()
    {
        Instance = this;
        showDialogueUI(false);
    }

    private void showDialogueUI(bool flag)
    {
        dialogueParent.gameObject.SetActive(flag);
        //dialogueHUD.gameObject.SetActive(flag);
    }

    public void StartStory(TextAsset inkJsonAsset)
    {
        showDialogueUI(true);
        OnDialogueStart?.Invoke();
        story = new Story(inkJsonAsset.text);
        OnDialogueStart?.Invoke();
        finished=false;
        refreshView();
    }


    public void StartStory(GameObject customDialogue,TextAsset inkJsonAsset)
    {
        dialogueObj_custom = customDialogue;
        showDialogueUI(true);
        OnDialogueStart?.Invoke();
        story = new Story(inkJsonAsset.text);
        OnDialogueStart?.Invoke();
        finished = false;
        refreshView();
    }

    public void ContinueDialogue()
    {
        onLineEnd?.Invoke();
        refreshView();
    }


    private void refreshView()
    {
        // Remove all the UI on screen
        removeDialogues();

        DialogueObj dialogue=null;
        // Read all the content until we can't continue any more
        if (story.canContinue)
        {
            // Continue gets the next line of the story
            string text = story.Continue();
            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen!
            dialogue=createContentView(text);
            if (story.canContinue)
            {
                return;
            }
        }


        // Display all the choices, if there are any!
        if (dialogue &&story.currentChoices.Count > 0)
        {

            StartCoroutine(waitForDialogueEnd(dialogue));
        }
        // If we've read all the content and there's no choices, the story is finished!
        else if(!finished)
        {
            finished = true;
        }
        else
        {
            storyEnd();
        }
    }

    private void storyEnd()
    {
        OnDialogueEnd?.Invoke();
        OnDialogueEnd?.RemoveAllListeners();
        Debug.Log("Story End");
        showDialogueUI(false);
        removeImg();
        dialogueObj_custom = null;
        auto = false;
    }

    IEnumerator waitForDialogueEnd(DialogueObj target)
    {
        
        while (!target.END)
        {
            yield return null;
        }
        createOptions();
    }

    private void createOptions()
    {
        optionsParent.gameObject.SetActive(true);
        optionsMask.gameObject.SetActive(true);
        for (int i = 0; i < story.currentChoices.Count; i++)
        {
            Choice choice = story.currentChoices[i];

            switch (optionStyle)
            {
                case OptionStyle.None:
                    createOptionView(choice.text.Trim(), () => {
                        onClickChoiceButton(choice);
                    });
                    break;
                case OptionStyle.Number:
                    createOptionView($"{i + 1}.{choice.text.Trim()}", () => {
                        onClickChoiceButton(choice);
                    });

                    break;
                case OptionStyle.Arrow:
                    createOptionView($"-> {choice.text.Trim()}", () => {
                        onClickChoiceButton(choice);
                    });
                    break;
                default:
                    break;
            }
        }

        createOptionView(null, () => { });
    }

    private void handleTags(List<string> currentTags,DialogueObj obj)
    {
        bool hasSpeaker=false;
        int portrait = 0;
        int layout = -1;

        foreach (string tag in currentTags) {
            string[] splitTag = tag.Split(':');
            string tagKey=null;
            string tagValue=null;
            if (splitTag.Length==1) {
                tagKey = splitTag[0].Trim();
            }
            else if (splitTag.Length==2)
            {
                tagKey = splitTag[0].Trim();
                tagValue = splitTag[1].Trim();
            }else if (splitTag.Length!=2)
            {
                Debug.LogError($"Tag could not be appropriately parsed: {tag}");
                break;
            }
            

            switch (tagKey)
            {
                case SPEAKER_TAG:
                    obj.SetSpeaker(charactersData.GetCharacter(tagValue));
                    hasSpeaker = true;
                    break;
                case PORTRAIT_TAG:
                    if (!int.TryParse(tagValue, out portrait))
                    {
                        portrait =0;
                    }
                    break;
                case LAYOUT_TAG:
                    if (!int.TryParse(tagValue, out layout))
                    {
                        layout = -1;
                    }
                    break;
                case EVENT_TAG:
                    string[] splitValue=tagValue.Split(",");
                    if (splitValue.Length==1)
                    {
                        onLineEnd?.AddListener(() => {
                            DialogueEventsMgr.Instance?.SendMessage(splitValue[0]);
                        });
                    }else if (splitValue.Length==2)
                    {
                        onLineEnd?.AddListener(() => {
                            DialogueEventsMgr.Instance?.SendMessage(splitValue[0], int.Parse(splitValue[1]));
                        });
                    }else if (splitValue.Length == 3)
                    {
                        onLineEnd?.AddListener(() => {
                            DialogueEventsMgr.Instance?.SendMessage(splitValue[0], (int.Parse(splitValue[1]),int.Parse(splitValue[2])));
                        });
                    }
                    break;
                case SPEED_TAG:
                    if (float.TryParse(tagValue,out float speed))
                    {
                        obj.SetSpeed(speed);
                    }
                    
                    break;
                case IMG_TAG:
                    dialogueImage_ins = createImage(tagValue);
                        break;
                case IMGOFF_TAG:
                    removeImg();
                    break;
                default:
                    Debug.LogError($"Tag came in but is not currently being handled: {tag}");
                    break;
            }
        }

        if (hasSpeaker)
        {
            obj.SetPortrait(portrait);
            obj.SetSpeakerLayout(layout);

        }

    }


    private void handleTags_option(List<string> currentTags, DialogueOption obj)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2)
            {
                Debug.LogError($"Tag could not be appropriately parsed: {tag}");
                break;
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case EVENT_TAG: 
                    break;
                default:
                    //Debug.LogError($"Tag came in but is not currently being handled: {tag}");
                    break;
            }
        }
    }

    private void removeDialogues()
    {
        for (int i= dialogueOptions.Count-1;i>=0;i--)
        {
            dialogueOptions[i].DestroyDialogue();
            dialogueOptions.RemoveAt(i);
        }

        dialogueObj_ins?.DestroyDialogue();
        dialogueObj_ins = null;


        optionsParent.gameObject.SetActive(false);
        optionsMask.gameObject.SetActive(false);
    }

    private void removeImg()
    {
        if (dialogueImage_ins != null)
        {
            dialogueImage_ins.DestroyImage();
            dialogueImage_ins = null;
        }
    }

    private void onClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        refreshView();
    }

    private DialogueOption createOptionView(string text,UnityAction clickevent)
    {
        var go = Instantiate(dialogueOption, optionsParent);
        var obj = go.GetComponent<DialogueOption>();
        obj.Show(text, false);
        if (text != null)
        {
            obj.SetClicked(clickevent);
            dialogueOptions.Add(obj);
            handleTags_option(story.currentTags, obj);
            Canvas.ForceUpdateCanvases();
            return obj;
        }
        else {
            Canvas.ForceUpdateCanvases();
            return null;
        }
    }
     
    private DialogueObj createContentView(string text)
    {
        onLineEnd.RemoveAllListeners();
        var go = dialogueObj_custom? Instantiate(dialogueObj_custom, dialogueParent) : Instantiate(dialogueObj,dialogueParent);
        var obj=go.GetComponent<DialogueObj>();
        obj.Show(text,auto);
        handleTags(story.currentTags,obj);
        dialogueObj_ins=obj;
        return obj;
    }

    private DialogueImage createImage(string imgname)
    {
        var go= Instantiate(dialogueImage,dialogueParent);
        var img=go.GetComponent<DialogueImage>();
        img.Set(charactersData.GetImage(imgname));
        return img;
    }

    public void AutoSwitcher()
    {
        auto = !auto;
        dialogueObj_ins.SetAuto(auto);
    }

    public void SetAuto()
    {
        auto =true;
        dialogueObj_ins.SetAuto(true);
    }


    public enum OptionStyle {
        None,
        Number,
        Arrow
    }
}
