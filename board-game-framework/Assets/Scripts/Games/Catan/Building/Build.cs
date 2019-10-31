using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Build : MonoBehaviour
{
    public CatanGameController gameController;
    public ClickedItem clickedItem;
    public GameObject cancelBuildingButton;
    private bool _building = false;
    string buildTag;
    // Start is called before the first frame update
    void Start()
    {
        clickedItem.SendClickedItem += gameObj => TryBuild(gameObj);
    }


    public void TryBuild(GameObject gameObj)
    {
        if (!_building)
        {
            return;
        }

        CatanPlayer currentPlayer = (CatanPlayer)gameController.GetPlayer();

        if (buildTag == "Settlement" && gameObj.tag == "Crossroads")
        {
            _building = !gameObj.GetComponentInParent<Crossroads>().BuildSettlement(currentPlayer);
            if (!_building)
            {
                gameController.SettlementBuiltAction?.Invoke();
            }
        }
        else

        if (buildTag == "Town" && (gameObj.tag == "Crossroads" || gameObj.tag == "Settlement"))
        {
            _building = !gameObj.GetComponentInParent<Crossroads>().UpgradeSettlement(currentPlayer);
            if (!_building)
            {
                gameController.TownBuiltAction?.Invoke();
            }
        }
        else

        if (buildTag == "Road" && gameObj.tag == "Road")
        {
            _building = !gameObj.GetComponentInParent<Road>().BuildRoad(currentPlayer);
            if (!_building)
            {
                gameController.RoadBuiltAction?.Invoke();
            }
        }

        cancelBuildingButton.SetActive(_building && !gameController.freeBuildPhase);
        gameController.interfacePanel.Refresh(currentPlayer);
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
