using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatanHexagon : HexagonField
{
    public int number;
    public Resource resource;
    public bool robber;

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
