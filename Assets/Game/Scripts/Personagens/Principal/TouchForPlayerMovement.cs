using UnityEngine;
using UnityEngine.UI;

public class TouchForPlayerMovement : Button
{
    PlayerMovement PlayerMovement;

    protected override void Awake()
    {
        base.Awake();

        PlayerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (IsPressed())
        {
            PlayerMovement.Mover();
        }
    }
}
