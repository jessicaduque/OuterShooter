using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    protected float ultimatePoints;
    [SerializeField] protected float ultimateMaxPoints;
    protected bool ultimateLiberado;

    private Animator PlayerAnimator;

    protected PoolManager _poolManager => PoolManager.I;
    private void Awake()
    {
        ultimateLiberado = false;
        PlayerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerAnimator.SetBool("Attack", true);
    }

    private void OnDisable()
    {
        PlayerAnimator.SetBool("Attack", false);
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
