using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Deck
{
    public Card[] CardList;
    public Card[] SpecialCardList;
    System.Random rnd = new System.Random();

    public static Deck CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Deck>(jsonString);
    }

    public string CreateJson()
    {
        return JsonUtility.ToJson(this);
    }

    public Card GetRandom() 
    {
        return CardList[rnd.Next(0, CardList.Length)];
    }
}
