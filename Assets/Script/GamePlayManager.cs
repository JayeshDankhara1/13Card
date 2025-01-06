
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using System;
using Unity.Collections.LowLevel.Unsafe;
using System.Diagnostics.Contracts;
using System.Collections.ObjectModel;
using UnityEngine.UIElements;

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


    public List<Card> ResultCardList = new List<Card>();    

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
        if(cards.Count < 5) return false;
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));
        ResultCardList.Clear();
        if (FindJoker(cards) && FindAce(cards))
        {
            
            bool IsSkip = true;
            if (FindTwo(cards))
            { 
                for(int i = 0; i < cards.Count-1;i++)
                {
                    if (cards[i - 1].Name == cards[i].Name - 1)
                    {
                        continue;
                    }
                    else if (cards[i - 1].Name + 1 == cards[i].Name - 1 && IsSkip)
                    {
                        IsSkip = false;
                        continue;
                    }
                    else if (cards[i].Name == Name.Ace && i==3)
                    {
                        ResultCardList = cards;
                       return true;
                    }
                    else 
                    {
                        return false;
                    }
                }
                ResultCardList = cards;
                return true ;
            }

            IsSkip = true;
            for (int i = 1;i < cards.Count-1;i++)
            {
                if (cards[i - 1].Name == cards[i].Name - 1)
                {
                    continue;
                }
                else if (cards[i - 1].Name + 1 == cards[i].Name - 1 && IsSkip)
                {
                    IsSkip = false;
                    continue;
                }
                else
                { 
                    return false ;
                }
            }
            ResultCardList = cards;
            return true;

        }
        else if (FindAce(cards))
        {
            Debug.Log("Fast Condtion Active");
            if (FindTwo(cards))
            {
                Debug.Log("Secound Condtion Active");
                for (int i = 0; i < cards.Count - 2; i++)
                {
                    if (cards[i].Name + 1 == cards[i + 1].Name)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                ResultCardList = cards;
                return true;
            }
            else 
            {
                for (int i = 0; i < cards.Count - 1; i++)
                {
                    if (cards[i].Name + 1 == cards[i + 1].Name)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                ResultCardList = cards;
                return true;
            }
            
        }
        else if (FindJoker(cards))
        {
            bool IsSkip = true;
            for (int i = 1; i < cards.Count-1; i++)
            {

                if (cards[i - 1].Name == cards[i].Name - 1)
                {
                    continue;
                }
                else if (cards[i - 1].Name + 1 == cards[i].Name - 1 && IsSkip)
                {
                    IsSkip = false;
                    continue;
                }
                else
                {
                    return false;
                }

            }
            ResultCardList = cards;
            return true;
        }
        else
        {
            for (int i = 1; i < cards.Count; i++)
            {

                if (cards[i - 1].Name == cards[i].Name - 1)
                {
                    continue;
                }
                else
                {
                    return false;
                }
                
            }
            ResultCardList = cards;
            return true;
        }
    }



    public bool ThreeOfaKind(List<Card> cards)
    {
          cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));
          ResultCardList.Clear();
        if (FindJoker(cards))
        {
            if (Pair(cards, false))
            {
                ResultCardList.Add(cards[cards.Count - 1]);
                return true;
            }
        }
       
            var rankGroups = cards
            .GroupBy(card => card.Name)  
            .Where(group => group.Count() == 3) 
            .ToList();

        if (rankGroups.Any())
        {
            ResultCardList=rankGroups.First().ToList();
            return true;
        }


        return false;


    }
    public bool TwoPairs(List<Card> cards)
    {
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));
        ResultCardList.Clear();
        int Count = 0;
        if (FindJoker(cards))
        {
            if (Pair(cards, false))
            {
                ResultCardList.Add(cards[cards.Count - 1]);
                for (int i = 0; i < ResultCardList.Count; i++)
                {
                    for (int j = cards.Count - 1; j >= 0; j--)
                    {
                        if (ResultCardList[i].Name != cards[i].Name)
                        {
                            ResultCardList.Add(cards[i]);
                            return true;
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 1; i < cards.Count; i++)
            {
                if (cards[i-1].Name == cards[i].Name)
                {
                    ResultCardList.Add(cards[i]);
                    ResultCardList.Add(cards[i-1]);
                    Count++;
                    i++;
                    if (Count == 2)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public bool Pair(List<Card> cards,bool IsJock =true)
    {
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));
        ResultCardList.Clear();
        if (FindJoker(cards) && IsJock) {
            ResultCardList.Add(cards[cards.Count-1]);
            ResultCardList.Add(cards[cards.Count-2]);
            return true;
        }

        for (int i = 1; i < cards.Count; i++) {

                if (cards[i-1].Name == cards[i].Name)
                {
                    ResultCardList.Add(cards[i]);
                    ResultCardList.Add(cards[i-1]);
                return true;
                }
        }
        return false ;

    }

    public bool FindJoker(List<Card> cards)
    {
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));
        if (cards.Last().Name == Name.Joker)
        { 
        return true; 
        }
      return false;
    }
    public bool FindKing(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].Name == Name.King)
            {
                return true;
            }
        }
        return false;

    }

    public bool FindTwo(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].Name == Name.Two)
            {
                return true;
            }
        }
        return false;

    }
    public bool FindAce(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].Name == Name.Ace)
            {
                return true;
            }
        }
        return false;
    }
    public bool HighCard(List<Card> cards)
    {
        ResultCardList.Clear();
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));

       ResultCardList.Add(cards.Last());
        
        return true;
    }


  

    public Result TestResult(List<Card> cards)
    {
        // bool IsRoyalFlush;
        // List<Card> ListRoyalFlush = new List<Card>();
        // (IsRoyalFlush, ListRoyalFlush) = Pair(cards);

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
        {
            HighCard(cards);
            return Result.HighCard;
        }
    }


    public void HighLiteCard(List<Card> cards)
    {
        //for (int j = 0; j < ResultCardList.Count; j++)
        //{
        //    for (int i = 0; i < cards.Count; i++)
        //    {
        //        if (ResultCardList[j].Name == cards[i].Name && ResultCardList[j].Color == cards[i].Color)
        //        {
        //            cards[i].gameObject.transform.localScale = Vector3.one *1.2f;
        //        }
        //    }
        //}
    }
    

    public void ShowResult()
    {
        Ref_GamePlayUiManager.SetResult1_Text(TestResult(Ref_GamePlayUiManager.List1Call()).ToString());
        HighLiteCard(Ref_GamePlayUiManager.List1Call());
        Ref_GamePlayUiManager.SetResult2_Text(TestResult(Ref_GamePlayUiManager.List2Call()).ToString());
        HighLiteCard(Ref_GamePlayUiManager.List2Call());
        Ref_GamePlayUiManager.SetResult3_Text(TestResult(Ref_GamePlayUiManager.List3Call()).ToString());
        HighLiteCard(Ref_GamePlayUiManager.List3Call());
    }
  
}
