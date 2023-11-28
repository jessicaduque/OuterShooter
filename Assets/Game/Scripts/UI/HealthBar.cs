using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private GameObject inimigo;
    private RectTransform rt;
    private SpriteRenderer spriteRenderer;

    private void OnValidate()
    {
        rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (inimigo != null)
        {
            transform.position = new Vector3(inimigo.transform.position.x, spriteRenderer.bounds.max.y, 0);
        } 
    }

    private void OnDisable()
    {
        inimigo = null;
    }

    public void SetInimigo(GameObject inimigo)
    {
        this.inimigo = inimigo;
        spriteRenderer = inimigo.GetComponent<SpriteRenderer>();
    }
}
