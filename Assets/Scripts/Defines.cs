using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Defines
{
    public static string CardsDataPath;
    public static string SpecialCardsDataPath;
    public static string WorkPath;
    public static string SpritesPath;
    public static VisualManager VisManager;
    public static GameManager GameManager;

    public static string Path
    {
        get
        {
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                    return System.IO.Path.Combine(Application.persistentDataPath, "Assets");

                case RuntimePlatform.Android:
                    return System.IO.Path.Combine(Application.temporaryCachePath, "Assets");

                default:
                    return System.IO.Path.Combine(Directory.GetParent(Application.dataPath).FullName, "Assets");
            }
        }
    }
}
