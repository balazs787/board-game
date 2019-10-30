﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildTownButton : MonoBehaviour
{
    public void Refresh(CatanPlayer player)
    {
        SetInteractable(player.CanAfford(0, 0, 2, 0, 3) && player.towns < 4);
    }

    public void SetInteractable(bool interactable)
    {
        gameObject.GetComponent<Image>().color = interactable ? Color.white : Color.gray;
        gameObject.GetComponentInChildren<Button>().interactable = interactable ? true : false;
    }
}
