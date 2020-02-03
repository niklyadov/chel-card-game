using System;
using UnityEngine;
using UnityEngine.UI;

enum AnimationDirection
{
	Plus,
	Minus,
	Idle
}

public class ValueBar : MonoBehaviour
{
	private float currentValue;

	[SerializeField]
    private Text text;

	[SerializeField]
    private Image image;

    private float animValue;
    private float animMax;
    private AnimationDirection currentDir;

	public float CurrentValue
	{
		get
		{
			return currentValue;
		}
		set
		{
			var deltaValue = currentValue - value;

			if(deltaValue > 0) SetAnimation(AnimationDirection.Plus);
			else if(deltaValue < 0) SetAnimation(AnimationDirection.Minus);

			currentValue = value;
			text.text = Math.Round(currentValue).ToString() + '%';
		}
	}

	void Start()
	{
		currentDir = AnimationDirection.Idle;
		animMax = 100f;
	}

   	void Update()
   	{
   		if (Equals(currentDir, AnimationDirection.Idle))
		   return; // выходим если анимация простоя

		if (animValue < 0)
		{
			animValue = animMax;
			currentDir = AnimationDirection.Idle;
			return;
		}

		animValue -= 0.7f;
		PrepaireAnimation(currentDir, animValue);
   	}

	private void PrepaireAnimation(AnimationDirection direction, float value)
	{
		if(direction == AnimationDirection.Plus)
		{
			image.fillAmount = value / 100;
			Color color = image.color;
			color.a = value / 100;
			image.color = color;
		}
		else if(direction == AnimationDirection.Minus)
		{
			image.fillAmount = 1 - (value / 100);
			Color color = image.color;
			color.a = value / 100;
			image.color = color;
		}
	}

	private void SetAnimation (AnimationDirection direction)
	{
		animValue = animMax;
		currentDir = direction;

		if(direction == AnimationDirection.Minus)
		{
			image.fillAmount = 1;
			image.color = Color.green;
		}
		else if(direction == AnimationDirection.Plus)
		{
			image.fillAmount = 0;
			image.color = Color.red;
		}
	}
}
