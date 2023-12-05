using UnityEngine;

public class Shot : MonoBehaviour
{
    protected Rigidbody2D Rb2D;

    [SerializeField] protected float shotSpeed;
    [SerializeField] private float dano;
    [SerializeField] public Pool efeitoExplosao;
    private SpriteRenderer thisRenderer;
    [SerializeField] public bool shotPlayer;
    [SerializeField] private PoderDetails bomContra;

    private LevelController _levelController => LevelController.I;
    private PoolManager _poolManager => PoolManager.I;

    protected void Awake()
    {
        Rb2D = GetComponent<Rigidbody2D>();
        thisRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        SetVelocity();
    }

    protected virtual void SetVelocity()
    {
        Rb2D.velocity = (shotPlayer ? shotSpeed : -shotSpeed) * transform.right;
    }

    public virtual void Explodir()
    {
        if (thisRenderer.isVisible)
        {
            _poolManager.GetObject(efeitoExplosao.tagPool, transform.position, Quaternion.identity);
        }
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
                if(bomContra == _levelController.GetFaseAtual().fasePoder)
                {
                    other.gameObject.GetComponent<Enemy>().LevarDano(dano + 1);
                }
                else
                {
                    other.gameObject.GetComponent<Enemy>().LevarDano(dano);
                }
                
            }
        }
        
    }

}
