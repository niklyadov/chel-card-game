using UnityEngine;

public class AnimationsController : MonoBehaviour
{
    [SerializeField]
    Animator[] animators;

    void Start()
    {
        GameController.ApplyChoice[(int)GameMode.Default] += PlayAnimations;
    }

    // Update is called once per frame
    void PlayAnimations(CardPosition position)
    {
        //выбираем соответствующий массив "последствий" для текущей карты
        float[] valuesToAdd = new float[4];
        if (position == CardPosition.OnLeft)
            valuesToAdd = GameController.CurrentCard.Left;
        else if (position == CardPosition.OnRight)
            valuesToAdd = GameController.CurrentCard.Right;

        for (int i = 0; i < valuesToAdd.Length; i++)
        {
            if (valuesToAdd[i] > 0)
                animators[i].SetBool("plus", true);
            else if (valuesToAdd[i] < 0)
                animators[i].SetBool("minus", true);
        }
    }
}