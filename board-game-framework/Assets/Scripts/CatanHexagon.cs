using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatanHexagon : HexagonField
{
    public int number;
    public Resource resource;
    public bool beingRobbed;
    public GameObject numberText;
    public Robber robber;

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
    public void Activate()
    {
        HexGiveResources();
    }

    public int getNumber()
    {
        return number;
    }

    public bool PlaceRobberHere(Player player)
    {
        if (beingRobbed)
        {
            return false;
        }

        beingRobbed = true;
        robber.gameObject.transform.position = gameObject.transform.position;
        robber.CanSteal(EnemyTowns(player));
        return true;
    }

    public bool EnemyTowns(Player player)
    {
        return (vertexes.top.GetPlayer() != player && vertexes.top.GetPlayer() != null) ||
                (vertexes.topRight.GetPlayer() != player && vertexes.topRight.GetPlayer() != null) ||
                (vertexes.bottomRight.GetPlayer() != player && vertexes.bottomRight.GetPlayer() != null) ||
                (vertexes.bottom.GetPlayer() != player && vertexes.bottom.GetPlayer() != null) ||
                (vertexes.bottomLeft.GetPlayer() != player && vertexes.bottomLeft.GetPlayer() != null) ||
                (vertexes.topLeft.GetPlayer() != player && vertexes.topLeft.GetPlayer() != null);
    }

    public void HexGiveResources()
    {
        if (!beingRobbed)
        {
            vertexes.top.CrossroadsGiveResources(resource);
            vertexes.topRight.CrossroadsGiveResources(resource);
            vertexes.bottomRight.CrossroadsGiveResources(resource);
            vertexes.bottom.CrossroadsGiveResources(resource);
            vertexes.bottomLeft.CrossroadsGiveResources(resource);
            vertexes.topLeft.CrossroadsGiveResources(resource);
        }
    }
}
