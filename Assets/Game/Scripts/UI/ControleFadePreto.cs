using Utils.Singleton;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleFadePreto : Singleton<ControleFadePreto>
{
    [SerializeField] public GameObject TelaPretaPanel;
    [SerializeField] public CanvasGroup cg_TelaPreta;

    private float tempoFadePreto => Helpers.tempoPretoFade;

    protected override void Awake()
    {
        base.Awake();

        Time.timeScale = 1;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FadeInSceneStart();
    }

    public void FadeInSceneStart()
    {
        TelaPretaPanel.SetActive(true);
        cg_TelaPreta.alpha = 1f;
        cg_TelaPreta.DOFade(0, tempoFadePreto).onComplete = () => TelaPretaPanel.SetActive(false);
    }

    public void FadeOutScene(string nomeScene)
    {
        TelaPretaPanel.SetActive(true);
        cg_TelaPreta.DOFade(1, tempoFadePreto).OnComplete(() => SceneManager.LoadScene(nomeScene)).SetUpdate(true);
    }

    public void FadePanel(GameObject panel, bool estado)
    {
        TelaPretaPanel.SetActive(true);
        cg_TelaPreta.DOFade(1, tempoFadePreto).onComplete = () => { 
            panel.SetActive(estado); 
            FadeInSceneStart(); 
        };
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}