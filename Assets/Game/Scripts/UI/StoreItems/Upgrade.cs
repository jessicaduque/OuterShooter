using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : Item
{
    private const int maxLevels = 5;
    private int currentLevel = 0;
    private int currentPrice;

    protected override void Awake()
    {
        base.Awake();

        currentPrice = inicialPrice;
    }

    protected override bool CheckAvailable()
    {
        if(currentLevel >= maxLevels)
        {
            available = false;
        }
        return available;
    }

    protected override void Purchased(bool state)
    {
        RisePrice();
    }

    private void RisePrice()
    {
        currentPrice = (currentLevel == 0 ? inicialPrice : ((int)((currentLevel) * inicialPrice) / 2) + currentPrice);
    }
}
