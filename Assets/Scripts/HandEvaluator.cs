using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HandEvaluator
{
    public enum Hand
    {
        ROYAL_FLUSH,
        STRAIGHT_FLUSH,
        FOUR_OF_A_KIND,
        FULL_HOUSE,
        FLUSH,
        STRAIGHT,
        THREE_OF_A_KIND,
        TWO_PAIR,
        //ONE_PAIR,
        HIGH_CARD,
    }
    public Hand EvaluateHand(List<Card> hand)
    {
        if (IsRoyalFlush(hand)) return Hand.ROYAL_FLUSH;
        if (IsStraightFlush(hand)) return Hand.STRAIGHT_FLUSH;
        if (IsFourOfAKind(hand)) return Hand.FOUR_OF_A_KIND;
        if (IsFullHouse(hand)) return Hand.FULL_HOUSE;
        if (IsFlush(hand)) return Hand.FLUSH;
        if (IsStraight(hand)) return Hand.STRAIGHT;
        if (IsThreeOfAKind(hand)) return Hand.THREE_OF_A_KIND;
        if (IsTwoPair(hand)) return Hand.TWO_PAIR;
        //if (IsOnePair(hand)) return Hand.ONE_PAIR;
        return Hand.HIGH_CARD;
    }
    private bool IsRoyalFlush(List<Card> hand)
    {
        return IsStraightFlush(hand) && hand.All(card => card.number >= Card.Number.TEN);
    }
    private bool IsStraightFlush(List<Card> hand)
    {
        return IsFlush(hand) && IsStraight(hand);
    }

    private bool IsFourOfAKind(List<Card> hand)
    {
        var rankGroups = hand.GroupBy(card => card.number);
        return rankGroups.Any(group => group.Count() == 4);
    }

    private bool IsFullHouse(List<Card> hand)
    {
        var rankGroups = hand.GroupBy(card => card.number);
        return rankGroups.Any(group => group.Count() == 3) && rankGroups.Any(group => group.Count() == 2);
    }

    private bool IsFlush(List<Card> hand)
    {
        return hand.GroupBy(card => card.suit).Count() == 1;
    }

    private bool IsStraight(List<Card> hand)
    {
        var sortedRanks = hand.Select(card => (int)card.number).OrderBy(rank => rank).ToList();
        if (sortedRanks.Last() == (int)Card.Number.ACE && sortedRanks.First() == (int)Card.Number.TWO)
        {
            // Handle A-2-3-4-5 as a valid straight (wheel)
            sortedRanks.Remove(sortedRanks.Last());
            sortedRanks.Insert(0, 1);
        }
        for (int i = 1; i < sortedRanks.Count; i++)
        {
            if (sortedRanks[i] != sortedRanks[i - 1] + 1)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsThreeOfAKind(List<Card> hand)
    {
        var rankGroups = hand.GroupBy(card => card.number);
        return rankGroups.Any(group => group.Count() == 3);
    }

    private bool IsTwoPair(List<Card> hand)
    {
        var rankGroups = hand.GroupBy(card => card.number);
        return rankGroups.Count(group => group.Count() == 2) == 2;
    }

    //private bool IsOnePair(List<Card> hand)
    //{
    //    var rankGroups = hand.GroupBy(card => card.number);
    //    return rankGroups.Any(group => group.Count() == 2);
    //}
}
