using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSettlement : MonoBehaviour
{
    public MeshRenderer base1;
    public MeshRenderer roof;

    public Material[] materials;


    public void Setup(int playerId)
    {
        base1.material = materials[playerId];
        roof.material = materials[playerId];
    }
}
