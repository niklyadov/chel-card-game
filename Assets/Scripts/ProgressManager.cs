using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager
{
    Progress[] progresses = new Progress[] {
        
        new Progress(),

        new Progress(),

        new Progress(),

        new Progress()

    };

    public void ApplyChanges(float[] changes)
    {

        for (int i = 0; i < changes.Length; i++)
        {
            if (i < progresses.Length)
                progresses[i].AddValue = changes[i];
        }

    }

}
