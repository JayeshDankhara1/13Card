using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public enum Color
{
    Diamonds,
    Clubs,
    Hearts,
    Spades
}
[Serializable]
public enum Name
{
    Ace,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King
}
[Serializable]
public class Card : MonoBehaviour
{ 
    public Color Color;
    public Name Name;

   public Card(Color color, Name name)
   {
        this.Color = color;
        this.Name = name;
   }    
}
