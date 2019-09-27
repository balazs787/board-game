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
                Debug.Log(hit.transform?.gameObject.name);
                Debug.Log(hit.transform?.gameObject.tag+" == "+buildTag);
                if (hit.transform?.gameObject.tag == buildTag)
                {
                    switch (buildTag)
                    {
                        case "Crossroads":
                            hit.transform.gameObject.GetComponent<Crossroads>().BuildSettlement(gameController.GetPlayer());
                            Debug.Log(gameController.GetPlayerName());
                            break;
                        case "Road":
                            hit.transform.gameObject.GetComponent<Road>().BuildRoad(gameController.GetPlayer());
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
