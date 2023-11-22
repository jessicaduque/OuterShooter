using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Poder", menuName = "Scriptable Objects/Criar detalhe/Criar poder")]
public class PoderDetails : ScriptableObject
{
    public int poderID;
    public NomePoder poderNome;
    public RuntimeAnimatorController poderPlayerAnimControl;
    public PoderDetails poderAumentoEfetividade;
    public Sprite poderSpriteEscolha;
}