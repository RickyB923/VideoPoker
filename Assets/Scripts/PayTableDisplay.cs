using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PayTableDisplay : MonoBehaviour
{
    [SerializeField] HandTypeDisplay[] handTypes;

    private void OnEnable()
    {
        GameManager.gameInitialized += Initialize;
    }
    private void OnDisable()
    {
        GameManager.gameInitialized -= Initialize;
    }
    public void Initialize()
    {
        for (int i = 0; i < handTypes.Length; i++)
        {
            handTypes[i].baseValue = GameManager.Instance.payTable.handTypes[i];
            int j = 1;
            foreach(TextMeshProUGUI text in handTypes[i].valueTexts)
            {
                text.text = (handTypes[i].baseValue * j).ToString();
                j++;
            }
        }
        // To account for a 5 bet royal flush
        handTypes[0].valueTexts[4].text = GameManager.Instance.payTable.fiveBetRoyalFlush.ToString();
    }
}
