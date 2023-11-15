using UnityEngine;

public class Paredes : MonoBehaviour
{
    private PoolManager _poolManager => PoolManager.I;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _poolManager.ReturnPool(collision.gameObject);
    }
}
