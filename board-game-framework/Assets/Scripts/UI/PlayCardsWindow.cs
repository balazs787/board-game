using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayCardsWindow : MonoBehaviour
{
    public GameController gameController;
    public TextMeshProUGUI cardNumberText;
    public List<ICard> currentCards = new List<ICard>();
    public int currentIndex=0;

    public void Refresh(Player player)
    {
        currentCards = player.cards;
        UpdateCardNumberText();
    }
    public void Hide(bool hide)
    {
        currentIndex = 0;
        gameObject.SetActive(!hide);
        currentCards[currentIndex].GetGameObject().SetActive(!hide);
        UpdateCardNumberText();
    }

    public void Next()
    {
        if (currentIndex + 1 == currentCards.Count)
            return;

        currentCards[currentIndex].GetGameObject().SetActive(false);
        currentIndex++;
        currentCards[currentIndex].GetGameObject().SetActive(true);
        UpdateCardNumberText();
    }

    public void Previous()
    {
        if (currentIndex == 0)
            return;

        currentCards[currentIndex].GetGameObject().SetActive(false);
        currentIndex--;
        currentCards[currentIndex].GetGameObject().SetActive(true);
        UpdateCardNumberText();
    }

    public void UpdateCardNumberText()
    {
        cardNumberText.text = $"{currentIndex + 1} / {currentCards.Count}";
    }
}
