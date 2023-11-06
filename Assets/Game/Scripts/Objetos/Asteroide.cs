using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Asteroide : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private float[] velocidades;

    private SpriteRenderer thisRenderer;
    private BoxCollider2D thisCollider;
    private Rigidbody2D thisRB;

    private Tween thisTween;

    private PlayerController _playerController => PlayerController.I;

    private void Awake()
    {
        thisRenderer = GetComponent<SpriteRenderer>();
        thisCollider = GetComponent<BoxCollider2D>();
        thisRB = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        // Reset sprite de asteroide
        thisRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

        // Reset tamanho de collider
        Vector3 tam = thisRenderer.bounds.size;
        thisCollider.size = tam;

        // Colocar asteróide para rotacionar infinitamente
        thisTween = transform.DOLocalRotate(new Vector3(0, 0, 360), 4, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);

        // Fazer asteróideir para esquerda
        thisRB.velocity = new Vector2(-velocidades[Random.Range(0, velocidades.Length)], 0);
    }

    private void OnDisable()
    {
        thisTween?.Kill();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            thisRB.velocity = Vector3.zero;
            _playerController.LevarDano();
            thisTween.Kill();
            StartCoroutine(Disable());
        }
    }
    private IEnumerator Disable()
    {
        yield return new WaitForSecondsRealtime(1);

        gameObject.SetActive(false);
    }

}
