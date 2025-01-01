
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum Result 
{
    HighCard=500,
    Pair=1000,
    TwoPairs=2000,
    ThreeOfaKind=4000,
    Straight=6000, 
    Flush=7000,
    FullHouse=8000,
    FourOfaKind=9000,
    StraightFlush=12000,
    RoyalFlush=15000
} 


public class GamePlayManager : MonoBehaviour
{

    public GamePlayUiManager Ref_GamePlayUiManager;

    public static GamePlayManager instance;

    public bool IsTrigar = false;
    public GameObject Collide_GameObject;
    public Vector3 Collide_GameObject_Postion;
    public GameObject Collide_GameObject1;
    public Vector3 Collide_GameObject_Postion1;


    // Start is called before the first frame update

    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        StartCoroutine(GameStart());
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator GameStart()
    {
        Ref_GamePlayUiManager.CounDowan(true);
        for (int i = 3; i >= 0; i--)
        {
            if (i == 0)
            {
                Ref_GamePlayUiManager.CounDownTaxtUpdate("Go!!!");
            }
            else
            {
                Ref_GamePlayUiManager.CounDownTaxtUpdate(i.ToString());
            }
            yield return new WaitForSeconds(1);
        }
        Ref_GamePlayUiManager.CounDowan(false);
        Ref_GamePlayUiManager.LoadCard();
        Ref_GamePlayUiManager.AllCardListUpdate();

        
      //  Ref_GamePlayUiManager.LoadCard();

    }

    public int GetValue(Name name)
    {
     
        switch (name)
        {
            case Name.Ace:
                return 14;
            case Name.Two:
                return 2;
            case Name.Three:
                return 3;
            case Name.Four:
                return 4;
            case Name.Five:
                return 5;
            case Name.Six:
                return 6;
            case Name.Seven:
                return 7;
            case Name.Eight:
                return 8;
            case Name.Nine:
                return 9;
            case Name.Ten:
                return 10;
            case Name.Jack:
                return 11;
            case Name.Queen:
                return 12;
            case Name.King:
                return 13;
        }
        return 0;
    }

    public int GetColor(Color color)
    {
       // Debug.Log("Color" + color);
        switch (color)
        {
            case Color.Clubs:
                return 0;
            case Color.Diamonds:
                return 13;
            case Color.Hearts:
                return 26;
            case Color.Spades:
                return 39;
            
        }
        return 0;
    }




    public bool RoyalFlush(List<Card> cards)
    {
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));
        if (StraightFlush(cards) && cards[0].Name == Name.Ace && cards[cards.Count - 1].Name == Name.King)
        {
            return true;
        }
        return false;
       
    }
    public bool StraightFlush(List<Card> cards)
    {
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));
        if (Flush(cards) && Straight(cards))
        {
            return true;
        }
        return false;
    }
    public bool FourOfaKind(List<Card> cards)
    {
       
        if (cards.Count < 5)
            return false;
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));

        var groupedCards = cards.GroupBy(c => c.Name);

        foreach (var group in groupedCards)
        {
            if (group.Count() == 4)
            {
                    return true;
            }
        }
      return false;
   
    }

   
    public bool FullHouse(List<Card> cards)
    {
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));
        var groupedCards = cards.GroupBy(c => c.Name);

        bool hasThree = ThreeOfaKind(cards);
        bool hasTwo = false;

        foreach (var group in groupedCards)
        {
            if (group.Count() == 2)
            {
                hasTwo = true;
            }
        }
        return hasThree && hasTwo;

    }
    public bool Flush(List<Card> cards)
    {
        if (cards.Count < 5)
            return false;
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));
        for (int i = 0; i < cards.Count-1; i++)
        {
            if (cards[i].Color != cards[i+1].Color)
            {
                return false;
            }
        }
        return true;
    }
    public bool Straight(List<Card> cards)
    {
        if (cards.Count < 5)
            return false;
        Jump:
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));
        for (int i = 1; i < cards.Count ; i++)
        {
            if (cards[i].Name != cards[i - 1].Name + 1)
            {
                if (cards[0].Name == Name.Ace && cards[cards.Count - 1].Name == Name.King)
                {
                    cards.RemoveAt(0);
                    goto Jump;
                }
                return false;
            }
        }
        return true;
    }

  

    public bool ThreeOfaKind(List<Card> cards)
    {
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));
        var groupedCards = cards.GroupBy(c => c.Name);

        foreach (var group in groupedCards)
        {
            if (group.Count() == 3)
            {
                return true;
            }
        }
        return false;
       
    }
    public bool TwoPairs(List<Card> cards)
    {
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));
        var groupedCards = cards.GroupBy(c => c.Name);      
        int pairCount = 0;

        foreach (var group in groupedCards)
        {
            if (group.Count() == 2)
            {
                pairCount++;
            }
        }
        return pairCount == 2;
    }

    public bool Pair(List<Card> cards)
    {


        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));
        var groupedCards = cards.GroupBy(c => c.Name);
       

        foreach (var group in groupedCards)
        {
            if (group.Count() == 2)
            {
                return true;
            }
        }
        return false;
    }



    public Result TestResult(List<Card> cards)
    {
        if (RoyalFlush(cards))
        {
            return Result.RoyalFlush;
        }
        else if (StraightFlush(cards))
        {
            return Result.StraightFlush;
        }
        else if (FourOfaKind(cards))
        {
            return Result.FourOfaKind;
        }
        else if (FullHouse(cards))
        {
            return Result.FullHouse;
        }
        else if (Flush(cards))
        {
            return Result.Flush;
        }
        else if (Straight(cards))
        {
            return Result.Straight;
        }
        else if (ThreeOfaKind(cards))
        {
            return Result.ThreeOfaKind;
        }
        else if (TwoPairs(cards))
        {
            return Result.TwoPairs;
        }
        else if (Pair(cards))
        { 
            return Result.Pair;   
        }
        else
            return Result.HighCard; 

    }


    public void ShowResult()
    {
        Ref_GamePlayUiManager.SetResult1_Text(TestResult(Ref_GamePlayUiManager.List1Call()).ToString());
        Ref_GamePlayUiManager.SetResult2_Text(TestResult(Ref_GamePlayUiManager.List2Call()).ToString());
        Ref_GamePlayUiManager.SetResult3_Text(TestResult(Ref_GamePlayUiManager.List3Call()).ToString());
    }
  
}
