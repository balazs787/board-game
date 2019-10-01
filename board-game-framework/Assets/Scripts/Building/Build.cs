using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GameController gameController;
    bool building = false;
    string buildTag;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (building && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            

            if (Physics.Raycast(ray, out hit, 100f))
            {
                //Debug.Log(hit.transform?.gameObject.name);
                Player currentPlayer = gameController.GetPlayer();

                if (buildTag == "Settlement" && hit.transform?.gameObject.tag == "Crossroads")
                {
                    building = !hit.transform.gameObject.GetComponentInParent<Crossroads>().BuildSettlement(currentPlayer);
                }else

                if(buildTag == "Town" && (hit.transform?.gameObject.tag == "Crossroads" || hit.transform?.gameObject.tag == "Settlement"))
                {
                    building = !hit.transform.gameObject.GetComponentInParent<Crossroads>().UpgradeSettlement(currentPlayer);
                }else

                if (buildTag == "Road" && hit.transform?.gameObject.tag == "Road")
                {
                    building = !hit.transform.gameObject.GetComponentInParent<Road>().BuildRoad(currentPlayer);
                }

                gameController.Refresh();
            }
        }
    }

    public void BuildThis(string tag)
    {
        if (building)
            return;

        building = true;
        buildTag = tag;
    }

    public bool GetBuilding()
    {
        return building;
    }
}
