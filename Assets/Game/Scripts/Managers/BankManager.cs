using System;
using UnityEngine;
using Utils.Singleton;

public class BankManager : Singleton<BankManager>
{
    public event Action AumentouEstrelas;
    private int estrelas = 0;

    private int maxEstrelas = 99;

    [Header("Compras possíveis")]
    private float danoAMaisPorcNormal = 0f;
    //private float danoAMaisPorcUlt = 0f;
    private float chanceAtaqueSeguir = 0f;
    private float vidaMaisPorcNormal = 0f;

    private bool podeReviver = false;
    private bool barreiraContraMeteoros = false;
    private bool maiorChanceAsEstre = false;

    private new void Awake()
    {
        
    }

    private bool Comprar(int valor)
    {
        if (estrelas >= valor)
        {
            estrelas -= valor;
            return true;
        }
        else
        {
            return false;
        }

    }

    #region Public Comprar

    public bool ComprarMelhorAtaqueNormal(int valor)
    {
        if (Comprar(valor))
        {
            danoAMaisPorcNormal += 20;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ComprarMelhorUltimate(int valor)
    {
        if (Comprar(valor))
        {
            danoAMaisPorcUlt += 20;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ComprarChancesAtaqueSeguir(int valor)
    {
        if (Comprar(valor))
        {
            chanceAtaqueSeguir += 10;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ComprarReviver(int valor)
    {
        if (Comprar(valor))
        {
            podeReviver = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ComprarBarreiraMeteoros(int valor)
    {
        if (Comprar(valor))
        {
            barreiraContraMeteoros = true;
            return true;
        }
        else
        {
            return false;
        }
    }


    #endregion

    #region SET

    public void AdicionarEstrelas(int estrelas = 1)
    {
        this.estrelas += estrelas;
        if(this.estrelas > maxEstrelas)
        {
            this.estrelas = maxEstrelas;
        }
        AumentouEstrelas?.Invoke();
    }

    public void SetResetarReviver()
    {
        podeReviver = false;
    }

    public void SetResetarMeteoros()
    {
        barreiraContraMeteoros = false;
    }

    public void SetResetarChancesAsEst()
    {
        maiorChanceAsEstre = false;
    }

    #endregion

    #region GET

    public int GetQuantEstrelas()
    {
        return estrelas;
    }

    private float GetInformarDanoNormalAMais()
    {
        return danoAMaisPorcNormal;
    }

    private float GetInformarDanoUltAMais()
    {
        return danoAMaisPorcUlt;
    }

    private float GetChanceAtaqueSeguir()
    {
        return chanceAtaqueSeguir;
    }

    #endregion

}
