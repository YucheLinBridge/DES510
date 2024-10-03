using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "Grid3D_data", menuName = "Installers/Grid3D_data")]
public class Grid3D_data : ScriptableObjectInstaller<Grid3D_data>
{
    [SerializeField] private float modelWidth,modelHeight,spacing;

    public float WIDTH=>modelWidth;
    public float HEIGHT=>modelHeight;
    public float SPACING=>spacing;



    public override void InstallBindings()
    {
        Container.BindInstance(this);
    }
}