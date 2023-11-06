using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int enemyHealth;
    [SerializeField] protected GameObject efeitoExplosao;
    [SerializeField] protected float waitLimitShot;
    [SerializeField] protected GameObject ShotPrefab;
    [SerializeField] protected GameObject Player;
    [SerializeField] protected int pointsToGive;
    [SerializeField] protected int energyToGive;

    [Header("Shot Variables")]
    [SerializeField] protected Transform FirePointMiddle;

    protected float waitTimeShot = 0f;
    bool visible;

    private UIController _uiController => UIController.I;
    private ScoreManager _scoreManager => ScoreManager.I;
    private PlayerController _playerController => PlayerController.I;

    protected void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    protected void Update()
    {
        visible = GetComponentInChildren<SpriteRenderer>().isVisible;
        Atirar(FirePointMiddle);
    }

    public void LevarDano(int dano)
    {
        if (visible)
        {
            enemyHealth -= dano;
            if (enemyHealth <= 0)
            {
                Morrer();
            }
        }
    }

    public void Morrer()
    {
        Instantiate(efeitoExplosao, transform.position, Quaternion.identity);

        _uiController.AdicionarPontosUltimate(energyToGive);
        _scoreManager.AdicionarPontosScore(pointsToGive);

        Destroy(this.gameObject);
    }

    public virtual void Atirar(Transform PontoSaida)
    {
        if (visible)
        {
            waitTimeShot += Time.deltaTime;

            if (waitTimeShot > waitLimitShot)
            {
                GameObject tiro = Instantiate(ShotPrefab, PontoSaida.position, PontoSaida.rotation);

                waitTimeShot = 0f;

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Player)
        {
            _playerController.LevarDano();
        }
    }

}
