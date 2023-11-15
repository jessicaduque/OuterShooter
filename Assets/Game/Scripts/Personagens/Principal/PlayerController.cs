using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singleton;

public class PlayerController : Singleton<PlayerController>
{
    private RuntimeAnimatorController AnimController;

    [Header("Específicos poder")]
    [SerializeField] PoderDetails poderAtual;

    private BoxCollider2D thisCollider;
    private int vidas = 1;

    public PlayerAttack _playerAttack;

    private PlayerMovement _playerMovement => PlayerMovement.I;
    private LevelController _levelController => LevelController.I;

    private UIController _uiController => UIController.I;

    

    private new void Awake()
    {
        AnimController = GetComponent<RuntimeAnimatorController>();
        thisCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        SetarPoder();
    }

    #region Dano

    public void LevarDano(int dano = 1)
    {
        // Checar se tem escudo

        vidas -= dano;

        if(vidas <= 0)
        {
            _playerMovement.SetAnimatorUnscaled(true);
            _uiController.SetPausePanelActive(true);
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

    #endregion

    public IEnumerator Reviver()
    {
        _playerMovement.SetAnimatorUnscaled(false);
        _playerMovement.AnimateBool("Morte", false);
        _uiController.SetPausePanelActive(false);
        _playerMovement.PermitirMovimento(true);
        yield return new WaitForSeconds(1);
        thisCollider.enabled = true;
    }

    private void SetarPoder()
    {
        //poderPrefab = poderAtual.poderPrefab;
        AnimController = poderAtual.poderPlayerAnimControl;
    }

}
