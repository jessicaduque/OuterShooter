using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singleton;

public class PlayerController : Singleton<PlayerController>
{
    private RuntimeAnimatorController AnimController;

    [Header("Scripts de ataque do Player")]
    [SerializeField] private PlayerAttack[] playerAttackScripts;

    [Header("Específicos poder")]
    private PoderDetails poderAtual;

    private BoxCollider2D thisCollider;
    private int vidas = 1;

    private PlayerMovement _playerMovement => PlayerMovement.I;
    private LevelController _levelController => LevelController.I;

    private UIController _uiController => UIController.I;

    

    private new void Awake()
    {
        AnimController = GetComponent<RuntimeAnimatorController>();
        thisCollider = GetComponent<BoxCollider2D>();
    }

    #region Dano

    public void LevarDano(int dano = 1)
    {
        // Checar se tem escudo

        vidas -= dano;

        if(vidas <= 0)
        {
            _playerMovement.SetAnimatorUnscaled(true);
            _uiController.PauseListener(false);
            _playerMovement.AnimateBool("Morte", true);
            _playerMovement.AnimateTrigger("TrigMorte");
            _playerMovement.PermitirMovimento(false);
            thisCollider.enabled = false;
            Time.timeScale = 0;
        }
    }

    public void Morrer()
    {
        _levelController.ChecarMaisUmaChance();
    }

    public IEnumerator Reviver()
    {
        _uiController.PauseListener(true);
        _playerMovement.SetAnimatorUnscaled(false);
        _playerMovement.AnimateBool("Morte", false);
        _uiController.SetPausePanelActive(false);
        _playerMovement.PermitirMovimento(true);
        yield return new WaitForSeconds(1);
        thisCollider.enabled = true;
    }

    public void DefineActivateAttack()
    {
        playerAttackScripts[poderAtual.poderID].enabled = true;
    }

    #endregion

    public void SetarPoder(FaseDetails fase)
    {
        poderAtual = fase.fasePoder;
        SetarPoderAnimator();
    }

    private void SetarPoderAnimator()
    {
        AnimController = poderAtual.poderPlayerAnimControl;
    }

}
