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

    public void CreatCard()
    {
        GameObject TempGameObject = Instantiate(Pref_GameObject, Parent1_GameObject);
        
        LoadSprit(TempGameObject, LoadRendomCard(TempGameObject));

        Card_GameObjects.Add(TempGameObject);

    }

    
    public void LoadSprit(GameObject gameObject, Card card)
    {
        gameObject.GetComponent<Image>().sprite = Card_Sprite[GetCardIndex(card)];
    }

    public Card LoadRendomCard(GameObject gameObject)
    {
            
        Card card = gameObject.GetComponent<Card>();
        card.Color = (Color)Random.Range(0, 4);
        card.Name = (Name)Random.Range(0, 13);
        return card;
        //cards[i].Name = (Name)Random.Range(0, 13) ;
        //cards[i].Color =(Color)Random.Range(0, 4);
        //Card_Image[i].sprite = Card_Sprite[GetCardIndex(cards[i])];

    }
}
