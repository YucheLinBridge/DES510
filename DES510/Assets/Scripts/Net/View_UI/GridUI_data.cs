using UnityEngine;
using Zenject;
using Net;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GridUI_data", menuName = "Installers/GridUI_data")]
public class GridUI_data : ScriptableObjectInstaller<GridUI_data>
{

    public GameObject Prefab;
    [SerializeField] private float spriteWidth, spriteHeight, spacing;
    [SerializeField] private List<ArtData> LineSprites;
    [SerializeField] private Sprite Node, Main;

    public Sprite NODE => Node;
    public Sprite MAIN => Main;

    public Sprite GetLine(string name) {
        int index = LineSprites.FindIndex(x=>x.IsName(name));
        if (index==-1)
        {
            Debug.LogError($"There is no sprite called {name}");
            return null;
        }
        return LineSprites[index].SPRITE;
    }



    public float WIDTH => spriteWidth;
    public float HEIGHT => spriteHeight;
    public float SPACING => spacing;

    public override void InstallBindings()
    {
        Container.BindInstance(this);
    }
}
