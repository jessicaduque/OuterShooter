using System.Collections;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float comprimentoX;
    private float PosAtualX;
    private Transform cam;
    [SerializeField] private float tempoParalaxe;
    private float auxTimeParallax = 0f;
    [SerializeField] private bool parallaxRodando = false;

    void Awake()
    {
        comprimentoX = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = Camera.main.transform;
        PosAtualX = transform.position.x;
    }

    void MovimentoParalaxe(float tp)
    {
        PosAtualX -= tp * Time.deltaTime;
        float rePos = cam.transform.position.x * (1 - tp);
        float distancia = cam.transform.position.x * tp;

        transform.position = new Vector3(PosAtualX + distancia, transform.position.y, transform.position.z);

        if (rePos > PosAtualX + comprimentoX)
        {
            PosAtualX += comprimentoX;
        }
        else if (rePos < PosAtualX - comprimentoX)
        {
            PosAtualX -= comprimentoX;
        }
    }

    public void MudarEstadoParallax(bool estado)
    {
        parallaxRodando = estado;
        StopAllCoroutines();
        if (parallaxRodando)
        {
            StartCoroutine(LigarParallax());
        }
        else
        {
            StartCoroutine(DesligarParallax());
        }
    }

    IEnumerator LigarParallax()
    {
        while (auxTimeParallax < tempoParalaxe)
        {
            MovimentoParalaxe(auxTimeParallax);
            auxTimeParallax += Time.deltaTime;
            yield return null;
        }

        while (true)
        {
            MovimentoParalaxe(tempoParalaxe);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator DesligarParallax()
    {
        while (auxTimeParallax > 0)
        {
            MovimentoParalaxe(auxTimeParallax);
            auxTimeParallax -= Time.deltaTime;
            yield return null;
        }
    }
}
