using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatanHexagon : HexagonField
{
    public int number;
    public Resource resource;
    public bool robber;
    public GameObject numberText;

    void Start()
    {
        
        TextMesh text = this.numberText.GetComponent<TextMesh>();
        text.text = number.ToString();
        switch (number)
        {
            case 8:
                text.color = Color.red;
                text.fontSize = 200;
                text.fontStyle = FontStyle.Bold;
                return;
            case 2:
                text.fontSize = 100;
                return;
            case 12:
                text.fontSize = 100;
                return;
            case 3:
                text.fontSize = 140;
                return;
            case 11:
                text.fontSize = 140;
                return;
            case 4:
                text.fontSize = 160;
                return;
            case 10:
                text.fontSize = 160;
                return;
            case 5:
                text.fontSize = 200;
                return;
            case 9:
                text.fontSize = 200;
                return;
            case 6:
                text.color = Color.red;
                text.fontSize = 200;
                text.fontStyle = FontStyle.Bold;
                return;
            
        }
    }

    public CatanHexagon(): base()
    {
        
    }


    override
    public void activate()
    {
        hexGiveResources();
    }

    public int getNumber()
    {
        return number;
    }

    public void hexGiveResources()
    {
        if (!robber)
        {
            vertexes.top.crossroadsGiveResources(resource);
            vertexes.topRight.crossroadsGiveResources(resource);
            vertexes.bottomRight.crossroadsGiveResources(resource);
            vertexes.bottom.crossroadsGiveResources(resource);
            vertexes.bottomLeft.crossroadsGiveResources(resource);
            vertexes.topLeft.crossroadsGiveResources(resource);
        }
    }

}
