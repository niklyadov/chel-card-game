using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager
{
    public float[] progresses = new float[4];

    public void ApplyChanges(float[] changes)
    {

        for (int i = 0; i < changes.Length; i++)
        {

            if (i < progresses.Length)
                progresses[i] += changes[i];
        }

    }

}
