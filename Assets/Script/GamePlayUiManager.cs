
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms.Impl;

public class GamePlayUiManager : MonoBehaviour
{
    #region Refarance Varibal 
    public List <Sprite> Card_Sprite = new List <Sprite>();
    [HideInInspector]
    public List<Card> cards = new List<Card>();
    public List <GameObject>Card_GameObjects = new List<GameObject>();
    [HideInInspector]
    public List <Card> Card_List1 = new List<Card>();
    [HideInInspector]
    public List<Card> Card_List2 = new List<Card>();
    [HideInInspector]
    public List<Card> Card_List3 = new List<Card>();

    [Header("Hader")]
    public GameObject HaderParent;
    public TextMeshProUGUI Score_text;
    public TextMeshProUGUI StopWatch_text;
    public TextMeshProUGUI CounDowan_Text;

    [Space]
    [Header("Table")]
    public GameObject Tabel;
    [Space]
    [Space]
    public RectTransform Parent1_GameObject;
    public TextMeshProUGUI Result1_Text;
    public TextMeshProUGUI Score1_Text;
    public Image Image_sorce1;
    [Space]
    [Space]
    public RectTransform Parent2_GameObject;
    public TextMeshProUGUI Result2_Text;
    public TextMeshProUGUI Score2_Text;
    public Image Image_sorce2;
    [Space]
    [Space]
    public Transform Parent3_GameObject;
    public TextMeshProUGUI Result3_Text;
    public TextMeshProUGUI Score3_Text;
    public Image Image_sorce3;
    [Space]
    [Space]
    [Header("Color")]
    public Color32 W_Color;
    public Color32 R_Color;
    public Color32 Y_Color;
    public Color32 G_Color;
    [Space]
    [Header("Card")]
    public GameObject Pref_GameObject;
    

    public static GamePlayUiManager Instance;
    [HideInInspector]
    public GamePlayManager Ref_GamePlayManager;
    #endregion

    #region unity function
    public void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Ref_GamePlayManager = GamePlayManager.instance;
        GameStart();
        
    }
    #endregion

    #region Other Function
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
        return card.Name==Name.Joker ? 52: (Ref_GamePlayManager.GetColor(card.Color)) + (Ref_GamePlayManager.GetValue(card.Name) - 2);
    }

    

    public void LoadCard()
    {
        for (int i = 0; i < Card_GameObjects.Count; i++) 
        {
            SetCardData(GetCard(Card_GameObjects[i]), cards[i].Color, cards[i].Name);
            LoadSprit(Card_GameObjects[i], GetCard(Card_GameObjects[i]));
           
        }
    }
    public void ActiveObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    public void DeactiveAllCard()
    {
        for (int i = 0; i < Card_GameObjects.Count; i++)
        {
            DeaciveObject(Card_GameObjects[i]);
        }
    }
    public void DeaciveObject(GameObject gameObject)
    { 
        gameObject.SetActive(false);
    }

   
   
    
    
    public void LoadSprit(GameObject gameObject, Card card)
    {
       
        gameObject.GetComponent<Image>().sprite = Card_Sprite[GetCardIndex(card)];

    }

    public Card LoadRendomCard(int i)
    {
        if (i==10)
        {

            Color color = Color.Null;
            Name name = Name.Joker;
            Card card = new Card(color, name);
            return card;
        }
        else
        {
          
             Card card = new Card((Color)Random.Range(0, 4), (Name)Random.Range(0, 13));

            return card;
        }
    

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
        int i = 0;
        cards.Clear();
        while (cards.Count < 13)
        {
            CreatCard(LoadRendomCard(i));
            i++;
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
        DeactiveAllCard();
        CreatCardList();
     
    }

    public Card Find_Card(Transform transform , int Index)
    {

        return transform.GetChild(Index).GetComponent<Card>(); 
    }

    public Image GetImage_Sorce(GameObject gameObject)
    { 
        return gameObject.GetComponent<Image>();
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
            Card_List1.Add(Find_Card(Parent1_GameObject, i+2));
        }
    }

    public void List2Update()
    {
        Card_List2.Clear();

        for (int i = 0; i < 5; i++)
        {
            Card_List2.Add(Find_Card(Parent2_GameObject, i+2));
        }
    }
    public void List3Update()
    {
        Card_List3.Clear();

        for (int i = 0; i < 3; i++)
        {
            Card_List3.Add(Find_Card(Parent3_GameObject, i + 2));
        }
    }

    public void DeHighliteAllCard()
    {
        DeHighLiteCard(List1Call());
        DeHighLiteCard(List2Call());
        DeHighLiteCard(List3Call());

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

    public void SetStopWatch_Text(int MM = 0, int SS = 0)
    {
        StopWatch_text.text = MM.ToString("00") +":"+ SS.ToString("00");
    }
    public void SetScore_Text(int score)
    { 
        Ref_GamePlayManager.Ref_Animation.ScoreUpdate(score, 0, Score_text);
    }

    public void SetScore1_Text(int Score)
    {
        Ref_GamePlayManager.Ref_Animation.ScoreUpdate(Score, 0, Score1_Text," X 1");
    }

    public void SetScore2_Text(int Score)
    {
        Ref_GamePlayManager.Ref_Animation.ScoreUpdate(Score, 0, Score2_Text, " X 2");
    }

    public void SetScore3_Text(int Score)
    {
        Ref_GamePlayManager.Ref_Animation.ScoreUpdate(Score, 0, Score3_Text, " X 3");
    }
    public void SwitchList()
    {
        for (int i = 2; i <= 6; i++)
        {
            SwapGameObject(Find_GameObject(Parent1_GameObject, i), Find_GameObject(Parent2_GameObject, i));
        }
        List1Update();
        List2Update();
       
    }

    public Image GetImage_Sorce1()
    {
        return Image_sorce1;
    }

    public Image GetImage_Sorce2()
    {
        return Image_sorce2;
    }

    public Image GetImage_Sorce3()
    {
        return Image_sorce3;
    }

   
    public void SwapGameObject(GameObject obj1, GameObject obj2)
    {

        Ref_GamePlayManager.SwapCard(GetCard(obj1), GetCard(obj2));
        LoadSprit(obj1, GetCard(obj1));
        LoadSprit(obj2, GetCard(obj2));

    }


    public void DeHighLiteCard(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            GetImage_Sorce(cards[i].gameObject).color = W_Color;
        }
    }


    public void TabelSetActive(bool active) 
    {
        Tabel.gameObject.SetActive(active);
    }
    public void HedarParentSetActive(bool active)
    {
        HaderParent.gameObject.SetActive(active);
    }

    public void StopWatch(int Seconds)
    {
        int mm = Seconds / 60;
        int ss = Seconds-(mm*60);
        SetStopWatch_Text(mm,ss);
    }
    #endregion

}
