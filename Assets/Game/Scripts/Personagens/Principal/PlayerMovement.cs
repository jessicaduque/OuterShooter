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

    private new void Awake()
    {
        transform.position = posInicial;
        anim = GetComponent<Animator>();
    }

    public void Mover()
    {
        if (podeMover)
        {
            // Captura a Posição do Mouse
            Vector3 destino = Input.mousePosition;

            // Corrigir posição
            Vector3 desCorri = Camera.main.ScreenToWorldPoint(destino);

            // Destino final corrigido
            Vector3 dFinal = new Vector3(-8, Mathf.Clamp(desCorri.y, -3.8f, 3.8f), 0);

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


    public void PermitirMovimento(bool estado)
    {
        podeMover = estado;
    }

    public IEnumerator MoverParaX()
    {
        yield return new WaitForSeconds(0.4f);
        AnimateBool("Mover", true);
        Vector3 posFinal = new Vector3(-8, 0, 0);
        while (transform.position != posFinal)
        {
            transform.position = Vector3.MoveTowards(transform.position, posFinal, 1.2f * Time.deltaTime);
            yield return false;
        }
        PermitirMovimento(true);
        yield return true;
    }


}
