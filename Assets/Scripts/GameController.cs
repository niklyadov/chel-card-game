using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode 
{
    Default,
    Labirint
}

public static class GameController
{
    public static Action<CardPosition>[] SwitchDescription = new Action<CardPosition>[2];
    public static Action<CardPosition>[] ApplyChoice       = new Action<CardPosition>[2];
    public static Action<BarPosition> [] BarStateChange    = new Action<BarPosition>[2];
    
    public static Action<Card> CardUpdate;

    public static GameMode GameMode = GameMode.Default;

    public static Card CurrentCard;
    public static float[] Progress = { 50, 30, 50, 50 };

    private static readonly Deck _deck;
    private static readonly Deck _specialDeck;

    public static readonly GameStorageController GameStorage;

    static GameController ()
    {
        GameStorage = new GameStorageController();

        if (GameStorage.Options.FpsLock > 0)
        {
            Application.targetFrameRate = GameStorage.Options.FpsLock;
        }

        _deck        = Deck.CreateFromJSON(Resources.Load<TextAsset>("cards").text);
        _specialDeck = Deck.CreateFromJSON(Resources.Load<TextAsset>("special_cards").text);

        if (_deck == null || _specialDeck == null)
        {
            throw new Exception("Error while load deck");
        }

        CurrentCard = _specialDeck.CardList[0];
    }

    public static void ResetValues(float[] values = null) 
    {
        values = values ?? (new float[] { 50, 30, 50, 50 });
        Progress = values;
    }

    public static void ChooseNewCard()
    {
        if (ProgressIsNormal())
            CurrentCard = _deck.GetRandom();
        else
        {
            Progress = new float[] { 50, 30, 50, 50 };
        }

        CardUpdate(CurrentCard);
    }

    private static bool ProgressIsNormal()
    {
        var ok = false;

        if (Progress[0] <= 0) //параметр
            HandleEnding(1);
        else if (Progress[0] >= 100)
            HandleEnding(2);
        else if (Progress[1] <= 0)
            HandleEnding(3);
        else if (Progress[1] >= 100)
            HandleEnding(4);
        else if (Progress[2] <= 0)
            HandleEnding(5);
        else if (Progress[2] >= 100)
            HandleEnding(6);
        else if (Progress[3] <= 0)
            HandleEnding(7);
        else if (Progress[3] >= 100)
            HandleEnding(8);
        else
            ok = true;

        return ok;
    }

    private static void HandleEnding(int id)
    {
        CurrentCard = _specialDeck.GetCard(id); //номер концовки
        GameStorage.Achievements.Data[id-1] = true;
        GameStorage.UpdateAchievements();
    }
}