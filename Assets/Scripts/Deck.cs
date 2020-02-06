using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Deck
{
    public Card[] CardList;
    
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
        return CardList[Defines.Random.Next(0, CardList.Length)];
    }

    public Card GetCard(int id)
    {
        return id > 0 && id < CardList.Length ? CardList[id] : null;
    }
}
