using UnityEngine;
using Utils.Singleton;

public class BackgroundController : Singleton<BackgroundController>
{
    [SerializeField] Parallax[] parallaxArray;

    private new void Awake()
    {
        
    }

    public void MudarEstadoParallax(bool estado)
    {
        foreach (Parallax p in parallaxArray)
        {
            p.MudarEstadoParallax(estado);
        }
    }
}

