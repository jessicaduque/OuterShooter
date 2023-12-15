using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected int inicialPrice;
    [SerializeField] protected Purchase typePurchase;
    [SerializeField] protected TextMeshProUGUI thisPriceText;
    protected bool available;

    protected virtual void Awake()
    {
        thisPriceText.text = inicialPrice.ToString();
    }

    protected abstract bool CheckAvailable();
    protected abstract void Purchased(bool state);

    private void ChangeAvailability(bool state)
    {
        available = state;
    }
}
