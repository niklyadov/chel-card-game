using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class Defines
{
    public static string CardsDataPath;
    public static string WorkPath;
    public static string SpritesPath;
    public static ProgressVisual progresVisual;
    public static VisualManager VisManager;

    public static GameManager GameManager;

    static Defines()
    {
        WorkPath = Path.Combine("Assets", "Resources");
        CardsDataPath = Path.Combine(WorkPath, "cards.json");
        SpritesPath = Path.Combine(Defines.WorkPath, "Sprites");
    }
}
