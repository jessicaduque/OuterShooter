using System.Collections;
using UnityEngine;

public class SpawnObjetos : MonoBehaviour
{
    [SerializeField] GameObject[] Objetos;

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
        float posY = Random.Range(-4.2f, 4.3f);
        Vector2 novaPos = new Vector2(transform.position.x, posY);
        int extraEscolhido = 0;
        int tipoExtra = Random.Range(0, 101);
        if (tipoExtra < 60)
        {
            extraEscolhido = 0;
        }
        else
        {
            extraEscolhido = 1;
        }
        GameObject Extra = Instantiate(Objetos[extraEscolhido], novaPos, Quaternion.identity);
        Destroy(Extra, 10f);
        meuTempo = 0;
        
    }
}
