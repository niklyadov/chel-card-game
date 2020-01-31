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

    /*
    public string CreateToJSON()
    {
        return JsonUtility.ToJson(this);
    }
    */
}
