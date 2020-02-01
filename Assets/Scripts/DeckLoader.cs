using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public static class DeckLoader
{
    public static Deck Load(string path)
    {
        if (!File.Exists(path))
            return null;

        var reader = new StreamReader(path, Encoding.UTF8);
        var content = reader.ReadToEnd();
        reader.Close();
        return Deck.CreateFromJSON(content);
    }


}
