using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Utils.Singleton;
using DG.Tweening;
using System.Collections;

public class UIController : Singleton<UIController>
{
    [Header("Panels")]
    [SerializeField] GameObject UIPanel;
    [SerializeField] GameObject PausePanel;
    [SerializeField] GameObject ChancePanel;
    [SerializeField] GameObject StartPanel;
    [SerializeField] GameObject CreditsPanel;
    [SerializeField] GameObject GameOverPanel;

    [Space(20)]
    [Header("Botões")]
    [SerializeField] Button b_iniciarJogo;
    [SerializeField] Button b_creditos;
    [SerializeField] Button b_pause;
    [SerializeField] Button b_ultimate;

    [Space(20)]
    [Header("Textos")]
    [SerializeField] private TMP_Text t_score;
    [SerializeField] private TMP_Text t_quantEstrelas;

    [Space(20)]
    [Header("Planeta")]
    [SerializeField] GameObject planetaObjeto;
    [SerializeField] float posXFinalPlanetaFora;
    [SerializeField] float posXInicioPlanetaDentro;
    [SerializeField] float posXFinalPlanetaDentro;
    private RuntimeAnimatorController planetaAnimator;

    private float ultimatePoints = 0;
    private float ultimateMaxPoints;

    private PlayerAttack _playerAttack;

    private LevelController _levelController => LevelController.I;
    private ControleFadePreto _fadePreto => ControleFadePreto.I;
    private BackgroundController _backgroundController => BackgroundController.I;
    private PlayerController _playerController => PlayerController.I;

    private BankManager _bankManager => BankManager.I;
    private ScoreManager _scoreManager => ScoreManager.I;

    private new void Awake()
    {
        b_iniciarJogo.onClick.AddListener(IniciarJogo);
        b_creditos?.onClick.AddListener(() => ControlCreditsPanel(true));
        b_pause.onClick.AddListener(() => ControlPausePanel(true));
        b_ultimate.enabled = false;
        b_ultimate.onClick.AddListener(ApertouUltimate);
    }

    private void Start()
    {
        _bankManager.AumentouEstrelas += AtualizarTextoEstrelas;
        _scoreManager.AumentouScore += AtualizarTextoScore;
    }

    private void IniciarJogo()
    {
        ControlStartPanel();
        _backgroundController.MudarEstadoParallax(true);
        _levelController.SetEstadoJogo(EstadoJogo.Inicial);
    }

    #region Textos

    private void AtualizarTextoEstrelas()
    {
        t_quantEstrelas.text = _bankManager.GetQuantEstrelas().ToString();
    }

    private void AtualizarTextoScore()
    {
        t_quantEstrelas.text = _scoreManager.GetScore().ToString();
    }

    #endregion

    #region Panels

    private void ControlStartPanel()
    {
        Helpers.FadeCrossPanel(StartPanel, UIPanel);
    }

    private void ControlCreditsPanel(bool estado)
    {
        _fadePreto.FadePanel(CreditsPanel, estado);
    }

    public void ControlPausePanel(bool estado)
    {
        if (estado)
        {
            Helpers.FadeInPanel(PausePanel);
        }
        else
        {
            Helpers.FadeOutPanel(PausePanel);
        }
        Time.timeScale = (estado ? 0 : 1);
    }

    public void ControlUIPanel(bool estado)
    {
        if (estado)
        {
            Helpers.FadeInPanel(UIPanel);
        }
        else
        {
            Helpers.FadeOutPanel(UIPanel);
        }
    }

    public void ControlAdChancePanel(bool estado)
    {
        if (estado)
        {
            Helpers.FadeInPanel(ChancePanel);
        }
        else
        {
            Helpers.FadeOutPanel(ChancePanel);
        }
        Time.timeScale = (estado ? 0 : 1);
    }

    public void ControlGameOverPanel(bool estado)
    {
        if (estado)
        {
            Helpers.FadeInPanel(GameOverPanel);
        }
        else
        {
            Helpers.FadeOutPanel(GameOverPanel);
        }
        Time.timeScale = (estado ? 0 : 1);
    }

    #endregion

    #region Planeta

    public IEnumerator MoverPlanetaFora()
    {
        Vector3 posFinal = new Vector3(posXFinalPlanetaFora, 0, 0);
        while (planetaObjeto.transform.position != posFinal)
        {
            planetaObjeto.transform.position = Vector3.MoveTowards(planetaObjeto.transform.position, posFinal, 1.6f * Time.deltaTime);
            yield return null;
        }
    }

    public IEnumerator MoverPlanetaDentro()
    {
        planetaObjeto.transform.position = new Vector2(posXInicioPlanetaDentro, 0);
        Vector3 posFinal = new Vector3(posXFinalPlanetaDentro, 0, 0);
        while (planetaObjeto.transform.position != posFinal)
        {
            planetaObjeto.transform.position = Vector3.MoveTowards(planetaObjeto.transform.position, posFinal, 20f * Time.deltaTime);
            yield return null;
        }
    }

    #endregion

    #region Poder

    public void SetarPlayerAttack(PlayerAttack _playerAttack)
    {
        this._playerAttack = _playerAttack;
    }

    public void SetarPlanetaAnimator(RuntimeAnimatorController animator)
    {
        planetaAnimator = animator;
    }

    #endregion

    #region Ultimate

    private void ApertouUltimate()
    {
        _playerAttack.AtaqueUltimate();
        b_ultimate.enabled = false;
        ultimatePoints = 0;
    }

    public void SetarPontosUltimate(float pontos)
    {
        ultimateMaxPoints = pontos;
    }

    public void AdicionarPontosUltimate(float pontos)
    {
        ultimatePoints += pontos;

        if (ultimatePoints >= ultimateMaxPoints)
        {
            ultimatePoints = ultimateMaxPoints;
            _playerAttack.EstadoUltimate(true);
            b_ultimate.enabled = true;
        }
    }

    #endregion

    #region Scenes

    public void RecomecarJogo()
    {
        _fadePreto.FadeOutScene("Main");
    }

    #endregion
}
