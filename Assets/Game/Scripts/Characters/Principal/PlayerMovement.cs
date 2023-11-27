using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singleton;

public class PlayerMovement : Singleton<PlayerMovement>
{
    [SerializeField] private Vector2 posInicial;
    private float tempoMover = 5f;
    private Animator anim;
    
    private bool podeMover = false;

    private LevelController _levelController => LevelController.I;
    private BackgroundController _backgroundController => BackgroundController.I;
    private new void Awake()
    {
        transform.position = posInicial;
        anim = GetComponent<Animator>();
    }

    public void Mover()
    {
        if (podeMover)
        {
            // Captura a Posi��o do Mouse
            Vector3 destino = Input.mousePosition;

            // Corrigir posi��o
            Vector3 desCorri = Camera.main.ScreenToWorldPoint(destino);

            // Destino final corrigido
            Vector3 dFinal = new Vector3(desCorri.x + 1f, Mathf.Clamp(desCorri.y, -3.8f, 3.8f), 0);

            // Mover objeto
            transform.position = Vector3.MoveTowards(transform.position, dFinal, tempoMover * Time.deltaTime);
        }

    }

    public void AnimateBool(string nomeBool, bool state)
    {
        anim.SetBool(nomeBool, state);
    }

    public void AnimateTrigger(string nomeTrigger)
    {
        anim.SetTrigger(nomeTrigger);
    }

    public void SetAnimatorUnscaled(bool estado) 
    {
        anim.updateMode = (estado ? AnimatorUpdateMode.UnscaledTime : AnimatorUpdateMode.Normal);
    }
    public void PermitirMovimento(bool estado)
    {
        podeMover = estado;
    }

    public IEnumerator MoverParaX()
    {
        AnimateBool("Mover", true);
        yield return new WaitForSeconds(0.8f);
        transform.DOMoveX(-7.2f, 4f).SetEase(Ease.InSine);
        while (transform.position.x != -7.2f)
        {
            yield return null;
        }
        PermitirMovimento(true);
    }

    public IEnumerator MoverParaMeio()
    {
        PermitirMovimento(false);
        AnimateBool("Mover", true);
        transform.DOMove(new Vector2(-7.2f, 0), 3f).SetEase(Ease.InSine);
        while (transform.position.x != -7.2f)
        {
            yield return null;
        }
        _backgroundController.MudarEstadoParallax(false);
    }


}
