using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public static class DeckLoader
{
    public static Deck Load()
    {
        return Deck.CreateFromJSON(File.ReadAllText(Defines.CardsDataPath));
    }


}
