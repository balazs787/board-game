using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildSettlementButton : InteractableButton
{
    public void Refresh(CatanPlayer player)
    {
        SetInteractable(player.CanAfford(1, 1, 1, 1, 0) && player.settlements < 5);
    }
}
