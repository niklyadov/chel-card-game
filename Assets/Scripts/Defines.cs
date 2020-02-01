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

    static Defines()
    {
        WorkPath = Path.Combine("Assets", "Resources");
        CardsDataPath = Path.Combine(WorkPath, "cards.json");
        SpecialCardsDataPath = Path.Combine(WorkPath, "special_cards.json");
        SpritesPath = Path.Combine(WorkPath, "Sprites");
    }
}
