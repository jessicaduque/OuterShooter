using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int enemyHealth;
    [SerializeField] protected int pointsToGive;
    [SerializeField] protected int energyToGive;

    [Header("Shot Variables")]
    [SerializeField] protected Pool ShotPrefab;
    [SerializeField] protected Transform FirePointMiddle;
    [SerializeField] protected float waitTimeShot;
    
    protected SpriteRenderer thisSpriteRenderer;

    public bool estaVivoEAtivo;
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
        estaVivoEAtivo = true;
    }

    private void OnDisable()
    {
        estaVivoEAtivo = false;
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
        estaVivoEAtivo = false;

        _spawnManager.DiminuirInimigosVivos();

        _uiController.AdicionarPontosUltimate(energyToGive);
        _scoreManager.AdicionarPontosScore(pointsToGive);

        _poolManager.ReturnPool(gameObject);
    }

    #endregion

    public virtual void Atirar()
    {
        if (thisSpriteRenderer.isVisible)
        {
            _poolManager.GetObject(ShotPrefab.tagPool, FirePointMiddle.position, Quaternion.identity);
        }
    }

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
