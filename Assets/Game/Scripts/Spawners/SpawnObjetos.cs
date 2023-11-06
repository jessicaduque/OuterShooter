using System.Collections;
using UnityEngine;
using Utils.Singleton;

public class SpawnObjetos : Singleton<SpawnObjetos>
{
    [SerializeField] GameObject[] Objetos;

    [SerializeField] Transform[] pontosSpawn;

    [Space(20)]
    [Header("Tempo criação objetos")]
    [SerializeField] private float meuTempo;
    [SerializeField] private static float tempoCriacao = 20f;

    private void OnEnable()
    {
        meuTempo = 0.0f;
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

        GameObject Extra = Instantiate(Objetos[extraEscolhido], pontosSpawn[Random.Range(0, pontosSpawn.Length)].position, Quaternion.identity);
        
    }
}
