using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;

public class PlayerController : Singleton<PlayerController>
{
    private Animator AnimController;

    [Header("Scripts de ataque do Player")]
    [SerializeField] private PlayerAttack[] playerAttackScripts;

    [Header("Específicos poder")]
    private PoderDetails poderAtual;

    private BoxCollider2D thisCollider;

    [Header("Vida")]
    private int maxHP = 10;
    private float currentHP;
    GameObject healthBar;
    Image healthBarFill;
    CanvasGroup cg_HealthBar;

    private PlayerMovement _playerMovement => PlayerMovement.I;
    private LevelController _levelController => LevelController.I;

    private UIController _uiController => UIController.I;

    

    private new void Awake()
    {
        AnimController = GetComponent<Animator>();
        thisCollider = GetComponent<BoxCollider2D>();
        currentHP = maxHP;
    }

    #region Dano

    public void ActivateHealthBar()
    {
        healthBar = PoolManager.I.GetObject("HealthBar", transform.position, Quaternion.identity);
        healthBarFill = healthBar.GetComponentsInChildren<Image>()[1];
        healthBarFill.fillAmount = 1;
        healthBar.GetComponent<HealthBar>().SetInimigo(gameObject);
        cg_HealthBar = healthBar.AddComponent<CanvasGroup>();
        cg_HealthBar.alpha = 0;
        FadeHealthBar(true);
    }

    private void FadeHealthBar(bool estado)
    {
        cg_HealthBar.DOFade((estado ? 1 : 0), 0.5f).SetUpdate(true);
    }

    public void LevarDano(float dano = 1)
    {
        // Checar se tem escudo

        currentHP -= dano;
        healthBarFill.fillAmount -= dano / maxHP;

        if (currentHP <= 0)
        {
            _playerMovement.SetAnimatorUnscaled(true);
            _uiController.PauseListener(false);
            _playerMovement.AnimateBool("Morte", true);
            _playerMovement.AnimateTrigger("TrigMorte");
            _playerMovement.PermitirMovimento(false);
            thisCollider.enabled = false;
            FadeHealthBar(false);
            Time.timeScale = 0;
        }
    }

    public void Morrer()
    {
        _levelController.ChecarMaisUmaChance();
    }

    public IEnumerator Reviver()
    {
        healthBarFill.fillAmount = 1;
        currentHP = maxHP;
        FadeHealthBar(true);
        _uiController.PauseListener(true);
        _playerMovement.SetAnimatorUnscaled(false);
        _playerMovement.AnimateBool("Morte", false);
        _playerMovement.PermitirMovimento(true);
        yield return new WaitForSeconds(1);
        thisCollider.enabled = true;
    }

    #endregion
    public void DefineActivateAttack(bool state)
    {
        playerAttackScripts[poderAtual.poderID].enabled = state;
    }

    public void SetarPoder(PoderDetails poder)
    {
        poderAtual = poder;
        SetarPoderAnimator();
    }

    public PoderDetails GetPoderAtual()
    {
        return poderAtual;
    }

    private void SetarPoderAnimator()
    {
        AnimController.runtimeAnimatorController = poderAtual.poderPlayerAnimControl;
    }

}
