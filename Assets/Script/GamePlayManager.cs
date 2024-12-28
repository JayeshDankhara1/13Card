
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
    [SerializeField]
    public List<Card> cards = new List<Card>();
    public List<Card> cards1 = new List<Card>();
    public List<Card> cards2 = new List<Card>();
    public List<Card> cards3 = new List<Card>();


    public static GamePlayManager instance;

    public bool IsTrigar = false;
    public GameObject Collide_GameObject;
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
        if (StraightFlush(cards) && cards[0].Name == Name.Ace && cards[cards.Count - 1].Name == Name.King)
        {
            return true;
        }
        return false;
       
    }
    public bool StraightFlush(List<Card> cards)
    {
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
       return false;
    }

   
    //public List<Card> Sort(List<Card> cards)
    //{
    //    if (cards == null) return null;
    //    for (int i = 0; i < cards.Count-1; i++)
    //    {
    //        for (int j = i + 1; j < cards.Count; j++)
    //        {
    //            if (GetValue(cards[j].Name) > GetValue(cards[i].Name))
    //            {
    //                Card TempCard = new Card(cards[j].Color, cards[j].Name);
    //                cards[j] = cards[i];
    //                cards[i] = TempCard;
    //            }
    //        }
    //    }
    //        return cards;
    //}

    //public void CreatCard(Color color, Name name)
    //{
    //    Card TempCard = new Card(color,name);

    //    if (!cards.Any())
    //    {
    //        cards.Add(TempCard);
    //    }
    //    else if (!FindEliment(TempCard, cards))
    //    {
    //     cards.Add(TempCard);
    //       return;
    //    }
    //}

 
    public void CreatList()
    {

        //cards.Clear();
        // int CardName = 0;
        // int ColorIndx = 0;
       
       //while(cards.Count<13)
       {
             int ColorIndx =Random.Range(0, 4);
             int CardName = Random.Range(0, 13);

            
            
          //  CreatCard((Color)ColorIndx, (Name)CardName);

            /* CardName++;
             if (CardName == 12)
             {
                 CardName = 0;
                 ColorIndx++;
             }*/
           
      }
       // PartList();
    }

    public void PartList()
    {
        cards1.Clear();
        cards2.Clear();
        cards3.Clear();
        for (int i = 0; i < 13; i++)
        {
            if (i < 5)
            {
                cards1.Add(cards[i]);
            }
            else if (i < 10)
            {
                cards2.Add(cards[i]);
            }
            else
            {
                cards3.Add(cards[i]);
            }
        }
       // AllListSort();
    }
  

    public void test()
    {
        
       
       // Result(cards1);
       // Result(cards2);
        //Result(cards3);
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
  
}
