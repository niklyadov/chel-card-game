using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager
{
    public float[] progresses = new float[4];

    public void ApplyChanges(float[] changes)
    {
        var rnd = new System.Random();
        for (int i = 0; i < changes.Length; i++)
        {
            if (i < progresses.Length)
                progresses[i] += (changes[i] == 0) ? 0 : rnd.Next(-5, 5) + changes[i];
        }

    }

}
