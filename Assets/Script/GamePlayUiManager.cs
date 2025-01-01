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
    
    public List <Card> Card_List1 = new List<Card>();
    public List<Card> Card_List2 = new List<Card>();
    public List<Card> Card_List3 = new List<Card>();

    public TextMeshProUGUI Result1_Text;
    public TextMeshProUGUI Result2_Text;
    public TextMeshProUGUI Result3_Text;



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
       Card card = new Card((Color)Random.Range(0, 4), (Name)Random.Range(0, 13));
        return card;

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
            cards.Add(card);
        }
        else if (!FindEliment(card, cards))
        {
            
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

    public Card Find_Card(Transform transform , int Index)
    {
        

        return transform.GetChild(Index).GetComponent<Card>(); 
    }

    public GameObject Find_GameObject(Transform transform, int Index)
    {
        return transform.GetChild(Index).gameObject;
    }

    public void List1Update()
    {
        Card_List1.Clear();

        for (int i = 0; i < 5; i++)
        {
            Card_List1.Add(Find_Card(Parent1_GameObject, i));
        }
    }

    public void List2Update()
    {
        Card_List2.Clear();

        for (int i = 0; i < 5; i++)
        {
            Card_List2.Add(Find_Card(Parent2_GameObject, i));
        }
    }
    public void List3Update()
    {
        Card_List3.Clear();

        for (int i = 0; i < 3; i++)
        {
            Card_List3.Add(Find_Card(Parent3_GameObject, i));
        }
    }

    public void AllCardListUpdate()
    {
        List1Update();
        List2Update();
        List3Update();
        Ref_GamePlayManager.ShowResult();
       
    }


    public List<Card> List1Call()
    {
        return Card_List1;
    }

    public List<Card> List2Call()
    {
        return Card_List2;
    }
    public List<Card> List3Call()
    {
        return Card_List3;
    }

    public void SetResult1_Text(string text)
    { 
        Result1_Text.text = text;
    }

    public void SetResult2_Text(string text)
    {
        Result2_Text.text = text;
    }
    public void SetResult3_Text(string text)
    {
        Result3_Text.text = text;
    }

    public void SwitchList()
    {
        SwapCard(List1Call(), List2Call());
        Load_Sprit();
        AllCardListUpdate();

    }


    public void SwapCard(List<Card> card1, List<Card> card2)
    {

        for (int i = 0; i < card1.Count; i++)
        { 
            Color color = card1[i].Color;
            Name name = card1[i].Name;

            card1[i].Color = card2[i].Color;
            card1[i].Name = card2[i].Name;

            card2[i].Color = color;
            card2[i].Name = name;


        }
       
    }

    public void Load_Sprit()
    {
        for (int i = 0; i < 10; i++)
        {
            if (i < 5)
            {
                LoadSprit(Find_GameObject(Parent1_GameObject,i), Card_List1[i]);
            }
            else
            {
                LoadSprit(Find_GameObject(Parent2_GameObject, i-5), Card_List2[i-5]);
            }
        }
        
    }

  
}
