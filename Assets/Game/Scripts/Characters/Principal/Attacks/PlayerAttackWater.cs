using UnityEngine;

public class PlayerAttackWater : PlayerAttack
{
    [SerializeField] private Pool prefabAtaqueNormal;
    [SerializeField] private Transform pontoSaida;
    //[SerializeField] private GameObject prefabAtaqueUltimate;

    public override void Attack()
    {
        _poolManager.GetObject(prefabAtaqueNormal.tagPool, pontoSaida.position, Quaternion.identity);
    }
}
