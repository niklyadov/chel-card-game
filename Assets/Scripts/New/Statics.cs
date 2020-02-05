using System;

public enum CardPosition
{
    OnLeft,
    OnRight,
    Passive
}

public static class Statics
{
    public static Random Random = new System.Random();
    public static GameController GameController;
}
