using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareField : Field
{
    [System.Serializable]
    public class Edges
    {
        public GameObject top;
        public GameObject right;
        public GameObject bottom;
        public GameObject left;
    }

    [System.Serializable]
    public class Vertexes
    {
        public GameObject topRight;
        public GameObject bottomRight;
        public GameObject bottomLeft;
        public GameObject topLeft;
    }

    public Edges edges;
    public Vertexes vertexes;

    public int number;

    override
    public void Activate()
    {

    }

    public SquareField() : base()
    {

    }
}
