using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyDevelopmentButton : MonoBehaviour
{
    public bool outOfCards;
    public void Refresh(Player player)
    {
        SetInteractable(player.CanAfford(0, 0, 1, 1, 1) && !outOfCards);
    }

    public void SetInteractable(bool interactable)
    {
        gameObject.GetComponent<Image>().color = interactable? Color.white : Color.gray;
        gameObject.GetComponentInChildren<Button>().interactable = interactable ? true : false;
    }
}
