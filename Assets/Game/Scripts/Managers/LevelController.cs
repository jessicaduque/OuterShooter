using System.Collections.Generic;
using UnityEngine;
using Utils.Singleton;

public class LevelController : Singleton<LevelController>
{
    [Header("Elementos de uma fase")]
    [SerializeField] private List<FaseDetails> fases;
    [SerializeField] private FaseDetails faseTerra;
    private FaseDetails faseAtual;
    private FaseDetails fasePassada;
    [SerializeField] private GameObject SpawnObjetosGameObject;

    EstadoJogo estadoAtualLevel;

    private bool chanceExtra = true;   

    [SerializeField] private int numeroFase = 0;
    private int fasesSemTerra = 0;

    private BankManager _bankManager => BankManager.I;
    private UIController _uiController => UIController.I;
    private PlayerMovement _playerMovement => PlayerMovement.I;
    private PlayerController _playerController => PlayerController.I;
    private SpawnObjetos _spawnObjetos => SpawnObjetos.I;
    private SpawnManager _spawnManager => SpawnManager.I;
    private PoolManager _poolManager => PoolManager.I;
    private AudioManager _audioManager => AudioManager.I;

    private new void Awake()
    {

    }

    #region EstadoJogo

    private void ControleEstados()
    {
        switch (estadoAtualLevel)
        {
            case EstadoJogo.Inicial:
                IniciarJogo();
                break;
            case EstadoJogo.CriarFase:
                CriarFase();
                break;
            case EstadoJogo.Lutar:
                Lutar();
                break;
            case EstadoJogo.EscolherPoder:
                EscolherPoderInicial();
                break;
            case EstadoJogo.Terra:
                Terra();
                break;
        }
    }

    public void SetEstadoJogo(EstadoJogo estado)
    {
        estadoAtualLevel = estado;
        ControleEstados();
    }

    public void RestartStraightGame()
    {
        _uiController.SetStartPanelFalse();
        SetEstadoJogo(EstadoJogo.Inicial);
    }
    #endregion

    #region InicioJogo

    private void IniciarJogo()
    {
        if(numeroFase == 0)
        {
            _playerController.ActivateHealthBar();
            _playerController.SetarPoder(faseTerra.fasePoder);
            faseAtual = faseTerra;
            fasePassada = faseTerra;
            _playerMovement.PermitirMovimento(false);
            StartCoroutine(_uiController.MoverPlanetaFora());
            StartCoroutine(_playerMovement.MoverParaX());
        }
        else
        {
            _playerMovement.PermitirMovimento(true);
            StartCoroutine(_uiController.MoverPlanetaFora(2.5f));
        }
    }

    public void IniciarJogoFinal()
    {
        SetEstadoJogo(EstadoJogo.CriarFase);
    }

    #endregion

    #region CriarFase
    
    private void CriarFase()
    {
        numeroFase++;
        fasesSemTerra++;
        if(numeroFase == 1)
        {
            AleatorizarFase();
        }
        else if(faseAtual == faseTerra)
        {
            SetEstadoJogo(EstadoJogo.Terra);
            return;
        }
        _uiController.SetarPlanetaAnimator(faseAtual.faseAnimControl);
        StartCoroutine(_uiController.MoverPlanetaDentro());
        
    }
    public void SpawnInimigos()
    {
        SetEstadoJogo(EstadoJogo.Lutar);
        _spawnManager.ComecarNovaFase(new List<Pool>(faseAtual.faseInimiPossiveis), faseAtual.faseNome, numeroFase);
    }

    #endregion

    #region Lutar

    private void Lutar()
    {
        SpawnObjetosGameObject.SetActive(true);
        _playerController.DefineActivateAttack(true);
    }

    #endregion

    #region EscolherPoder
    
    private void EscolherPoderInicial()
    {
        fasePassada = faseAtual;
        SpawnObjetosGameObject.SetActive(false);
        FinalFase();
        ReturnShotsAndObjectsPool();
        _playerController.DefineActivateAttack(false);
        StartCoroutine(_playerMovement.MoverParaMeio());
        StartCoroutine(_uiController.MoverPlanetaPlayer());
    }

    public void EscolherPoder()
    {
        _playerMovement.AnimateBool("Mover", false);
        AleatorizarFase();
        _uiController.ControlEscolhaPanel(true, _playerController.GetPoderAtual().poderSpriteEscolha, fasePassada.fasePoder.poderSpriteEscolha, faseAtual.fasePlanetaSprite);
    }

    private void AleatorizarFase()
    {
        if (fasesSemTerra == 3)
        {
            faseAtual = faseTerra;
            fasesSemTerra = 0;
        }
        else
        {
            while (faseAtual == fasePassada)
            {
                faseAtual = fases[Random.Range(0, fases.Count)];
            }
        }

    }

    public void EscolherPoderFinal(bool escolherPoderNovo)
    {
        if (escolherPoderNovo)
        {
            _playerController.SetarPoder(fasePassada.fasePoder);
        }
        _uiController.ControlEscolhaPanel(false);
        _playerMovement.PermitirMovimento(true);
        _playerMovement.AnimateBool("Mover", true);
        fasePassada = faseAtual;
        SetEstadoJogo(EstadoJogo.Inicial);
    }

    #endregion

    #region Terra
    
    private void Terra()
    {
        _uiController.ResetPositionPlanet();
        _uiController.SetarPlanetaAnimator(faseAtual.faseAnimControl);
        StartCoroutine(_playerMovement.MoverParaMeio());
        StartCoroutine(_uiController.MoverPlanetaPlayer(9));
    }

    #endregion

    #region FinalJogo

    public void ChecarMaisUmaChance()
    {
        Time.timeScale = 0;

        if (chanceExtra)
        {
            _uiController.ControlAdChancePanel(true);
            chanceExtra = false;
        }
        else
        {
            _uiController.ControlGameOverPanel(true);
        }
    }

    public void MaisUmaChance()
    {
        _uiController.ControlAdChancePanel(false);
        StartCoroutine(_playerController.Reviver());
        ReturnShotsAndObjectsPool();
        SetEstadoJogo(EstadoJogo.Lutar);
        Time.timeScale = 1;
    }

    private void ReturnShotsAndObjectsPool()
    {
        foreach (Asteroide asteroid in FindObjectsOfType<Asteroide>())
        {
            if (asteroid.gameObject.activeInHierarchy)
            {
                asteroid.Explode();
            }
        }

        foreach (Shot shot in FindObjectsOfType<Shot>())
        {
            if (shot.gameObject.activeInHierarchy)
            {
                if (shot.shotPlayer)
                {
                    _poolManager.ReturnPool(shot.gameObject);
                }
                else
                {
                    shot.Explodir();
                }
                
            }
        }

    }

    private void FinalFase()
    {
        foreach (Desejo desejo in FindObjectsOfType<Desejo>())
        {
            if (desejo.gameObject.activeInHierarchy)
            {
                if (desejo.GetComponent<SpriteRenderer>().isVisible)
                {
                    desejo.MoveToPlayer();
                }
                else
                {
                    _poolManager.ReturnPool(desejo.gameObject);
                }
            }
        }
    }

    public FaseDetails GetFaseAtual()
    {
        return faseAtual;
    }

    #endregion
}
