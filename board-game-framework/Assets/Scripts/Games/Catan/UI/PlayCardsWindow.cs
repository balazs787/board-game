using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayCardsWindow : MonoBehaviour
{
    public CatanGameController gameController;
    public TextMeshProUGUI cardNumberText;
    public GameObject playButton;
    public List<ICard> currentCards;
    public int currentIndex=0;

    public void Refresh(CatanPlayer player)
    {
        currentCards = player.cards;
        UpdateCardNumberText();
    }

    public void Open()
    {
        currentIndex = 0;
        gameObject.SetActive(true);
        currentCards[currentIndex]?.GetGameObject().SetActive(true);
        UpdateCardNumberText();
    }

    public void Close()
    {
        if (currentCards?.Count > 0)
        {
            currentCards[currentIndex]?.GetGameObject().SetActive(false);
        }
        gameObject.SetActive(false);
    }

    public void Next()
    {
        if (currentIndex + 1 >= currentCards.Count)
            return;

        currentCards[currentIndex]?.GetGameObject().SetActive(false);
        currentIndex++;
        currentCards[currentIndex]?.GetGameObject().SetActive(true);
        UpdateCardNumberText();
    }

    public void Previous()
    {
        if (currentIndex <= 0)
            return;

        currentCards[currentIndex]?.GetGameObject().SetActive(false);
        currentIndex--;
        currentCards[currentIndex]?.GetGameObject().SetActive(true);
        UpdateCardNumberText();
    }

    public void UpdateCardNumberText()
    {
        if (currentCards?.Count == 0)
            return;

        cardNumberText.text = $"{currentIndex + 1} / {currentCards.Count}";

        if (currentCards[currentIndex].GetPlayable())
        {
            playButton.GetComponent<Image>().color = Color.green;
            playButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            playButton.GetComponent<Image>().color = Color.gray;
            playButton.GetComponent<Button>().interactable = false;
        }
    }


    public void PlayButton()
    {
        var card = currentCards[currentIndex];
        card.Play(gameController);
        gameController.GetPlayer().cards.Remove(card);
        currentIndex = 0;
        Refresh(gameController.GetPlayer());
        Destroy(card.GetGameObject());
    }
}
