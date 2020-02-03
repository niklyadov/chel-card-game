using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Card
{
    public string Text;
    public string Icon;

    public float[] Left = new float[0];
    public float[] Right = new float[0];

    public string LeftLabel;
    public string RightLabel;

    public override string ToString()
    {
        return string.Format("{0}, {1} {2}  {3} {4}", Text, LeftLabel, RightLabel, string.Join(" ", Left), string.Join(" ", Right));
    }
}
