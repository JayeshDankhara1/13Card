using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayUiManager : MonoBehaviour
{

    public TextMeshProUGUI CounDowan_Text;
    public List <Sprite> Card_Sprite = new List <Sprite>();
    public List<Image> Card_Image = new List <Image>();
    public List<Card> cards = new List<Card>();
    public List <GameObject>Card_GameObjects = new List<GameObject>();


    

    public static GamePlayUiManager Instance;
    public GamePlayManager Ref_GamePlayManager;

    public void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        DeavtiveCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CounDownTaxtUpdate(string Text)
    {
        CounDowan_Text.text = Text;
    }
    public void CounDowan(bool IsActive)
    {
        CounDowan_Text.gameObject.SetActive(IsActive);
    }
    public void LoadCard()
    {
        ActiveCard();
        for (int i = 0; i < Card_Image.Count; i++)
        {
            Card_Image[i].sprite = Card_Sprite[GetCardIndex(Ref_GamePlayManager.cards[i])];
            cards[i].Name = Ref_GamePlayManager.cards[i].Name;
            cards[i].Color = Ref_GamePlayManager.cards[i].Color;
        }
    }


    public int GetCardIndex(Card card)
    {
        return (Ref_GamePlayManager.GetColor(card.Color)) + (Ref_GamePlayManager.GetValue(card.Name) - 2);
    }

    public void DeavtiveCard()
    {
        for (int i = 0; i < 13; i++)
        {
            Card_Image[i].gameObject.SetActive(false);
        }
    }
    public void ActiveCard()
    {
        for (int i = 0; i < 13; i++)
        {
            Card_Image[i].gameObject.SetActive(true);
        }
       // GetCard();
    }

    public void GetCard()
    {
        for (int i = 0; i < Card_GameObjects.Count; i++)
        {
            cards[i] = Card_GameObjects[i].GetComponent<Card>();
        }
    }

}
