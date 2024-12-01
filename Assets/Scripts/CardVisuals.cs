using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardVisuals : MonoBehaviour
{
    [SerializeField] public Image suit;
    [SerializeField] public TextMeshProUGUI numberText;

    [SerializeField] Color redColor;
    [SerializeField] Color blackColor;
    private Color numberColor;

    [SerializeField] Sprite[] suitSprites;

    public void SetCardVisuals(Card.Suit suit, Card.Number number)
    {
        switch (suit)
        {
            case Card.Suit.SPADES:
                this.suit.sprite = suitSprites[0];
                numberColor = blackColor;
                break;
            case Card.Suit.HEARTS:
                this.suit.sprite = suitSprites[1];
                numberColor = redColor;
                break;
            case Card.Suit.DIAMONDS:
                this.suit.sprite = suitSprites[2];
                numberColor = redColor;
                break;
            case Card.Suit.CLUBS:
                this.suit.sprite = suitSprites[3];
                numberColor = blackColor;
                break;
        }
        numberText.color = numberColor;
        switch (number)
        {
            case Card.Number.ACE:
                numberText.text = "A";
                break;
            case Card.Number.TWO:
                numberText.text = "2";
                break;
            case Card.Number.THREE:
                numberText.text = "3";
                break;
            case Card.Number.FOUR:
                numberText.text = "4";
                break;
            case Card.Number.FIVE:
                numberText.text = "5";
                break;
            case Card.Number.SIX:
                numberText.text = "6";
                break;
            case Card.Number.SEVEN:
                numberText.text = "7";
                break;
            case Card.Number.EIGHT:
                numberText.text = "8";
                break;
            case Card.Number.NINE:
                numberText.text = "9";
                break;
            case Card.Number.TEN:
                numberText.text = "10";
                break;
            case Card.Number.JACK:
                numberText.text = "J";
                break;
            case Card.Number.QUEEN:
                numberText.text = "Q";
                break;
            case Card.Number.KING:
                numberText.text = "K";
                break;
        }


    }
}
