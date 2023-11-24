using UnityEngine;

public class Shot : MonoBehaviour
{
    protected Rigidbody2D Rb2D;

    [SerializeField] protected float shotSpeed;
    [SerializeField] private int dano;
    [SerializeField] private Pool efeitoExplosao;

    [SerializeField] public bool shotPlayer;

    private PoolManager _poolManager => PoolManager.I;

    protected void Awake()
    {
        Rb2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        SetVelocity();
    }

    protected virtual void SetVelocity()
    {
        Rb2D.velocity = new Vector2((shotPlayer ? shotSpeed : -shotSpeed), 0);
    }

    public void Explodir()
    {
        _poolManager.GetObject(efeitoExplosao.tagPool, transform.position, Quaternion.identity);
        _poolManager.ReturnPool(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            Explodir();

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
