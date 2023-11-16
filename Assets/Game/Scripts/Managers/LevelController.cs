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

    [Header("Scripts de ataque do Player")]
    [SerializeField] private PlayerAttack[] playerAttackScripts;
    private PlayerAttack _playerAttack;

    [SerializeField] GameObject SpawnObjetosGameObject;

    EstadoJogo estadoAtualLevel;

    private bool chanceExtra = true;   

    private int numeroFase = 0;

    private BankManager _bankManager => BankManager.I;
    private UIController _uiController => UIController.I;
    private PlayerMovement _playerMovement => PlayerMovement.I;
    private PlayerController _playerController => PlayerController.I;
    private SpawnObjetos _spawnObjetos => SpawnObjetos.I;
    private SpawnManager _spawnManager => SpawnManager.I;

    private AudioManager _audioManager => AudioManager.I;

    private new void Awake()
    {
        SetEstadoJogo(EstadoJogo.AntesInicio);
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
                //GetComponent<GerenciadorDeExtras>().enabled = false;
                // Código
                break;
            case EstadoJogo.Terra:
                // Código
                break;
            case EstadoJogo.Morte:
                //GetComponent<GerenciadorDeExtras>().enabled = false;
                // Código
                break;
        }
    }

    public void SetEstadoJogo(EstadoJogo estado)
    {
        estadoAtualLevel = estado;
        ControleEstados();
    }


    #endregion

    #region InicioJogo

    private void IniciarJogo()
    {
        _audioManager.PlayCrossFade("Main");
        StartCoroutine(_playerMovement.MoverParaX());
        StartCoroutine(_uiController.MoverPlanetaFora());
    }

    public void IniciarJogoFinal()
    {
        faseAtual = faseTerra;
        SetEstadoJogo(EstadoJogo.CriarFase);
    }

    #endregion

    #region CriarFase
    
    private void CriarFase()
    {
        numeroFase++;
        //SpawnObjetosGameObject.SetActive(true);
        AleatorizarFase();
    }

    private void AleatorizarFase()
    {
        faseAtual = fases[Random.Range(0, fases.Count)];
        _uiController.SetarPlanetaAnimator(faseAtual.faseAnimControl);
    }

    public void SpawnInimigos()
    {
        //SpawnManager.I.SpawnarWave(new List<GameObject>(faseAtual.faseInimiPossiveis));
        SetEstadoJogo(EstadoJogo.Lutar);
    }

    #endregion

    #region Lutar

    private void Lutar()
    {
        DefineActivateAttack(faseAtual.faseID);
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
        StartCoroutine(_playerController.Reviver());   
        _uiController.ControlAdChancePanel(false);
        Time.timeScale = 1;
        SetEstadoJogo(EstadoJogo.Lutar);
    }

    #endregion

    #region Player Activate Attack
    private void DefineActivateAttack(int id)
    {
        playerAttackScripts[id].enabled = true;
    }
    #endregion
}
