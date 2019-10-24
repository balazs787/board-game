﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robber : MonoBehaviour
{
    private bool _placingRobber;
    private bool _stealing;
    private bool _canSteal;
    public GameController gameController;
    public CatanHexagon currentHex;

    private void Start()
    {
        gameObject.transform.position = currentHex.transform.position;
    }
    void Update()
    {
        if (_placingRobber && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);



            if (Physics.Raycast(ray, out hit, 100f))
            {
                Player currentPlayer = gameController.GetPlayer();

                if ((hit.transform?.gameObject.tag == "CatanHex"))
                {
                    _canSteal = true;
                    currentHex.beingRobbed = false;
                    currentHex = hit.transform.gameObject.GetComponentInParent<CatanHexagon>();
                    _placingRobber = !currentHex.PlaceRobberHere(currentPlayer);
                    if (!_placingRobber)
                    {
                        gameController.StealResourcesAction?.Invoke();
                    }
                }
            }
        }

        if(_stealing && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);



            if (Physics.Raycast(ray, out hit, 100f))
            {
                Player currentPlayer = gameController.GetPlayer();

                if (hit.transform?.gameObject.tag == "Crossroads" || hit.transform?.gameObject.tag == "Settlement")
                {
                    _stealing = !hit.transform.gameObject.GetComponentInParent<Crossroads>().StealResource(currentPlayer);
                    if (!_stealing)
                    {
                        gameController.ResourcesStolenAction?.Invoke();
                    }
                }
            }
        }
    }

    public void PlaceRobber()
    {
        _placingRobber = true;
    }

    public void Steal()
    {
        _stealing = _canSteal;
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
