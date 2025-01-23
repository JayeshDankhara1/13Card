using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Test : MonoBehaviour
{

    public TextMeshProUGUI R_text;

    public GamePlayManager gamePlayManager;
    public List<Card> cards = new List<Card>();

    public void ButtonClick_Test()
    {
        R_text.text = gamePlayManager.TestResult(cards).ToString();
    }
}
