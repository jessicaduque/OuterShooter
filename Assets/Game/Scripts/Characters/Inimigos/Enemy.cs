using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected int enemyHealth;
    [SerializeField] protected float waitLimitShot;
    [SerializeField] protected Pool ShotPrefab;
    [SerializeField] protected Pool efeitoExplosao;
    [SerializeField] protected int pointsToGive;
    [SerializeField] protected int energyToGive;

    [Header("Shot Variables")]
    [SerializeField] protected Transform FirePointMiddle;

    protected float waitTimeShot = 0f;
    protected SpriteRenderer thisSpriteRenderer;

    public bool estaVivo;
    private UIController _uiController => UIController.I;
    private ScoreManager _scoreManager => ScoreManager.I;
    private PlayerController _playerController => PlayerController.I;

    private PoolManager _poolManager=> PoolManager.I;
    private SpawnManager _spawnManager => SpawnManager.I;
    private void Awake()
    {
        thisSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        estaVivo = true;
    }

    private void OnDisable()
    {
        estaVivo = false;
    }

    #region Levar Dano

    public void LevarDano(int dano)
    {
        if (thisSpriteRenderer.isVisible)
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
        estaVivo = false;

        _spawnManager.DiminuirInimigosVivos();

        _poolManager.GetObject(efeitoExplosao.tagPool, transform.position, Quaternion.identity);

        _uiController.AdicionarPontosUltimate(energyToGive);
        _scoreManager.AdicionarPontosScore(pointsToGive);

        _poolManager.ReturnPool(gameObject);
    }

    #endregion

    //public virtual void Atirar(Transform PontoSaida)
    //{
    //    if (visible)
    //    {
    //        waitTimeShot += Time.deltaTime;

    //        if (waitTimeShot > waitLimitShot)
    //        {
    //            GameObject tiro = Instantiate(ShotPrefab, PontoSaida.position, PontoSaida.rotation);

    //            waitTimeShot = 0f;

    //        }
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vibration.Vibrate();
            _playerController.LevarDano();
            Morrer();
        }
    }

}
