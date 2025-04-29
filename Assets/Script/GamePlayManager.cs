
using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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
    #region varibal and Refrance

    [HideInInspector]
    public static GamePlayManager instance;
    [HideInInspector]
    public GamePlayUiManager Ref_GamePlayUiManager;
    [HideInInspector]
    public Animation Ref_Animation;
    [HideInInspector]
    public GoogleAds Ref_GoogleAds;
    [HideInInspector]
    public List<Card> ResultCardList = new List<Card>();

    int Result1_Score=0, Result2_Score=0, Result3_Score=0;
    #endregion

    #region Unity Function

    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        Ref_Animation = Animation.instance;
        Ref_GoogleAds = GoogleAds.instance;
        Ref_GamePlayUiManager = GamePlayUiManager.Instance;
        GameSatrt();
    }
    #endregion


    #region other Function
    public void ShowResult()
    {

        Change_AllBg();
        Ref_GamePlayUiManager.SetScore_Text(0);
        ResultCardList.Clear();


        Result result1 = TestResult(Ref_GamePlayUiManager.List1Call());
        Ref_GamePlayUiManager.SetResult1_Text(result1.ToString());
        HighLiteCard(ResultCardList);
        Result1_Score = CalculetScore(ResultCardList) + GetResultScore(result1);
        Ref_GamePlayUiManager.SetScore1_Text(Result1_Score);

        ResultCardList.Clear();
        Result result2 = TestResult(Ref_GamePlayUiManager.List2Call());
        Ref_GamePlayUiManager.SetResult2_Text(result2.ToString());
        HighLiteCard(ResultCardList);
        Result2_Score = CalculetScore(ResultCardList) + GetResultScore(result2);
        Ref_GamePlayUiManager.SetScore2_Text(Result2_Score);

        ResultCardList.Clear();
        Result result3 = TestResult(Ref_GamePlayUiManager.List3Call());
        Ref_GamePlayUiManager.SetResult3_Text(result3.ToString());
        HighLiteCard(ResultCardList);
        Result3_Score = CalculetScore(ResultCardList) + GetResultScore(result3);
        Ref_GamePlayUiManager.SetScore3_Text(CalculetScore(ResultCardList) + GetResultScore(result3));

        Ref_GamePlayUiManager.SetScore_Text(Result1_Score + (Result2_Score * 2) + (Result3_Score * 3));
        Change_Bg(Result1_Score, Result2_Score, Result3_Score);


    }

    public bool Check_Score()
    {
        if (Result1_Score > Result2_Score && Result2_Score > Result3_Score)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public IEnumerator Coundown(int Time)
    {
        while (Time > 0)
        {
            Ref_GamePlayUiManager.StopWatch(Time);
            yield return new WaitForSeconds(1);
            Time--;
        }
        Ref_GamePlayUiManager.GameOver("You Are Loss!");
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
        {
            HighCard(cards);
            return Result.HighCard;
        }

    }


    public bool FindJoker(List<Card> cards)
    {
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].Name == Name.Joker)
            {
                return true;
            }
        }
        return false;
    }

    public void SwapCard(Card card1, Card card2)
    {
        Color tempColor = card1.Color;
        Name tempName = card1.Name;

        card1.Color = card2.Color;
        card1.Name = card2.Name;

        card2.Color = tempColor;
        card2.Name = tempName;

    }
    public IEnumerator start()
    {

        Ref_GamePlayUiManager.GameSatrt();
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
        StartCoroutine(Coundown(180));
        Ref_GamePlayUiManager.HedarParentSetActive(true);
        Ref_GamePlayUiManager.TabelSetActive(true);
        StartCoroutine(CardDeail());
        yield return new WaitForSeconds(3.7f);
        Ref_GamePlayUiManager.LoadCard();
        Ref_GamePlayUiManager.AllCardListUpdate();
        Ref_GamePlayUiManager.start(true);
        Ref_GoogleAds.ShowBannerAd();

    }

    public void GameSatrt()
    {
        Ref_GamePlayUiManager.GameStart();
        StartCoroutine(start());
    }
    public void GameOver()
    {
        StopAllCoroutines();
        Ref_GamePlayUiManager.start(false);
        Ref_GamePlayUiManager.Restart(true);
    }

    public IEnumerator CardDeail()
    {
        for (int i = 0; i < 13; i++)
        {

            Ref_Animation.Movecard(Ref_GamePlayUiManager.Card_GameObjects[i].GetComponent<RectTransform>(), Ref_GamePlayUiManager.Card_GameObjects[i].transform.localPosition, 1f, OnStart: () => {

                Ref_GamePlayUiManager.ActiveObject(Ref_GamePlayUiManager.Card_GameObjects[i]);
            });
            yield return new WaitForSeconds(0.2f);
        }
    }
    #endregion


    #region Get Method Reletad Funtion

    public int CalculetScore(List<Card> cards)
    {
        int TempScore = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            TempScore += GetCardScore(cards[i].Name);
        }
        return TempScore;
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

    public int GetCardScore(Name name)
    {

        switch (name)
        {
            case Name.Ace:
                return 150;
            case Name.Two:
                return 20;
            case Name.Three:
                return 30;
            case Name.Four:
                return 40;
            case Name.Five:
                return 50;
            case Name.Six:
                return 60;
            case Name.Seven:
                return 70;
            case Name.Eight:
                return 80;
            case Name.Nine:
                return 90;
            case Name.Ten:
                return 100;
            case Name.Jack:
                return 110;
            case Name.Queen:
                return 120;
            case Name.King:
                return 130;
            case Name.Joker:
                return 150;
        }
        return 0;
    }

    public int GetResultScore(Result name)
    {
        return (int)name;
    }


    public int GetColor(Color color)
    {
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

    #endregion

    #region Rulse Define Function
    public bool RoyalFlush(List<Card> cards)
    {
        ResultCardList.Clear();
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));

        if (cards[0].Color == Color.Spades && cards.Find(c=> c.Name==Name.King) && !cards.Find(c => c.Name == Name.Ace) && StraightFlush(cards))
        {
            ResultCardList.AddRange(cards);
            return true;
        }
        return false;
       
    }
    public bool StraightFlush(List<Card> cards)
    {
        ResultCardList.Clear();
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));
        if (Flush(cards) && Straight(cards))
        {
            ResultCardList.AddRange(cards);
            return true;
        }
        return false;
    }
    public bool FourOfaKind(List<Card> cards)
    {
        if (cards.Count < 5)
            return false;  

        ResultCardList.Clear(); 

        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name)); 

        int jokerCount = cards.Count(card => card.Name == Name.Joker); 

        var groupedCards = cards.GroupBy(c => c.Name);  
     
        foreach (var group in groupedCards)
        {
            if (group.Count() == 4)
            {
           
                ResultCardList.AddRange(group);
                return true;  
            }
            else if (group.Count() == 3 && jokerCount > 0)
            {
            
                ResultCardList.AddRange(group); 
                ResultCardList.Add(new Card(Color.Null,Name.Joker));  
                return true; 
            }
        }
        return false;

    }

   
    public bool FullHouse(List<Card> cards)
    {
        if (cards.Count < 5)
            return false;  

        ResultCardList.Clear();  
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));  

        int jokerCount = cards.Count(card => card.Name == Name.Joker);  

        var groupedCards = cards.GroupBy(c => c.Name);  

        bool hasThree = false;
        bool hasTwo = false;

      
        List<Card> threeOfAKindCards = new List<Card>();
        List<Card> pairCards = new List<Card>();

        foreach (var group in groupedCards)
        {
            if (group.Count() == 3)
            {
                hasThree = true;
                threeOfAKindCards.AddRange(group);  
            }
            else if (group.Count() == 2)
            {
                hasTwo = true;
                pairCards.AddRange(group); 
            }
        }

        
        if(FindJoker(cards))
        {
            int pairCount = 0;
            foreach (var group in groupedCards)
            {
                if (group.Count() == 2)
                {
                    pairCount++;

                }
                else if (pairCount >= 2)
                {
                    hasThree = true;
                    hasTwo = true;
                }


            }
        }

        if (hasThree && hasTwo)
        {
            
            ResultCardList.AddRange(cards);
            return true;
        }

        return false;  
    }
    public bool Flush(List<Card> cards)
    {
        if (cards.Count < 5)
            return false;
        ResultCardList.Clear();
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));

        for (int i = 0; i < cards.Count-1; i++)
        {
            if (cards[i].Color != cards[i + 1].Color)
            {
                if (cards[i + 1].Name == Name.Joker)
                {
                    continue;
                }
                 return false;
            }
           
        }
        ResultCardList.AddRange(cards);
        return true;
    }
    public bool Straight(List<Card> cards)
    {
        if (cards.Count < 5) return false;

        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));
        ResultCardList.Clear();

        bool isJokerUsed = false;
        bool isAceLow = false;

       
        for (int i = 1; i < cards.Count; i++)
        {
         
            if (cards[i].Name == cards[i - 1].Name + 1)
            {
                continue;
            }
            
            else if (cards[i].Name == Name.Joker || (FindJoker(cards) &&!isJokerUsed && cards[i].Name == cards[i - 1].Name + 2))
            {
                isJokerUsed = true;
                continue;
            }
            
            else if (cards[i].Name == Name.Ace && (cards[0].Name == Name.Two || cards[0].Name == Name.Three || cards[0].Name == Name.Four || cards[0].Name == Name.Five || cards[0].Name == Name.Six) && !isAceLow)
            {
                isAceLow = true;
                continue;
            }
          
            else
            {
                return false;
            }
        }
        ResultCardList.AddRange(cards);
        return true;
    }


    public bool ThreeOfaKind(List<Card> cards)
    {
    
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));
        var groupedCards = cards.GroupBy(c => c.Name);
        ResultCardList.Clear();

        foreach (var group in groupedCards)
        {
        
            if (group.Count() == 3)
            {
                ResultCardList.AddRange(group);
                return true; 
            }
           
            else if (group.Count() == 2 && FindJoker(cards))
            {
                ResultCardList.AddRange(group);
                ResultCardList.Add(new Card(Color.Null, Name.Joker));
                return true; 
            }
        }

  
        return false;

    }
    public bool TwoPairs(List<Card> cards)
    {
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));
        var groupedCards = cards.GroupBy(c => c.Name);
        ResultCardList.Clear();
        int pairCount = 0;
        bool IsJockUse = false;
        foreach (var group in groupedCards)
        {
         
            if (group.Count() == 2)
            {
                pairCount++;
                ResultCardList.AddRange(group); 
            }
           
            else if (pairCount == 1  && FindJoker(cards) && !IsJockUse)
            {
                IsJockUse = true;
                pairCount++;
                ResultCardList.Add(group.First());
            }
            if (pairCount >= 2)
            {
                return true;
            }
        }
        return false;
    }

    public bool Pair(List<Card> cards)
    {
        cards.Sort((card1, card2) => card1.Name.CompareTo(card2.Name));

        var groupedCards = cards.GroupBy(c => c.Name);
        ResultCardList.Clear();

        foreach (var group in groupedCards)
        {
            if (group.Count() == 2)
            {
                ResultCardList.AddRange(group);
                return true;
            }
            else if(group.Count() == 1 && FindJoker(cards) ) 
            {
                ResultCardList.Add(cards[cards.Count-2]);
                ResultCardList.Add(new Card(Color.Null,Name.Joker));
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
    #endregion

    #region Highlite Releted Function

    public void HighLiteCard(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            for (int j = 0; j < Ref_GamePlayUiManager.Card_GameObjects.Count; j++)
            {
                if (cards[i].Name == Ref_GamePlayUiManager.Card_GameObjects[j].GetComponent<Card>().Name && cards[i].Color == Ref_GamePlayUiManager.Card_GameObjects[j].GetComponent<Card>().Color)
                {
                    Ref_GamePlayUiManager.GetImage_Sorce(Ref_GamePlayUiManager.Card_GameObjects[j]).color = Ref_GamePlayUiManager.Y_Color;
                }
            }
        }
    }

    public void Change_Bg(int s1, int s2, int s3)
    {
        if (s1 < s2)
        {
            Ref_GamePlayUiManager.GetImage_Sorce2().color = Ref_GamePlayUiManager.R_Color;
        }
        if (s2 < s3)
        {
            Ref_GamePlayUiManager.GetImage_Sorce3().color = Ref_GamePlayUiManager.R_Color;
        }
    }


    public void Change_AllBg()
    {
        Ref_GamePlayUiManager.GetImage_Sorce1().color = Ref_GamePlayUiManager.G_Color;
        Ref_GamePlayUiManager.GetImage_Sorce2().color = Ref_GamePlayUiManager.G_Color;
        Ref_GamePlayUiManager.GetImage_Sorce3().color = Ref_GamePlayUiManager.G_Color;
    }


    #endregion


}
