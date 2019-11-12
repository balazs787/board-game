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
    public Action<GameObject> AiBuilding;
    
    void Start()
    {
        clickedItem.SendClickedItem += gameObj => TryBuild(gameObj);
        AiBuilding += gameObj => TryBuild(gameObj);
    }


    public void TryBuild(GameObject clickedGameObject)
    {
        if (!_building)
        {
            return;
        }

        CatanPlayer currentPlayer = (CatanPlayer)gameController.GetPlayer();

        if (buildTag == "Settlement" && clickedGameObject.tag == "Crossroads")
        {
            _building = !clickedGameObject.GetComponentInParent<Crossroads>().BuildSettlement(currentPlayer);
            if (!_building && !gameController.GetPlayer().Ai)
            {
                gameController.SettlementBuiltAction?.Invoke();
            }
        }

        if (buildTag == "Town" && (clickedGameObject.tag== "Crossroads" || clickedGameObject.tag== "Settlement"))
        {
            _building = !clickedGameObject.GetComponentInParent<Crossroads>().UpgradeSettlement(currentPlayer);
            if (!_building && !gameController.GetPlayer().Ai)
            {
                gameController.TownBuiltAction?.Invoke();
            }
        }

        if (buildTag == "Road" && clickedGameObject.tag=="Road")
        {
            _building = !clickedGameObject.GetComponentInParent<Road>().BuildRoad(currentPlayer);
            if (!_building && !gameController.GetPlayer().Ai)
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

        if (gameController.GetPlayer().Ai)
        {
            AiTryBuild();
            return;
        }

        if (!gameController.freeBuildPhase)
            cancelBuildingButton.SetActive(true);
    }

    public void AiTryBuild()
    {
        if (buildTag == "Road")
        {
            List<Road> roads = new List<Road>(gameController.hexmap.roads);

            while(_building && roads.Count > 0)
            {
                int random = UnityEngine.Random.Range(0, roads.Count);
                TryBuild(roads[random].gameObject);
                roads.RemoveAt(random);
            }
            var b = _building;
            _building = false;
            cancelBuildingButton.SetActive(false);
            if (!b)
            { 
                gameController.RoadBuiltAction?.Invoke();
            }
            return;
        }

        if (buildTag == "Settlement" || buildTag == "Town")
        {
            List<Crossroads> crossroads = new List<Crossroads>(gameController.hexmap.crossroads);

            while (_building && crossroads.Count > 0)
            {
                int random = UnityEngine.Random.Range(0, crossroads.Count);
                if (gameController.freeBuildPhase)
                {
                    crossroads[random].hex1.TryGetComponent(out CatanHexagon h1);
                    crossroads[random].hex2.TryGetComponent(out CatanHexagon h2);
                    crossroads[random].hex3.TryGetComponent(out CatanHexagon h3);

                    if ((h1 != null && h2 != null && h3 != null) &&
                        (h1.resource != Resource.none && h2.resource != Resource.none && h3.resource != Resource.none))
                    {
                        TryBuild(crossroads[random].gameObject);
                    }
                }
                else
                {
                    TryBuild(crossroads[random].gameObject);
                }
                crossroads.RemoveAt(random);
            }
            var b = _building;
            _building = false;
            cancelBuildingButton.SetActive(false);
            if (!b)
            {
                gameController.SettlementBuiltAction?.Invoke();
            }
            return;
        }
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
