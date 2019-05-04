using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonField : Field
{
    public GameObject hexagonField;

    [System.Serializable]
    public class Edges
    {
        public Road topRight;
        public Road right;
        public Road bottomRight;
        public Road bottomLeft;
        public Road left;
        public Road topLeft;
    }

    [System.Serializable]
    public class Vertexes
    {
        public Crossroads top;
        public Crossroads topRight;
        public Crossroads bottomRight;
        public Crossroads bottom;
        public Crossroads bottomLeft;
        public Crossroads topLeft;
    }

    public Edges edges;
    public Vertexes vertexes;

    override
    public void activate()
    {

    }

    public HexagonField() : base()
    {

    }

}
