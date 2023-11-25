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
                SpawnFogoInicial();
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
                ResetarValoresFase(numeroFase == 1 ? 2 : 4);
                StartCoroutine(SpawnGeloRobo1());
                break;
            case < 6:
                ResetarValoresFase(numeroFase < 5 ? 3 : 6);
                StartCoroutine(SpawnGeloRobo2());
                break;
            case < 8:
                ResetarValoresFase(numeroFase == 6 ? 3 : 6);
                StartCoroutine(SpawnGeloBunny1());
                break;
            case >= 8:
                //case < 11:
                ResetarValoresFase(numeroFase < 10 ? 4 : 8);
                StartCoroutine(SpawnGeloRobo3());
                break;
            //case > 10:
            //    ResetarValoresFase(numeroFase < 13 ? 6 : 12);
            //    StartCoroutine(SpawnGeloBunny2());
            //    break;
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

            quantInimigosWave += 2;

            while (quantInimigosWave > quantInimgiosMortos + quantInimigosEscondidos)
            {
                yield return null;
            }

            StartCoroutine(SpawnGeloRobo1());
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            _levelController.SetEstadoJogo(EstadoJogo.EscolherPoder);
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

            quantInimigosWave += 3;

            while (quantInimigosWave > quantInimgiosMortos + quantInimigosEscondidos)
            {
                yield return null;
            }

            StartCoroutine(SpawnGeloRobo2());
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            _levelController.SetEstadoJogo(EstadoJogo.EscolherPoder);
        }
    }
    private IEnumerator SpawnGeloRobo3()
    {
        if (quantInimgiosMortos < quantInimigosFase)
        {
            GameObject enemyRobot1 = _poolManager.GetObject(InimigosPossiveisList[1].tagPool, Right[0].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("InPlaceBackOne", enemyRobot1.transform);

            GameObject enemyRobot2 = _poolManager.GetObject(InimigosPossiveisList[1].tagPool, Right[1].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("InPlaceBackTwo", enemyRobot2.transform);

            GameObject enemyRobot3 = _poolManager.GetObject(InimigosPossiveisList[1].tagPool, Right[3].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("InPlaceBackThree", enemyRobot3.transform);

            GameObject enemyRobot4 = _poolManager.GetObject(InimigosPossiveisList[1].tagPool, Right[4].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("InPlaceBackFour", enemyRobot4.transform);

            quantInimigosWave += 4;

            while (quantInimigosWave > quantInimgiosMortos + quantInimigosEscondidos)
            {
                yield return null;
            }

            StartCoroutine(SpawnGeloRobo3());
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            _levelController.SetEstadoJogo(EstadoJogo.EscolherPoder);
        }
    }
    private IEnumerator SpawnGeloBunny1()
    {
        if (quantInimgiosMortos < quantInimigosFase)
        {
            GameObject bunny1 = _poolManager.GetObject(InimigosPossiveisList[0].tagPool, Bottom[0].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("BackAndFourthTop", bunny1.transform, null, null, true);

            GameObject bunny2 = _poolManager.GetObject(InimigosPossiveisList[0].tagPool, Top[2].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("BackAndFourthMiddle", bunny2.transform, null, null, true);

            GameObject bunny3 = _poolManager.GetObject(InimigosPossiveisList[0].tagPool, Bottom[2].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("BackAndFourthBottom", bunny3.transform, null, null, true);

            quantInimigosWave += 3;

            while (quantInimigosWave > quantInimgiosMortos + quantInimigosEscondidos)
            {
                yield return null;
            }

            StartCoroutine(SpawnGeloBunny1());
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            _levelController.SetEstadoJogo(EstadoJogo.EscolherPoder);
        }

    }
    private IEnumerator SpawnGeloBunny2()
    {
        if (quantInimgiosMortos < quantInimigosFase)
        {
            int atual = 0;

            while(atual < 6)
            {
                GameObject bunny1 = _poolManager.GetObject(InimigosPossiveisList[0].tagPool, Right[3].position, Quaternion.identity);
                _enemyMovement.FollowMovementPattern("MiddleCrossOutTop", bunny1.transform);

                GameObject bunny2 = _poolManager.GetObject(InimigosPossiveisList[0].tagPool, Right[1].position, Quaternion.identity);
                _enemyMovement.FollowMovementPattern("MiddleCrossOutBottom", bunny2.transform);

                atual += 2;

                yield return new WaitForSeconds(0.5f);
            }

            quantInimigosWave += 6;

            while (quantInimigosWave > quantInimgiosMortos + quantInimigosEscondidos)
            {
                yield return null;
            }

            StartCoroutine(SpawnGeloBunny2());
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            _levelController.SetEstadoJogo(EstadoJogo.EscolherPoder);
        }

    }


    #endregion

    #region Fase de Fogo

    private void SpawnFogoInicial()
    {
        switch (numeroFase)
        {
            case < 4:
                ResetarValoresFase(numeroFase < 3 ? 5 : 10);
                StartCoroutine(SpawnFireEye1());
                break;
            case < 6:
                ResetarValoresFase(numeroFase == 4 ? 6 : 12);
                StartCoroutine(SpawnFireEye2());
                break;
            case >= 6:
                //case < 8:
                ResetarValoresFase(numeroFase < 8 ? 5 : (numeroFase < 10 ? 10 : 15));
                StartCoroutine(SpawnFireWhisp());
                break;
            //case >= 8:
            //    ResetarValoresFase(numeroFase < 10 ? 2 : 4);
            //    StartCoroutine(SpawnFireDragon());
            //    break;
        }
    }

    private IEnumerator SpawnFireEye1()
    {
        if (quantInimgiosMortos < quantInimigosFase)
        {
            GameObject enemyEye1 = _poolManager.GetObject(InimigosPossiveisList[0].tagPool, Right[0].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("RightToLeft1", enemyEye1.transform);

            GameObject enemyEye2 = _poolManager.GetObject(InimigosPossiveisList[0].tagPool, Right[1].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("RightToLeft2", enemyEye2.transform);

            GameObject enemyEye3 = _poolManager.GetObject(InimigosPossiveisList[0].tagPool, Right[2].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("RightToLeft3", enemyEye3.transform);

            GameObject enemyEye4 = _poolManager.GetObject(InimigosPossiveisList[0].tagPool, Right[3].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("RightToLeft4", enemyEye4.transform);

            GameObject enemyEye5 = _poolManager.GetObject(InimigosPossiveisList[0].tagPool, Right[4].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("RightToLeft5", enemyEye5.transform);

            quantInimigosWave += 5;

            while (quantInimigosWave > quantInimgiosMortos + quantInimigosEscondidos)
            {
                yield return null;
            }

            StartCoroutine(SpawnFireEye1());
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            _levelController.SetEstadoJogo(EstadoJogo.EscolherPoder);
        }
    }

    private IEnumerator SpawnFireEye2()
    {
        if (quantInimgiosMortos < quantInimigosFase)
        {
            int atual = 0;

            while(atual < 6)
            {
                GameObject enemyEye1 = _poolManager.GetObject(InimigosPossiveisList[0].tagPool, Right[0].position, Quaternion.identity);
                _enemyMovement.FollowMovementPattern("RightToLeft1", enemyEye1.transform);

                GameObject enemyEye2 = _poolManager.GetObject(InimigosPossiveisList[0].tagPool, Right[2].position, Quaternion.identity);
                _enemyMovement.FollowMovementPattern("RightToLeft3", enemyEye2.transform);

                GameObject enemyEye3 = _poolManager.GetObject(InimigosPossiveisList[0].tagPool, Right[4].position, Quaternion.identity);
                _enemyMovement.FollowMovementPattern("RightToLeft5", enemyEye3.transform);

                atual += 3;

                yield return new WaitForSeconds(2f);

            }

            quantInimigosWave += 6;

            while (quantInimigosWave > quantInimgiosMortos + quantInimigosEscondidos)
            {
                yield return null;
            }

            StartCoroutine(SpawnFireEye2());
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            _levelController.SetEstadoJogo(EstadoJogo.EscolherPoder);
        }
    }
    private IEnumerator SpawnFireWhisp()
    {
        if (quantInimgiosMortos < quantInimigosFase)
        {
            int atual = 0;

            while (atual < 5)
            {
                GameObject enemyWhisp = _poolManager.GetObject(InimigosPossiveisList[1].tagPool, Bottom[3].position, Quaternion.identity);
                _enemyMovement.FollowMovementPattern("ZigZagBottom", enemyWhisp.transform);

                atual++;

                yield return new WaitForSeconds(1.2f);

            }

            quantInimigosWave += 5;

            while (quantInimigosWave > quantInimgiosMortos + quantInimigosEscondidos)
            {
                yield return null;
            }

            StartCoroutine(SpawnFireWhisp());
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            _levelController.SetEstadoJogo(EstadoJogo.EscolherPoder);
        }
    }
    private IEnumerator SpawnFireDragon()
    {
        if (quantInimgiosMortos < quantInimigosFase)
        {
            GameObject enemyDragon1 = _poolManager.GetObject(InimigosPossiveisList[2].tagPool, Top[1].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("BackAndFourthHalfTop", enemyDragon1.transform, null, null, true);
            
            GameObject enemyDragon2 = _poolManager.GetObject(InimigosPossiveisList[2].tagPool, Bottom[1].position, Quaternion.identity);
            _enemyMovement.FollowMovementPattern("BackAndFourthHalfBottom", enemyDragon2.transform, null, null, true);


            quantInimigosWave += 5;

            while (quantInimigosWave > quantInimgiosMortos + quantInimigosEscondidos)
            {
                yield return null;
            }

            StartCoroutine(SpawnFireDragon());
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
            _levelController.SetEstadoJogo(EstadoJogo.EscolherPoder);
        }
    }

    #endregion
}
