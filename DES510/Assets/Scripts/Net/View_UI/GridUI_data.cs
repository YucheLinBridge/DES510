using UnityEngine;
using Zenject;
using Net;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GridUI_data", menuName = "Installers/GridUI_data")]
public class GridUI_data : ScriptableObjectInstaller<GridUI_data>
{

    public GameObject Prefab;
    [SerializeField] private float spriteWidth, spriteHeight, spacing,rotatingTime;
    [SerializeField] private List<ArtData> LineSprites;
    [SerializeField] private Sprite Node, Main;
    public Color line_active, line_inactive, node_active, node_inactive;

    public Sprite NODE => Node;
    public Sprite MAIN => Main;

    public float ROTTIME=>rotatingTime;

    public Sprite GetLine(string name) {
        int index = LineSprites.FindIndex(x=>x.IsName(name));
        if (index==-1)
        {
            Debug.LogError($"There is no sprite called {name}");
            return null;
        }
        return LineSprites[index].SPRITE;
    }

    public Sprite GetLineBG(string name)
    {
        int index = LineSprites.FindIndex(x => x.IsName(name));
        if (index == -1)
        {
            Debug.LogError($"There is no sprite called {name}");
            return null;
        }
        return LineSprites[index].BG;

    }

    public float WIDTH => spriteWidth;
    public float HEIGHT => spriteHeight;
    public float SPACING => spacing;

    public override void InstallBindings()
    {
        Container.BindInstance(this);
    }
}
