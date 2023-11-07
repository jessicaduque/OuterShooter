using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Desejo : MonoBehaviour
{
    private BoxCollider2D thisCollider;
    private Rigidbody2D thisRB;

    private Sequence thisSequence;

    [SerializeField] private float[] velocidades;

    private BankManager _bankManager => BankManager.I;

    private void Awake()
    {
        thisCollider = GetComponent<BoxCollider2D>();
        thisRB = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // Colocar desejo para rotacionar infinitamente
        thisSequence = DOTween.Sequence();
        thisSequence.Append(transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 1f).SetLoops(-1, LoopType.Yoyo));
        thisSequence.Join(transform.DOLocalRotate(new Vector3(0, 0, 20), 0.5f).SetLoops(-1, LoopType.Yoyo));
        thisSequence.Append(transform.DOLocalRotate(new Vector3(0, 0, -20), 0.5f).SetLoops(-1, LoopType.Yoyo));

        // Fazer asteróideir para esquerda
        thisRB.velocity = new Vector2(-velocidades[Random.Range(0, velocidades.Length)], 0);
    }

    private void OnDisable()
    {
        thisSequence?.Kill();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _bankManager.AdicionarEstrelas();

            gameObject.SetActive(false);
        }
    }
}
