using UnityEngine;

public class Shot : MonoBehaviour
{
    protected Rigidbody2D Rb2D;
    protected GameObject Player;

    [SerializeField] protected float shotSpeed;
    [SerializeField] private int dano;
    [SerializeField] private Pool efeitoExplosao;

    [SerializeField] private bool shotPlayer;

    private PoolManager _poolManager => PoolManager.I;

    private void Awake()
    {
        Rb2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Rb2D.velocity = new Vector2((shotPlayer ? shotSpeed : -shotSpeed), 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            _poolManager.GetObject(efeitoExplosao.tagPool, transform.position, Quaternion.identity);
            _poolManager.ReturnPool(gameObject);

            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerController>().LevarDano();
            }
            else
            {
                other.gameObject.GetComponent<Enemy>().LevarDano(dano);
            }
        }
        
    }

}
