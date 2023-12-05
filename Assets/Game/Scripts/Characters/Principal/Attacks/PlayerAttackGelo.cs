using UnityEngine;

public class PlayerAttackGelo : PlayerAttack
{
    [SerializeField] private Pool prefabAtaqueNormal;
    [SerializeField] private Transform pontoSaidaCima;
    [SerializeField] private Transform pontoSaidaMeio;
    [SerializeField] private Transform pontoSaidaBaixo;
    //[SerializeField] private GameObject prefabAtaqueUltimate;

    public override void Attack()
    {
        if (this.enabled)
        {
            _poolManager.GetObject(prefabAtaqueNormal.tagPool, pontoSaidaCima.position, Quaternion.Euler(0, 0, 12));
            _poolManager.GetObject(prefabAtaqueNormal.tagPool, pontoSaidaMeio.position, Quaternion.identity);
            _poolManager.GetObject(prefabAtaqueNormal.tagPool, pontoSaidaBaixo.position, Quaternion.Euler(0, 0, -12));
        }
    }
}