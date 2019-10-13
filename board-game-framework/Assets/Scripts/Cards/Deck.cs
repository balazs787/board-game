using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public GameObject knightCardPrefab;
    public GameObject victoryPointCardPrefab;
    public GameObject yearOfPlentyCardPrefab;
    public GameObject roadBuildingCardPrefab;
    public GameObject monopolyCardPrefab;
    public GameObject cardInspector;
    public List<ICard> cards;
    void Start()
    {
        cards = new List<ICard>();

        for (int i = 0; i < 15; i++)
        {
            cards.Add(Instantiate(knightCardPrefab, cardInspector.transform).GetComponent<ICard>());
        }

        for (int i = 0; i < 5; i++)
        {
            cards.Add(Instantiate(victoryPointCardPrefab, cardInspector.transform).GetComponent<ICard>());
        }

        cards.Add(Instantiate(yearOfPlentyCardPrefab, cardInspector.transform).GetComponent<ICard>());
        cards.Add(Instantiate(yearOfPlentyCardPrefab, cardInspector.transform).GetComponent<ICard>());

        cards.Add(Instantiate(roadBuildingCardPrefab, cardInspector.transform).GetComponent<ICard>());
        cards.Add(Instantiate(roadBuildingCardPrefab, cardInspector.transform).GetComponent<ICard>());

        cards.Add(Instantiate(monopolyCardPrefab, cardInspector.transform).GetComponent<ICard>());
        cards.Add(Instantiate(monopolyCardPrefab, cardInspector.transform).GetComponent<ICard>());

        foreach (var c in cards)
        {
            c.GetGameObject().SetActive(false);
        }
    }

    public ICard GetCard()
    {
        var random = Random.Range(0, cards.Count);
        ICard card = cards[random];
        cards.RemoveAt(random);
        return card;
    }
}
