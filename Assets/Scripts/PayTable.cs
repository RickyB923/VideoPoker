using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Each int represents the payout for each type of winning hand
/// How much to pay out based on each hand can be adjusted in the pay table SO asset that the game manager is using
/// </summary>

[CreateAssetMenu(fileName = "New Pay Table", menuName = "Pay Table" )]
public class PayTable : ScriptableObject
{
    public int fiveBetRoyalFlush;
    public int royalFlush;
    public int straightFlush;
    public int fourOfAKind;
    public int fullHouse;
    public int flush;
    public int straight;
    public int threeOfAKind;
    public int twoPair;

    public int[] handTypes;

    public void Initialize()
    {
        handTypes = new int[9];
        handTypes[0] = fiveBetRoyalFlush;
        handTypes[1] = royalFlush;
        handTypes[2] = straightFlush;
        handTypes[3] = fourOfAKind;
        handTypes[4] = fullHouse;
        handTypes[5] = flush;
        handTypes[6] = straight;
        handTypes[7] = threeOfAKind;
        handTypes[8] = twoPair;
    }
}
