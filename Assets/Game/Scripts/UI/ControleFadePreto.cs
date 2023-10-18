using Utils.Singleton;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControleFadePreto : Singleton<ControleFadePreto>
{
    [SerializeField] public GameObject TelaPretaPanel;
    [SerializeField] public CanvasGroup cg_TelaPreta;

    protected override void Awake()
    {
        base.Awake();

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
        cg_TelaPreta.DOFade(0, 0.2f).onComplete = () => TelaPretaPanel.SetActive(false);
    }

    public void FadeOutScene(string nomeScene)
    {
        TelaPretaPanel.SetActive(true);
        cg_TelaPreta.DOFade(1, 0.2f).onComplete = () => SceneManager.LoadScene(nomeScene);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}