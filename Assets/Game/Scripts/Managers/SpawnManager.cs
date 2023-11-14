using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singleton;

public class SpawnManager : Singleton<SpawnManager>
{
    private PoolManager _poolManager => PoolManager.I;
    private FaseDetails faseAtual;
    private int quantInimigosFase;
    private int quantInimigosVivos;

    private void ResetarValoresFase()
    {
        quantInimigosFase = faseAtual.faseInimiPossiveis.Length;
        quantInimigosVivos = quantInimigosFase;
    }

    #region Public

    public void SetFase(FaseDetails fase)
    {
        faseAtual = fase;
    }

    public void DiminuirInimigosVivos()
    {
        quantInimigosVivos--;
    }

    #endregion
}
