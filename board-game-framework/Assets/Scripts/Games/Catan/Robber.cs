using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robber : MonoBehaviour
{
    private bool _placingRobber;
    private bool _stealing;
    private bool _canSteal;
    public CatanGameController gameController;
    public ClickedItem clickedItem;
    public CatanHexagon currentHex;

    private void Start()
    {
        clickedItem.SendClickedItem += clickedGameObject => TryPlaceRobber(clickedGameObject);
        clickedItem.SendClickedItem += clickedGameObject => TrySteal(clickedGameObject);
        gameObject.transform.position = currentHex.transform.position;
    }

    public void TryPlaceRobber(GameObject clickedGameObject=null)
    {
        if (!_placingRobber)
        {
            return;
        }

        if (gameController.GetPlayer().Ai)
        {
            clickedGameObject = gameController.hexmap.PickRandomHex();
            if (clickedGameObject.GetComponentInParent<CatanHexagon>().HasPlayerSettlement(gameController.GetPlayer()))
            {
                return;
            }
        }

        if ((clickedGameObject.tag == "CatanHex") && !clickedGameObject.GetComponentInParent<CatanHexagon>().beingRobbed)
        {
            _canSteal = true;
            currentHex.beingRobbed = false;
            currentHex = clickedGameObject.GetComponentInParent<CatanHexagon>();
            _placingRobber = !currentHex.PlaceRobberHere((CatanPlayer)gameController.GetPlayer());
            if (!_placingRobber && !gameController.GetPlayer().Ai)
            {
                gameController.StealResourcesAction?.Invoke();
            }
        }
    }

    public void TrySteal(GameObject clickedGameObject=null)
    {
        if (!_stealing)
        {
            return;
        }

        if (gameController.GetPlayer().Ai)
        {
            clickedGameObject = gameController.hexmap.PickRandomCrossroad();
        }

        if (clickedGameObject.tag == "Crossroads" || clickedGameObject.tag == "Settlement")
        {
            _stealing = !clickedGameObject.GetComponentInParent<Crossroads>().StealResource((CatanPlayer)gameController.GetPlayer());
            if (!_stealing && !gameController.GetPlayer().Ai)
            {
                gameController.ResourcesStolenAction?.Invoke();
            }
        }
    }

    public void PlaceRobber()
    {
        _placingRobber = true;

        if (gameController.GetPlayer().Ai)
        {
            while (_placingRobber)
            {
                TryPlaceRobber();
            }
            gameController.StealResourcesAction?.Invoke();
        }
    }

    public void Steal()
    {
        _stealing = _canSteal;

        if (gameController.GetPlayer().Ai)
        {
            while (_stealing)
            {
                TrySteal();
            }
            gameController.ResourcesStolenAction?.Invoke();
        }
    }

    public void CanSteal(bool canSteal)
    {
        _canSteal = canSteal;
    }

    public bool GetStealing()
    {
        return _stealing;
    }

    public bool GetCanSteal()
    {
        return _canSteal;
    }

    public bool GetPlacingRobber()
    {
        return _placingRobber;
    }
}
