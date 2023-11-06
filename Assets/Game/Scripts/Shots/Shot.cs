using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    protected Rigidbody2D Rb2D;
    protected GameObject Player;

    [SerializeField] protected float shotSpeed;
    [SerializeField] private int dano;
    [SerializeField] private GameObject efeitoExplosao;


    private void Awake()
    {
        Rb2D = GetComponent<Rigidbody2D>();
        Rb2D.velocity = new Vector2(0, shotSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().LevarDano();
        }
        else if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().LevarDano(dano);
        }

        GameObject explosao = Instantiate(efeitoExplosao, other.gameObject.transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }

}
