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
    private AudioManager _audioManager => AudioManager.I;

    private BankManager _bankManager => BankManager.I;

    private PoolManager _poolManager => PoolManager.I;

    private void Awake()
    {
        thisCollider = GetComponent<BoxCollider2D>();
        thisRB = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // Colocar desejo para rotacionar infinitamente
        thisSequence = DOTween.Sequence();
        thisSequence.Append(transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.5f).SetLoops(2, LoopType.Yoyo));

        Sequence newSeq = DOTween.Sequence();
        newSeq.Append(transform.DOLocalRotate(new Vector3(0, 0, 20), 0.1f).SetLoops(2, LoopType.Yoyo));
        newSeq.Append(transform.DOLocalRotate(new Vector3(0, 0, -20), 0.1f).SetLoops(2, LoopType.Yoyo));
        newSeq.AppendInterval(0.3f);
        thisSequence.Join(newSeq);
        thisSequence.SetLoops(-1); 

        // Fazer aster�ideir para esquerda
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
            _audioManager.PlaySfx("Star");
            _bankManager.AdicionarEstrelas();
            _poolManager.ReturnPool(gameObject);
        }
    }

    public void MoveToPlayer()
    {
        Transform PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        thisRB.velocity = Vector2.zero;
        transform.DOMove(PlayerTransform.position, 2f);
    }
}
