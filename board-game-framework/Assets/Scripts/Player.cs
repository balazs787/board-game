using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    [System.Serializable]
    public class Resources
    {
        public int brick;
        public int wool;
        public int ore;
        public int grain;
        public int lumber;
        
    }

    public void givePlayerResources(Resource resource, int amount)
    {

    }
}
