using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName ="CharacterData",menuName ="Custom/Create a Characters Data")]
public class CharactersData : ScriptableObject
{
    [SerializeField]private List<Character> characters = new List<Character>();
    [SerializeField] private List<ImageData> images = new List<ImageData>();

    public Character GetCharacter(string name)
    {
        return characters.Find(c => c.Name == name);
    }

    public Sprite GetImage(string name)
    {
        return images.Find(c => c.Name == name).Sprite;
    }

}

[System.Serializable]
public class Character {
    public string Name;
    public string Name_Shown;
    public GameObject SpeakerPrefab;
    public TMP_FontAsset FontAsset;
}

[System.Serializable]
public class ImageData {
    public string Name;
    public Sprite Sprite;
}
