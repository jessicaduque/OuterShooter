using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singleton;

public class SpawnManager : Singleton<SpawnManager>
{
    private List<Pool> InimigosPossiveisList;

    private int quantInimigosFase;
    private int quantInimigosWave;
    private int quantInimgiosMortos;
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
        quantInimigosWave = 0;
        quantInimigosEscondidos = 0;
        quantInimgiosMortos = 0;
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
            case NomeFase.Lightning:
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

    public void AumentarInimigosMortos()
    {
        quantInimgiosMortos++;
    }

    #endregion

    #region Fase de Gelo

    private void SpawnGeloInicial()
    {
        switch (numeroFase)
        {
            case < 3:
                ResetarValoresFase(numeroFase * 2);
                StartCoroutine(SpawnGeloBunny1());
                break;
            case < 5:
                ResetarValoresFase((int) (numeroFase / 2.5f)  * 3);
                StartCoroutine(SpawnGeloRobo2());
                break;
            case < 7:
                ResetarValoresFase((numeroFase == 6 ? 6 : 8));
                StartCoroutine(SpawnGeloBunny1());
                break;
        }
    }
    private IEnumerator SpawnGeloRobo1()
    {
        if(quantInimgiosMortos < quantInimigosFase)
        {
            GameObject enemyRobot1 = _poolManager.GetObject(InimigosPossiveisList[1].tagPool, Right[0].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("InPlaceTop", enemyRobot1.transform);

            GameObject enemyRobot2 = _poolManager.GetObject(InimigosPossiveisList[1].tagPool, Right[4].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("InPlaceBottom", enemyRobot2.transform);

            quantInimigosWave = 2;

            yield return new WaitForSeconds(1.2f);


            while (quantInimigosWave > quantInimgiosMortos + quantInimigosEscondidos)
            {
                yield return null;
            }

            quantInimigosWave = 0;
            quantInimigosEscondidos = 0;
            StartCoroutine(SpawnGeloRobo1());
        }
        else
        {
            _levelController.SetEstadoJogo(EstadoJogo.EscolherPoder);
        }
        


        quantInimigosFase -= 3;

        if (quantInimigosFase > 0)
        {
            StartCoroutine(SpawnGeloRobo1());
        }
        else
        {
            
        }
    }
    private IEnumerator SpawnGeloRobo2()
    {
        if (quantInimgiosMortos < quantInimigosFase)
        {
            GameObject enemyRobot1 = _poolManager.GetObject(InimigosPossiveisList[1].tagPool, Right[0].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("InPlaceTop", enemyRobot1.transform);

            GameObject enemyRobot2 = _poolManager.GetObject(InimigosPossiveisList[1].tagPool, Right[4].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("InPlaceBottom", enemyRobot2.transform);

            GameObject enemyRobot3 = _poolManager.GetObject(InimigosPossiveisList[1].tagPool, Right[2].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("InPlaceMiddle", enemyRobot3.transform);

            quantInimigosWave = 3;

            while (quantInimigosWave > quantInimgiosMortos + quantInimigosEscondidos)
            {
                yield return null;
            }

            quantInimigosWave = 0;
            quantInimigosEscondidos = 0;
            StartCoroutine(SpawnGeloRobo2());
        }
        else
        {
            _levelController.SetEstadoJogo(EstadoJogo.EscolherPoder);
        }
    }

    private IEnumerator SpawnGeloBunny1()
    {
        if (quantInimgiosMortos < quantInimigosFase)
        {
            quantInimigosWave = 0;

            while (quantInimigosWave < quantInimigosFase / 2)
            {
                GameObject bunny = _poolManager.GetObject(InimigosPossiveisList[0].tagPool, Top[2].position, Quaternion.identity);
                _enemyMovement.FollowMovementPattern("TopToBottom", bunny.transform);

                quantInimigosWave += 1;

                yield return new WaitForSeconds(0.4f);
            }

            quantInimigosWave = 0;

            while (quantInimigosWave < quantInimigosFase / 2)
            {
                GameObject bunny = _poolManager.GetObject(InimigosPossiveisList[0].tagPool, Top[2].position, Quaternion.identity);
                _enemyMovement.FollowMovementPattern("BottomToTop", bunny.transform);

                quantInimigosWave += 1;

                yield return new WaitForSeconds(0.4f);
            }

            while (quantInimigosWave > quantInimgiosMortos + quantInimigosEscondidos)
            {
                yield return null;
            }

            quantInimigosWave = 0;
            quantInimigosEscondidos = 0;
            StartCoroutine(SpawnGeloBunny1());
        }
        else
        {
            _levelController.SetEstadoJogo(EstadoJogo.EscolherPoder);
        }

    }


    #endregion
}
