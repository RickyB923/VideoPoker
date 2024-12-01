using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerGameManager : GameManager
{
    new void Start()
    {
        base.Start();
        StartGame();
    }


    public void StartGame()
    {
        //Deals 5 cards to the player at the start of the game
        for (int i = 0; i < 5; i++)
        {
            Card newCard = deck.DealCard();    
            hand.Add(newCard);
            cardSlots[i].currentCard = newCard;
            cardSlots[i].isEmpty = false;
            GameObject cardObject = Instantiate(cardPrefab, cardSlots[i].transform);
            cardSlots[i].visuals = cardObject.GetComponent<CardVisuals>();
            cardSlots[i].visuals.SetCardVisuals(newCard.suit, newCard.number);
        }
    }
    public void Deal()
    {
        int slotsEmpty = 0;
        foreach(CardSlot slot in cardSlots)
        {
            if(!slot.shouldHold)
            {
                hand.Remove(slot.currentCard);
                slot.isEmpty = true;
                slotsEmpty++;
            }
        }
        
        foreach(CardSlot slot in cardSlots)
        {
            if(slot.isEmpty)
            {
                Card newCard = deck.DealCard();
                hand.Add(newCard);
                slot.visuals.SetCardVisuals(newCard.suit, newCard.number);
                slot.currentCard = newCard;
                slot.isEmpty = false;
            }
        }

        foreach(Card card in hand)
        {
            Debug.Log($"{card.number} of {card.suit}");
        }

        EvaluateHand();
    }
    void EvaluateHand()
    {
        //Debug.Log(evaluator.EvaluateHand(hand));
        HandEvaluator.Hand handRank = evaluator.EvaluateHand(hand);
        Debug.Log(handRank);
        credits += (payTable.handTypes[(int)handRank]) * bet;
        creditsText.text = credits.ToString();
    }
}
