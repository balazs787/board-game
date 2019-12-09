using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayCardButton : InteractableButton
{
    public void Refresh(CatanPlayer player)
    {
        SetInteractable(player.cards.Count > 0);
    }
}
