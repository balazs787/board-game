using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildTownButton : InteractableButton
{
    public void Refresh(CatanPlayer player)
    {
        SetInteractable(player.CanAfford(0, 0, 2, 0, 3) && player.towns < 4);
    }
}
