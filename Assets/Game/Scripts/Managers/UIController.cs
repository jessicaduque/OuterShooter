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
    [SerializeField] GameObject EscolherPoderPanel;

    [Space(20)]
    [Header("Botões")]
    [SerializeField] Button b_iniciarJogo;
    [SerializeField] Button b_creditos;
    [SerializeField] Button b_video;
    [SerializeField] Button b_pause;
    [SerializeField] Button b_ultimate;
    [SerializeField] Button b_reiniciarGameOver;
    [SerializeField] Button b_menuGameOver;
    [SerializeField] Button b_adButton1;
    [SerializeField] Button b_adButton2;
    [SerializeField] Button b_escolhaAtual;
    [SerializeField] Button b_escolhaNovo;

    [Space(20)]
    [Header("Textos")]
    [SerializeField] private TMP_Text t_score;
    [SerializeField] private TMP_Text t_quantEstrelas;
    [SerializeField] private TMP_Text t_scoreFinal;
    [SerializeField] private TMP_Text t_bestScoreFinal;

    private Image im_PoderAtual;
    private Image im_PoderNovo;

    [Space(20)]
    [Header("CanvasGroup")]
    [SerializeField] private CanvasGroup score;
    [SerializeField] private CanvasGroup bestScore;


    [Space(20)]
    [Header("ScrollRect")]
    [SerializeField] private ScrollRect creditsScrollRect;

    [Space(20)]
    [Header("Planeta")]
    [SerializeField] GameObject planetaObjeto;
    [SerializeField] float posXFinalPlanetaFora;
    [SerializeField] float posXInicioPlanetaDentro;
    [SerializeField] float posXFinalPlanetaDentro;
    private Animator planetaAnimator;

    private float ultimatePoints = 0;
    private float ultimateMaxPoints;

    private PlayerAttack _playerAttack;

    private LevelController _levelController => LevelController.I;
    private ControleFadePreto _fadePreto => ControleFadePreto.I;
    private BackgroundController _backgroundController => BackgroundController.I;

    private BankManager _bankManager => BankManager.I;
    private ScoreManager _scoreManager => ScoreManager.I;

    private AudioManager _audioManager => AudioManager.I;
    private new void Awake()
    {
        b_video.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.9f;
        planetaAnimator = planetaObjeto.GetComponent<Animator>();

        im_PoderAtual = b_escolhaAtual.GetComponent<Image>();
        im_PoderNovo = b_escolhaNovo.GetComponent<Image>();

        // Button Listeners
        b_iniciarJogo.onClick.AddListener(IniciarJogo);
        b_creditos.onClick.AddListener(() => ControlCreditsPanel(true));
        b_pause.onClick.AddListener(() => ControlPausePanel(true));
        b_ultimate.onClick.AddListener(ApertouUltimate);
        b_adButton1.onClick.AddListener(ShowAd);
        b_adButton2.onClick.AddListener(ShowAd);
        

        b_ultimate.enabled = false;
    }

    private void Start()
    {
        b_escolhaNovo.onClick.AddListener(() =>
        {
            b_escolhaNovo.transform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 1).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
            {
                _levelController.EscolherPoderFinal(true);
                b_escolhaNovo.interactable = false;
                b_escolhaAtual.interactable = false;
            });
            
        });
        b_escolhaAtual.onClick.AddListener(() =>
        {
            b_escolhaAtual.transform.DOScale(new Vector3(1.4f, 1.4f, 1.4f), 0.4f).SetLoops(2, LoopType.Yoyo).OnComplete(() =>
            {
                _levelController.EscolherPoderFinal(false);
                b_escolhaNovo.interactable = false;
                b_escolhaAtual.interactable = false;
            });
        });


        _bankManager.AumentouEstrelas += AtualizarTextoEstrelas;
        _scoreManager.AumentouScore += AtualizarTextoScore;
    }

    private void IniciarJogo()
    {
        _audioManager.PlayCrossFade("Main");
        _audioManager.PlaySfx("ButtonClick");
        b_iniciarJogo.enabled = false;
        ControlStartPanel();
        _levelController.SetEstadoJogo(EstadoJogo.Inicial);
    }

    #region Textos

    private void AtualizarTextoEstrelas()
    {
        t_quantEstrelas.text = _bankManager.GetQuantEstrelas().ToString();
    }

    private void AtualizarTextoScore()
    {
        t_score.text = _scoreManager.GetScore().ToString();
    }

    private void AtualizarTextosScoreFinal()
    {
        b_menuGameOver.enabled = false;
        b_reiniciarGameOver.enabled = false;
        t_bestScoreFinal.text = _scoreManager.GetBestScore().ToString(); // DEPOIS TROCA BANKMANAGER PARA SCOREMANAGER (TEMPORARIO)
        Sequence seq = DOTween.Sequence().SetUpdate(true);
        seq.PrependInterval(0.2f);
        seq.Append(score.DOFade(1, 0.3f));
        seq.Append(bestScore.DOFade(1, 0.3f));
        seq.Join(DOTween.To(x => t_scoreFinal.text = ((int) x).ToString(), 0, _scoreManager.GetScore(), 1.2f)); // DEPOIS TROCA BANKMANAGER PARA SCOREMANAGER (TEMPORARIO)
        seq.Append(b_menuGameOver.transform.DOScale(new Vector3(1f, 1f, 1f), 1f).SetEase(Ease.OutBounce).OnComplete(() => b_menuGameOver.enabled = true));
        seq.Join(b_reiniciarGameOver.transform.DOScale(new Vector3(1f, 1f, 1f), 1f).SetEase(Ease.OutBounce).OnComplete(() => b_reiniciarGameOver.enabled = true));
    }

    #endregion

    #region Panels

    private void ControlStartPanel()
    {
        Helpers.FadeCrossPanel(StartPanel, UIPanel);
    }

    public void ControlCreditsPanel(bool estado)
    {
        if (estado) { creditsScrollRect.verticalNormalizedPosition = 1; }
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
    public void SetPausePanelActive(bool estado)
    {
        PausePanel.SetActive(estado);
    }
    public void PauseListener(bool state)
    {
        b_pause.enabled = state;
    }
    public void SetStartPanelFalse()
    {
        StartPanel.SetActive(false);
        Helpers.FadeInPanel(UIPanel);
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
            ChancePanel.SetActive(false);
        }
        Time.timeScale = (estado ? 0 : 1);
    }

    public void ControlGameOverPanel(bool estado)
    {
        if (estado)
        {
            AtualizarTextosScoreFinal();
            Helpers.FadeInPanel(GameOverPanel);
        }
        else
        {
            Helpers.FadeOutPanel(GameOverPanel);
        }
        Time.timeScale = (estado ? 0 : 1);
    }

    public void ControlEscolhaPanel(bool estado, Sprite atual, Sprite novo)
    {
        if (estado)
        {
            b_escolhaNovo.interactable = true;
            b_escolhaAtual.interactable = true;
            im_PoderAtual.sprite = atual;
            im_PoderNovo.sprite = novo;
            Helpers.FadeInPanel(EscolherPoderPanel);
        }
        else
        {
            Helpers.FadeOutPanel(EscolherPoderPanel);
        }
    }

    #endregion

    #region Planeta

    public IEnumerator MoverPlanetaFora(float tempo = 4)
    {
        _backgroundController.MudarEstadoParallax(true);
        yield return new WaitForSeconds(0.4f);
        Vector3 posFinal = new Vector3(posXFinalPlanetaFora, 0, 0);
        planetaObjeto.transform.DOMove(posFinal, tempo).SetEase(Ease.InSine);
        while(planetaObjeto.transform.position != posFinal)
        {
            yield return null;
        }
        _levelController.IniciarJogoFinal();
    }

    public IEnumerator MoverPlanetaPlayer(float tempo = 7)
    {
        _backgroundController.MudarEstadoParallax(true);
        yield return new WaitForSeconds(0.4f);
        Vector3 posFinal = new Vector3(-7.2f, 0, 0);
        planetaObjeto.transform.DOMove(posFinal, tempo).SetEase(Ease.OutSine);
        while (planetaObjeto.transform.position.x > -7.2f)
        {
            if(planetaObjeto.transform.position.x  < -7.205f)
            {
                planetaObjeto.transform.position = posFinal;
                yield return null;
            }
            yield return null;
        }
        Debug.Log("asd");
        _levelController.EscolherPoder();
    }

    public IEnumerator MoverPlanetaDentro()
    {
        _playerAttack = FindObjectOfType<PlayerAttack>();
        yield return new WaitForSeconds(1.5f);
        _backgroundController.MudarEstadoParallax(false);
        ResetPositionPlanet();
        Vector3 posFinal = new Vector3(posXFinalPlanetaDentro, 0, 0);
        planetaObjeto.transform.DOMove(posFinal, 4f).SetEase(Ease.OutSine).OnComplete(() => _levelController.SpawnInimigos());
    }

    public void ResetPositionPlanet()
    {
        planetaObjeto.transform.position = new Vector2(posXInicioPlanetaDentro, 0);
        DOTween.Kill(planetaObjeto.transform);
    }

    #endregion

    #region Poder

    public void SetarPlayerAttack(PlayerAttack _playerAttack)
    {
        this._playerAttack = _playerAttack;
    }

    public void SetarPlanetaAnimator(RuntimeAnimatorController animator)
    {
        planetaAnimator.runtimeAnimatorController = animator;
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

    public void RestartStraightGame()
    {
        _audioManager.StopMusic();
        _fadePreto.RestartStraightGame();
    }

    public void BackMenu()
    {
        _audioManager.PlayCrossFade("Menu");
        _fadePreto.FadeOutScene("Main");
    }

    private void ShowAd()
    {
        b_adButton1.enabled = false;
        b_adButton2.enabled = false;
        _audioManager.PlaySfx("ButtonClick");
        AdController.I.ShowAd();
    }

    #endregion
}
