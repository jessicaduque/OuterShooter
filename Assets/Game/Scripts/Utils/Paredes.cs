using UnityEngine;

public class Paredes : MonoBehaviour
{
    private PoolManager _poolManager => PoolManager.I;
    private SpawnManager _spawnManager => SpawnManager.I;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (!collision.GetComponent<Enemy>().estaVivoEAtivo)
            {
                _spawnManager.AumentarInimigosEscondidos();
            }
        }
        _poolManager.ReturnPool(collision.gameObject);
    }
}
