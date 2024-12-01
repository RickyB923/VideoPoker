using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokerGameManager : GameManager
{
    private bool isFirstGame;
    private bool hasDealtFirstHand;
    
    new void Start()
    {
        base.Start();
        isFirstGame = true;
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
            isFirstGame = false;
        }       
    }
    public void DealSecondHand()
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
                
                slot.visuals.UpdateCardVisuals();
                slot.visuals.SetCardVisuals(newCard.suit, newCard.number);
                slot.currentCard = newCard;
                slot.isEmpty = false;
            }
        }
        foreach (CardSlot slot in cardSlots)
        {
            slot.shouldHold = false;
            slot.holdIcon.SetActive(false);
        }
        EvaluateHand();
    }
    void EvaluateHand()
    {
        HandEvaluator.Hand handRank = evaluator.EvaluateHand(hand);
        Debug.Log(handRank);
        if (handRank != HandEvaluator.Hand.LOSE)
        {
            credits += (payTable.handTypes[(int)handRank]) * bet;
            creditsText.text = credits.ToString();
        }
        StartCoroutine(display.DisplayEvaluationMessage(handRank));
    }
    public void OnDealButtonClick()
    {
        if(!hasDealtFirstHand)
        {
            // Reseting the deck and card slots
            deck.ShuffleDeck();
            hand.Clear();

            if (credits >= bet)
            {
                if(isFirstGame)
                {
                    StartGame();
                }
                else
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Card newCard = deck.DealCard();
                        hand.Add(newCard);
                        cardSlots[i].visuals.TurnCardFaceUp();
                        cardSlots[i].visuals.UpdateCardVisuals();
                        cardSlots[i].visuals.SetCardVisuals(newCard.suit, newCard.number);
                        cardSlots[i].currentCard = newCard;
                        cardSlots[i].isEmpty = false;
                    }
                }
                


                credits -= bet;
                creditsText.text = credits.ToString();
                
                hasDealtFirstHand = true;
            }
            else
            {
                StartCoroutine(display.DisplayMessage("Not Enough Credits"));
            }
        }
        else
        {
            DealSecondHand();
            hasDealtFirstHand = false;
        }
    }
    public void RaiseBet()
    {
        if (hasDealtFirstHand)
            return;
        if (bet < 5)
        {
            bet++;
            betText.text = bet.ToString();
        }
    }
    public void LowerBet()
    {
        if (hasDealtFirstHand)
            return;
        if (bet > 1)
        {
            bet--;
            betText.text = bet.ToString();
        }
    }
}
