using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] Parallax[] parallaxArray;

    public void MudarEstadoParallax(bool estado)
    {
        foreach(Parallax p in parallaxArray)
        {
            p.MudarEstadoParallax(estado);
        }
    }
}
