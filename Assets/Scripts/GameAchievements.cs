using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[SerializeField]
public class GameAchievements
{
    public bool[] Data = new bool[8];
    public static GameAchievements CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GameAchievements>(jsonString);
    }

    public string CreateJson()
    {
        return JsonUtility.ToJson(this);
    }

    public new string ToString()
    {
        return $"Получено достижений {Data.Count(c => c)} из {Data.Length}";
    }
}
