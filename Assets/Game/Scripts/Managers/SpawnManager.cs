using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singleton;

public class SpawnManager : Singleton<SpawnManager>
{
    private List<GameObject> InimigosPossiveisList;

    private int quantInimigosFase;
    private int quantInimigosWave;
    private int quantInimigosVivos;
    private int quantInimigosEscondidos;
    private NomeFase nomeFase;
    private int numeroFase;

    private PoolManager _poolManager => PoolManager.I;
    private EnemyMovement _enemyMovement => EnemyMovement.I;
    private void ResetarValoresFase(int quantIni)
    {
        quantInimigosFase = quantIni;
        quantInimigosVivos = quantInimigosFase;
        quantInimigosEscondidos = 0;
    }

    private void DeterminarFase(NomeFase nome)
    {
        nomeFase = nome;

        switch (nomeFase)
        {
            case NomeFase.Fogo:
                // Código
                break;
            case NomeFase.Gelo:
                SpawnGelo();
                break;
            case NomeFase.Rosa:
                // Código
                break;
        }
    }

    #region Public

    public void ComecarNovaFase(List<GameObject> inimigos, NomeFase nome, int numeroFase)
    {
        this.numeroFase = numeroFase;
        ResetarValoresFase(inimigos.Count);
        InimigosPossiveisList = inimigos;
        DeterminarFase(nome);
    }

    public void AumentarInimigosEscondidos()
    {
        quantInimigosEscondidos++;
    }

    public void DiminuirInimigosVivos()
    {
        quantInimigosVivos--;
        if (quantInimigosVivos <= 0)
        {
            Debug.Log("Acabou fase!");
        }
    }

    #endregion

    #region Fase de Gelo

    private void SpawnGelo()
    {
    }

    #endregion
}
