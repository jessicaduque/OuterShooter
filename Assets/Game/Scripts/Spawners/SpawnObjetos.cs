using System.Collections;
using UnityEngine;
using Utils.Singleton;

public class SpawnObjetos : Singleton<SpawnObjetos>
{
    [SerializeField] Pool[] Objetos;

    [SerializeField] Transform[] pontosSpawn;

    [Space(20)]
    [Header("Tempo criação objetos")]
    private float meuTempo;
    [SerializeField] private float tempoCriacao = 10f;

    private PoolManager _poolManager => PoolManager.I;

    private new void Awake()
    {
        
    }

    private void OnEnable()
    {
        meuTempo = 0.0f;
        StartCoroutine(GerarObjetos());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator GerarObjetos()
    {
        meuTempo = 0.0f;

        while (meuTempo < tempoCriacao)
        {
            meuTempo += Time.deltaTime;
            yield return null;
        }

        InstanciarObjeto();
        StartCoroutine(GerarObjetos());
    }

    void InstanciarObjeto()
    {
        int extraEscolhido = 0;
        int tipoExtra = Random.Range(0, 10);
        if (tipoExtra < 6)
        {
            extraEscolhido = 0;
        }
        else
        {
            extraEscolhido = 1;
        }

        _poolManager.GetObject(Objetos[extraEscolhido].tagPool, pontosSpawn[Random.Range(0, pontosSpawn.Length)].position, Quaternion.identity);
    }
}
