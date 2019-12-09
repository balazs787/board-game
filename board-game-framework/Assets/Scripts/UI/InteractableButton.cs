using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableButton : MonoBehaviour
{
    public void SetInteractable(bool interactable)
    {
        gameObject.GetComponent<Image>().color = interactable ? Color.white : Color.gray;
        gameObject.GetComponentInChildren<Button>().interactable = interactable ? true : false;
    }
}
