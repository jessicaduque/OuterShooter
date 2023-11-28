using UnityEngine;

public class PlayerAttackFire : PlayerAttack
{
    [SerializeField] private Pool prefabAtaqueNormal;
    [SerializeField] private Transform pontoSaida;
    //[SerializeField] private GameObject prefabAtaqueUltimate;

    public override void Attack()
    {
        if (enabled)
        {
            _poolManager.GetObject(prefabAtaqueNormal.tagPool, pontoSaida.position, Quaternion.identity);
        }
    }
}