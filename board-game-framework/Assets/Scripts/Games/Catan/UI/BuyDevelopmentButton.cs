using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyDevelopmentButton : InteractableButton
{
    public bool outOfCards;
    public void Refresh(CatanPlayer player)
    {
        SetInteractable(player.CanAfford(0, 0, 1, 1, 1) && !outOfCards);
    }
}
