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
                if (hit.transform?.gameObject.tag == buildTag)
                {
                    switch (buildTag)
                    {
                        case "Crossroads":
                            hit.transform.gameObject.GetComponentInParent<Crossroads>().BuildSettlement(gameController.GetPlayer());
                            building = false;
                            break;
                        case "Road":
                            hit.transform.gameObject.GetComponent<Road>().BuildRoad(gameController.GetPlayer());
                            building = false;
                            break;
                        default:
                            break;
                    }
                    
                }
            }
        }
    }

    public void BuildThis(string tag)
    {
        building = true;
        buildTag = tag;
    }
}
