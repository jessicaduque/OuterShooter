using System.Collections.Generic;
using UnityEngine;
using Utils.Singleton;

public class LevelController : Singleton<LevelController>
{
    [Header("Elementos de uma fase")]
    private List<FaseDetails> fases;
    private FaseDetails faseAtual;
    private FaseDetails fasePassada;

    EstadoJogo estadoAtualLevel;

    private bool chanceExtra = true;   

    private int numeroFase = 0;

    private BankManager _bankManager => BankManager.I;
    private UIController _uiController => UIController.I;
    private PlayerMovement _playerMovement => PlayerMovement.I;
    private PlayerController _playerController => PlayerController.I;
    private FaseList _faseList => FaseList.Instance;
    private SpawnObjetos _spawnObjetos => SpawnObjetos.I;

    private new void Awake()
    {
        fases = _faseList.GetFasesSemTerra();
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
                //movendoPlaneta = false;
                ////PlayerMov.AnimatateAttack();
                ////PlayerContr.PermitirAtacar();
                //// Código
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
        StartCoroutine(_playerMovement.MoverParaX());
        StartCoroutine(_uiController.MoverPlanetaFora());
    }

    #endregion

    #region CriarFase
    
    private void CriarFase()
    {
        numeroFase++;
        _spawnObjetos.enabled = true;
        AleatorizarFase();
    }

    private void AleatorizarFase()
    {
        faseAtual = fases[Random.Range(0, fases.Count)];
        _uiController.SetarPlanetaAnimator(faseAtual.faseAnimControl);
        //WaveManager.I.SpawnarWave(new List<GameObject>(faseAtual.faseInimiPossiveis));
    }

    #endregion

    #region Lutar

    private void Lutar()
    {
        
    }

    #endregion

    #region FinalJogo

    public void ChecarMaisUmaChance()
    {
        Time.timeScale = 0;

        if (chanceExtra)
        {
            _uiController.ControlAdChancePanel(true);
        }
        else
        {
            _uiController.ControlGameOverPanel(true);
        }
    }

    public void MaisUmaChance()
    {
        Time.timeScale = 1;
        SetEstadoJogo(EstadoJogo.Lutar);
    }

    #endregion

}
