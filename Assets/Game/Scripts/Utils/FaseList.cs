using System.Collections.Generic;
using UnityEngine;
using Utils.Singleton;

[CreateAssetMenu(fileName = "FaseList", menuName = "Scriptable Objects/Criar lista/Criar fase lista")]
public class FaseList : ScriptableSingleton<FaseList>
{
    [SerializeField] public FaseDetails[] fases;

    public FaseDetails GetFaseTerra()
    {
        foreach (FaseDetails f in fases)
        {
            if (f.faseNome == NomeFase.Terra)
            {
                return f;
            }
        }
        return null;
    }

    public List<FaseDetails> GetFasesSemTerra()
    {
        List<FaseDetails> fasesSemTerra = new List<FaseDetails>();

        foreach (FaseDetails f in fases)
        {
            if (f.faseNome != NomeFase.Terra)
            {
                fasesSemTerra.Add(f);
            }
        }

        return fasesSemTerra;
    }
}
