using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
enum Par
{
    Society,
    Money,
    Ecology,
    Administration
}
*/

[Serializable]
public class Card
{
    public string Text;
    public string Icon;

    public float[] Left;
    public float[] Right;

    public string LeftLabel;
    public string RightLabel;

    public override string ToString()
    {
        return string.Format("{0}, {1} {2}  {3} {4}", Text, LeftLabel, RightLabel, string.Join(" ", Left), string.Join(" ", Right));
    }
}
