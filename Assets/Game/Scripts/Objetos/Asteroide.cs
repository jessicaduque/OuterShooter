using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Asteroide : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;

    [SerializeField] private float[] velocidades;

    private SpriteRenderer thisRenderer;
    private BoxCollider2D thisCollider;
    private Rigidbody2D thisRB;

    [SerializeField] private GameObject explosaoGameObject;

    private Tween thisTween;

    private PlayerController _playerController => PlayerController.I;

    private PoolManager _poolManager => PoolManager.I;
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

        int rotationSpeed = Random.Range(2, 4);

        // Colocar asteróide para rotacionar infinitamente
        thisTween = transform.DOLocalRotate(new Vector3(0, 0, (rotationSpeed == 3 ? -360 : 360)), rotationSpeed, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);

        // Fazer asteróideir para esquerda
        thisRB.velocity = new Vector2(-velocidades[Random.Range(0, velocidades.Length)], 0);
    }

    private void OnDisable()
    {
        thisTween?.Kill();
    }

    public void Explode()
    {
        if (thisRenderer.isVisible)
        {
            Vibration.Vibrate();
            _poolManager.GetObject("ExplosionNormal", transform.position, transform.rotation);
            _poolManager.ReturnPool(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            Vibration.Vibrate();
            thisRB.velocity = Vector3.zero;
            _playerController.LevarDano();
            _poolManager.GetObject("ExplosionNormal", transform.position, transform.rotation);
            _poolManager.ReturnPool(gameObject);
        }
    }

}
