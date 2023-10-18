using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 posInicial = new Vector2(3.7f, 0);
    private float tempoMover;
    private Animator anim;
    
    private bool podeMover = true;

    void Awake()
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
            transform.position = Vector3.MoveTowards(transform.position, dFinal, 5f * Time.deltaTime);
        }

    }

    public void AnimatateBool(string nomeBool, bool state)
    {
        anim.SetBool(nomeBool, state);
    }

    public void PermitirMovimento()
    {
        podeMover = true;
    }

    public IEnumerator MoverParaX()
    {
        yield return new WaitForSeconds(2);
        Vector3 posFinal = new Vector3(-8, 0, 0);
        while (transform.position != posFinal)
        {
            transform.position = Vector3.MoveTowards(transform.position, posFinal, 1.2f * Time.deltaTime);
            yield return null;
        }
        podeMover = true;
    }

}
