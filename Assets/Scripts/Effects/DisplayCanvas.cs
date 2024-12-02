using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// This class is used for controlling the canvas that will display messages and information to the player
/// </summary>
public class DisplayCanvas : MonoBehaviour
{
    [SerializeField] public GameObject mainMessageDisplay;
    [SerializeField] public TextMeshProUGUI mainMessageText;
    [SerializeField] public GameObject playAgain;
    [SerializeField] public GameObject deal;

    [SerializeField] public Button[] buttons;
    [SerializeField] CardSlot[] cardSlots;

    [SerializeField] float messageDisplayTime;
    public IEnumerator DisplayEvaluationMessage(HandEvaluator.Hand handRank)
    {
        switch (handRank)
        {
            case (HandEvaluator.Hand.ROYAL_FLUSH):
                mainMessageText.text = "Royal Flush!";
                break;
            case (HandEvaluator.Hand.STRAIGHT_FLUSH):
                mainMessageText.text = "Straight Flush!";
                break;
            case (HandEvaluator.Hand.FOUR_OF_A_KIND):
                mainMessageText.text = "Four of a Kind!";
                break;
            case (HandEvaluator.Hand.FULL_HOUSE):
                mainMessageText.text = "Full House!";
                break;
            case (HandEvaluator.Hand.FLUSH):
                mainMessageText.text = "Flush!";
                break;
            case (HandEvaluator.Hand.STRAIGHT):
                mainMessageText.text = "Straight!";
                break;
            case (HandEvaluator.Hand.THREE_OF_A_KIND):
                mainMessageText.text = "Three of a Kind!";
                break;
            case (HandEvaluator.Hand.TWO_PAIR):
                mainMessageText.text = "Two Pair!";
                break;
            case (HandEvaluator.Hand.LOSE):
                mainMessageText.text = "Try Again";
                break;
        }
        mainMessageDisplay.SetActive(true);
        SetAllButtonsActive(false);
        yield return new WaitForSeconds(messageDisplayTime);
        SetAllButtonsActive(true);
        mainMessageDisplay.SetActive(false);
        foreach(CardSlot slot in cardSlots)
        {
            slot.visuals.TurnCardFaceDown();
        }
    }

    public IEnumerator DisplayMessage(string message)
    {
        mainMessageText.text = message;
        mainMessageDisplay.SetActive(true);
        SetAllButtonsActive(false);
        yield return new WaitForSeconds(messageDisplayTime);
        SetAllButtonsActive(true);
        mainMessageDisplay.SetActive(false);
    }
    public IEnumerator DisplayInbetweenGameScreen(string message)
    {
        StartCoroutine(DisplayMessage(message));
        yield return new WaitForSeconds(messageDisplayTime);
        mainMessageText.text = "Play Again?";
        mainMessageDisplay.SetActive(true);
        playAgain.SetActive(true);
        SetAllButtonsActive(false);
        foreach (CardSlot slot in cardSlots)
        {
            if (!slot.isEmpty)
            {
                slot.visuals.TurnCardFaceDown();
            }           
        }
    }

    public void SetAllButtonsActive(bool shouldBeActive) // For making sure the player cannot input any commands during transition sequences
    {
        if(shouldBeActive)
        {
            foreach (Button b in buttons)
            {
                b.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Button b in buttons)
            {
                b.gameObject.SetActive(false);
            }
        }
    }
}
