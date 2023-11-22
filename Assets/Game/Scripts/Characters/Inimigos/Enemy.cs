using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int enemyHealth;
    [SerializeField] protected int pointsToGive;
    [SerializeField] protected int energyToGive;
    [SerializeField] protected float[] timesShot;
    private float timeShot;

    [Header("Shot Variables")]
    [SerializeField] protected Pool ShotPrefab;
    [SerializeField] protected Transform FirePointMiddle;
    
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
        timeShot = timesShot[Random.Range(0, timesShot.Length)];
        StartCoroutine(Atirar());
    }

    private void OnDisable()
    {
        estaVivoEAtivo = false;
        StopAllCoroutines();
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

        _spawnManager.AumentarInimigosMortos();

        _uiController.AdicionarPontosUltimate(energyToGive);
        _scoreManager.AdicionarPontosScore(pointsToGive);

        _poolManager.ReturnPool(gameObject);
    }

    #endregion

    private IEnumerator Atirar()
    {
        if (thisSpriteRenderer.isVisible)
        {
            _poolManager.GetObject(ShotPrefab.tagPool, FirePointMiddle.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(timeShot);
        StartCoroutine(Atirar());
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
