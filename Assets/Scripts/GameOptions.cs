using UnityEngine;

[SerializeField]
public class GameOptions
{
    public bool  MuteBackgroundSound; // заглушить фоновый звук
    public bool  MuteOnListSound;     // заглушить звук при листании
    public short FpsLock = 60;        // ограничение fps

    public static GameOptions CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<GameOptions>(jsonString);
    }

    public string CreateJson()
    {
        return JsonUtility.ToJson(this);
    }
}