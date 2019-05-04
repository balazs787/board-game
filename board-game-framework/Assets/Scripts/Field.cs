using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Field : MonoBehaviour
{
    public static int id;

    public abstract void activate();

    public Field()
    {
        id = id++;
    }
}
