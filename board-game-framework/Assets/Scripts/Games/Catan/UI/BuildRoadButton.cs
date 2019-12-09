using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildRoadButton : InteractableButton
{
    public void Refresh(CatanPlayer player)
    {
        SetInteractable(player.CanAfford(1, 1, 0, 0, 0) && player.roads < 15);
    }
}
