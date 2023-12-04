using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    protected float ultimatePoints;
    [SerializeField] protected float ultimateMaxPoints;
    protected bool ultimateLiberado;
    [SerializeField] CanvasGroup cg_FixedJoyStick;
    private Animator PlayerAnimator;

    protected PoolManager _poolManager => PoolManager.I;
    private void Awake()
    {
        ultimateLiberado = false;
        PlayerAnimator = GetComponent<Animator>();
    }

    private void OnValidate()
    {
        if(cg_FixedJoyStick != null)
        {
            cg_FixedJoyStick = PlayerMovement.I._joystick.GetComponent<CanvasGroup>();
        }
       
    }

    private void OnEnable()
    {
        PlayerAnimator.SetBool("Attack", true);
        cg_FixedJoyStick.DOFade(1, 0.4f);
    }

    private void OnDisable()
    {
        PlayerAnimator.SetBool("Attack", false);
        cg_FixedJoyStick.DOFade(0, 0.4f);
    }

    public virtual void Attack()
    {

    }

    #region Ultimate

    public virtual void EstadoUltimate(bool estado)
    {
        ultimateLiberado = estado;
    }

    public virtual void AtaqueUltimate()
    {
        // Programação do ataque de ultimate
    }

    public float GetMaxUltimatePoints()
    {
        return ultimateMaxPoints;
    }

    #endregion
}
