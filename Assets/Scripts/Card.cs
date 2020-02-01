using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Par
{
    Society,
    Money,
    Ecology,
    Administration
}

public class Card
{
    public string Text;
    public string Icon;

    public float[] Left;
    public float[] Right;

    public string LeftLabel;
    public string RightLabel;
}
