using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    protected float waitTimeShot;
    protected float waitLimitShot;
    protected bool podeAtacar;

    protected float ultimatePoints;
    protected float ultimateMaxPoints;
    protected bool ultimateLiberado;

    protected PoderDetails poder;

    private Animator PlayerAnimator;

    private void Awake()
    {
        ultimateLiberado = false;
        PlayerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerAnimator.SetBool("Atacando", true);
    }

    private void OnDisable()
    {
        PlayerAnimator.SetBool("Atacando", false);
    }

    protected virtual void AtaqueNormal()
    {
        waitTimeShot += Time.deltaTime;
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
