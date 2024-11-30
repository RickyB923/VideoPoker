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
    

    public int royalFlush;
    public int straightFlush;
    public int fourOfAKind;
    public int fullHouse;
    public int flush;
    public int straight;
    public int threeOfAKind;
    public int twoPair;
    public int jacksOrBetter;

    public int[] handTypes;

    public void Initialize()
    {
        handTypes = new int[9];
        handTypes[0] = royalFlush;
        handTypes[1] = straightFlush;
        handTypes[2] = fourOfAKind;
        handTypes[3] = fullHouse;
        handTypes[4] = flush;
        handTypes[5] = straight;
        handTypes[6] = threeOfAKind;
        handTypes[7] = twoPair;
        handTypes[8] = jacksOrBetter;
    }

}
