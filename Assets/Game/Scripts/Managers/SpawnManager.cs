using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singleton;

public class SpawnManager : Singleton<SpawnManager>
{
    private List<Pool> InimigosPossiveisList;

    private int quantInimigosFase;
    private int quantInimigosWave;
    private int quantInimigosVivos;
    private int quantInimigosEscondidos;

    private NomeFase nomeFase;
    private int numeroFase;

    [Header("Start Points")]
    [SerializeField] private Transform[] Bottom;
    [SerializeField] private Transform[] Top;
    [SerializeField] private Transform[] Right;

    private PoolManager _poolManager => PoolManager.I;
    private EnemyMovement _enemyMovement => EnemyMovement.I;
    private LevelController _levelController => LevelController.I;


    private new void Awake()
    {

    }

    private void ResetarValoresFase(int quantIni)
    {
        quantInimigosFase = quantIni;
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
                SpawnGeloInicial();
                break;
            case NomeFase.Rosa:
                // Código
                break;
        }
    }

    #region Public

    public void ComecarNovaFase(List<Pool> inimigos, NomeFase nome, int numeroFase)
    {
        this.numeroFase = numeroFase;
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

    private void SpawnGeloInicial()
    {
        switch (numeroFase)
        {
            case < 3:
                ResetarValoresFase(numeroFase * 3);
                StartCoroutine(SpawnGeloRobo1());
                break;
            case < 5:
                ResetarValoresFase(10);
                StartCoroutine(SpawnGeloRobo2());
                break;
        }
    }
    private IEnumerator SpawnGeloRobo1()
    {
        quantInimigosWave = 3;
        quantInimigosVivos = 3;

        GameObject enemyRobot1 = _poolManager.GetObject(InimigosPossiveisList[1].tagPool, Right[0].position, Quaternion.identity);
        _enemyMovement.FollowMovementPattern("InPlaceTop", enemyRobot1.transform);

        GameObject enemyRobot2 = _poolManager.GetObject(InimigosPossiveisList[1].tagPool, Right[4].position, Quaternion.identity);
        _enemyMovement.FollowMovementPattern("InPlaceBottom", enemyRobot2.transform);

        GameObject enemyRobot3 = _poolManager.GetObject(InimigosPossiveisList[1].tagPool, Right[2].position, Quaternion.identity);
        _enemyMovement.FollowMovementPattern("InPlaceMiddle", enemyRobot3.transform);

        while(quantInimigosVivos > 0)
        {
            yield return null;
        }

        quantInimigosFase -= 3;

        if (quantInimigosFase > 0)
        {
            StartCoroutine(SpawnGeloRobo1());
        }
        else
        {
            _levelController.SetEstadoJogo(EstadoJogo.EscolherPoder);
        }
    }


    private IEnumerator SpawnGeloRobo2()
    {
        quantInimigosWave = 5;
        quantInimigosVivos = 5;

        GameObject enemyRobot1 = _poolManager.GetObject(InimigosPossiveisList[1].tagPool, Right[0].position, Quaternion.identity);
        _enemyMovement.FollowMovementPattern("InPlaceTop", enemyRobot1.transform);

        GameObject enemyRobot2 = _poolManager.GetObject(InimigosPossiveisList[1].tagPool, Right[4].position, Quaternion.identity);
        _enemyMovement.FollowMovementPattern("InPlaceBottom", enemyRobot2.transform);

        GameObject enemyRobot3 = _poolManager.GetObject(InimigosPossiveisList[1].tagPool, Right[2].position, Quaternion.identity);
        _enemyMovement.FollowMovementPattern("InPlaceMiddle", enemyRobot3.transform);

        GameObject enemyRobot4 = _poolManager.GetObject(InimigosPossiveisList[1].tagPool, Right[1].position, Quaternion.identity);
        _enemyMovement.FollowMovementPattern("InPlaceTopMiddle", enemyRobot4.transform);

        GameObject enemyRobot5 = _poolManager.GetObject(InimigosPossiveisList[1].tagPool, Right[3].position, Quaternion.identity);
        _enemyMovement.FollowMovementPattern("InPlaceBottomMiddle", enemyRobot5.transform);

        while (quantInimigosVivos > 0)
        {
            yield return null;
        }

        quantInimigosFase -= 5;

        if (quantInimigosFase > 0)
        {
            StartCoroutine(SpawnGeloRobo2());
        }
        else
        {
            _levelController.SetEstadoJogo(EstadoJogo.EscolherPoder);
        }
    }

    //private IEnumerator SpawnGeloBunny()
    //{

    //}


    #endregion
}
