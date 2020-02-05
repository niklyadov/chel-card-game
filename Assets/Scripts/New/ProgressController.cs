using UnityEngine;

public class ProgressController : MonoBehaviour
{
    public BarController[] progresses;

    public void SetValues(float[] changes)
    {
        for (int i = 0; i < changes.Length; i++)
        {
            if (i < progresses.Length)
            {
                progresses[i].CurrentValue = changes[i];
            }
        }
    }

    public void ApplyChanges(float[] changes)
    {
        for (int i = 0; i < changes.Length; i++)
        {
            if (i < progresses.Length)
            {
                progresses[i].CurrentValue += (changes[i] == 0) ? 0 : Defines.Random.Next(-5, 5) + changes[i];
            }
        }
    }

    public float[] GetValues()
    {
        var result = new float[progresses.Length];

        for (int i = 0; i < progresses.Length; i++)
            result[i] = progresses[i].CurrentValue;

        return result;
    }
}
