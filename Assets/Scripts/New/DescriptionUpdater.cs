using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DescriptionUpdater : MonoBehaviour
{
	[SerializeField]
	private Text Text;

	void Awake()
	{
		GameController.SwitchDescription = UpdateData; // при изменении карты
	}

	void UpdateData(CardPosition position)
	{
        if (GameController.CurrentCard != null)
        {
            Text.text = (position == CardPosition.OnLeft) ? GameController.CurrentCard.LeftLabel : GameController.CurrentCard.RightLabel;
        }
    }
}
