using UnityEngine;

[CreateAssetMenu (fileName ="New Season", menuName ="Scriptable Objects/Season")]
public class MenuLevel : ScriptableObject
{
    public int seasonIndex;
    public string seasonName;
    public string seasonNameEn;
    public string seasonDescriptionEn;
    public string seasonDescription;
    public Sprite seasonImage;
    public GameObject menuLevels;
}
