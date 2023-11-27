using UnityEngine;

[CreateAssetMenu(fileName = "Fase", menuName = "Scriptable Objects/Criar detalhe/Criar fase")]
public class FaseDetails : ScriptableObject
{
    public int faseID;
    public NomeFase faseNome;
    public RuntimeAnimatorController faseAnimControl;
    public PoderDetails fasePoder;
    public Pool[] faseInimiPossiveis;
    public Sprite fasePlanetaSprite;
}