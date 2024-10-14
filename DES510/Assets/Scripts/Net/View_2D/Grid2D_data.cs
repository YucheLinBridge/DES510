using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "Grid2D_data", menuName = "Installers/Grid2D_data")]
public class Grid2D_data : ScriptableObjectInstaller<Grid2D_data>
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