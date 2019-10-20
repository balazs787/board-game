using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GameController gameController;
    public GameObject cancelBuildingButton;
    private bool _building = false;
    string buildTag;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_building && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            

            if (Physics.Raycast(ray, out hit, 100f))
            {
                //Debug.Log(hit.transform?.gameObject.name);
                Player currentPlayer = gameController.GetPlayer();

                if (buildTag == "Settlement" && hit.transform?.gameObject.tag == "Crossroads")
                {
                    _building = !hit.transform.gameObject.GetComponentInParent<Crossroads>().BuildSettlement(currentPlayer);
                }else

                if(buildTag == "Town" && (hit.transform?.gameObject.tag == "Crossroads" || hit.transform?.gameObject.tag == "Settlement"))
                {
                    _building = !hit.transform.gameObject.GetComponentInParent<Crossroads>().UpgradeSettlement(currentPlayer);
                }else

                if (buildTag == "Road" && hit.transform?.gameObject.tag == "Road")
                {
                    _building = !hit.transform.gameObject.GetComponentInParent<Road>().BuildRoad(currentPlayer);
                    if (!_building)
                    { 
                        gameController.RoadBuilt?.Invoke();
                    }
                }

                cancelBuildingButton.SetActive(_building && !gameController.freeBuildPhase);
                gameController.interfacePanel.Refresh(currentPlayer);
            }
        }
    }

    public void BuildThis(string tag)
    {
        if (_building)
            return;

        _building = true;
        buildTag = tag;

        if (!gameController.freeBuildPhase)
            cancelBuildingButton.SetActive(true);
    }

    public bool GetBuilding()
    {
        return _building;
    }

    public void CancelBuilding()
    {
        _building = false;
        cancelBuildingButton.SetActive(false);
    }
}
