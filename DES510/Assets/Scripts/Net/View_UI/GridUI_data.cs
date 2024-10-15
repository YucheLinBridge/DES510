using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GridUI_data", menuName = "Installers/GridUI_data")]
public class GridUI_data : ScriptableObjectInstaller<GridUI_data>
{
    [SerializeField] private float spriteWidth, spriteHeight, spacing;

    public float WIDTH => spriteWidth;
    public float HEIGHT => spriteHeight;
    public float SPACING => spacing;

    public override void InstallBindings()
    {
        Container.BindInstance(this);
    }
}