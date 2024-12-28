using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GamePlayUiManager : MonoBehaviour
{

    public TextMeshProUGUI CounDowan_Text;
    public List <Sprite> Card_Sprite = new List <Sprite>();
   // public List<Image> Card_Image = new List <Image>();
    public List<Card> cards = new List<Card>();
    public List <GameObject>Card_GameObjects = new List<GameObject>();
    public Card TempCard;


    public GameObject Pref_GameObject;
    
    public Transform Parent1_GameObject;
    public Transform Parent2_GameObject;
    public Transform Parent3_GameObject;


    public static GamePlayUiManager Instance;
    public GamePlayManager Ref_GamePlayManager;

    public void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        // DeavtiveCard();
        GameStart();
        
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
    


    public int GetCardIndex(Card card)
    {
        return (Ref_GamePlayManager.GetColor(card.Color)) + (Ref_GamePlayManager.GetValue(card.Name) - 2);
    }

    

    public void LoadCard()
    {
        for (int i = 0; i < Card_GameObjects.Count; i++) 
        {
            SetCardData(GetCard(Card_GameObjects[i]), cards[i].Color, cards[i].Name);
            LoadSprit(Card_GameObjects[i], GetCard(Card_GameObjects[i]));
            ActiveObject(Card_GameObjects[i]);
        }
    }
    public void ActiveObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public void DeaciveObject(GameObject gameObject)
    { 
        gameObject.SetActive(false);
    }

    public void CreatCard_GameObjectList()
    {
        Card_GameObjects.Clear();
        while (Card_GameObjects.Count < 13)
        {
            Card_GameObject();
        }
    }

    public void Card_GameObject()
    {
        GameObject TempGameObject = Instantiate(Pref_GameObject, SetTrnform(Card_GameObjects.Count));
        DeaciveObject(TempGameObject);
        Card_GameObjects.Add(TempGameObject);
    }

    public Transform SetTrnform(int Count)
    {
        if (Count < 5)
        {
            return Parent1_GameObject;
        }
        else if (Count < 10)
        {
            return Parent2_GameObject;
        }
        else
        {
            return Parent3_GameObject;
        }

    }
    
    
    public void LoadSprit(GameObject gameObject, Card card)
    {
        gameObject.GetComponent<Image>().sprite = Card_Sprite[GetCardIndex(card)];
    }

    public Card LoadRendomCard()
    {
       
        TempCard.Color = (Color)Random.Range(0, 4);
        TempCard.Name = (Name)Random.Range(0, 13);

        
        //Card card =new Card(color, name);
       // Card card = gameObject.GetComponent<Card>();
        return TempCard;

    }
    public Card GetCard(GameObject gameObject)
    {
        return gameObject.GetComponent<Card>();

    }

    public void SetCardData(Card card, Color color, Name name)
    { 
        card.Name = name;
        card.Color = color;
    }

   
    public void CreatCardList()
    {
        cards.Clear();
        while (cards.Count < 13)
        {
            CreatCard(LoadRendomCard());
        }
    }

    public void CreatCard(Card card)
    {
       
        if (!cards.Any())
        {
            Debug.Log("FastCard Creat List");
            cards.Add(card);
        }
        else if (!FindEliment(card, cards))
        {
            Debug.Log("SecoundCard Creat List");
            cards.Add(card);
            return;
        }
    }

    public bool FindEliment(Card card, List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].Color == card.Color && cards[i].Name == card.Name)
                return true;
        }
        return false;
    }


    public void GameStart()
    {
        CreatCardList();
        CreatCard_GameObjectList();
    }

}
