using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationWindow : MonoBehaviour
{
    private TextMeshProUGUI _textMesh;
    private void Start()
    {
        _textMesh = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }
    public void Rob(CatanPlayer player)
    {
        gameObject.SetActive(true);
        _textMesh.text = "Place robber";
        _textMesh.color = player.color;
    }

    public void Steal(CatanPlayer player)
    {
        gameObject.SetActive(true);
        _textMesh.text = "Pick opposing settlement to steal from";
        _textMesh.color = player.color;
    }

    public void FreeBuild(CatanPlayer player)
    {
        gameObject.SetActive(true);
        _textMesh.text = "Place free settlement and road";
        _textMesh.color = player.color;
    }

    public void Hide(bool hide)
    {
        gameObject.SetActive(!hide);
    }
}
