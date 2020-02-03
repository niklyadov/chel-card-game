using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualManager : MonoBehaviour
{
    // карта
    [SerializeField]
    private Text mainDescription;
    [SerializeField]
    private GameObject icon;

    //описание выбора
    [SerializeField]
    private Text choiceDescrioption;

    //показания параметров
    [SerializeField]
    private ValueBar[] parametres;

    //влияние параметров
    [SerializeField]
    private GameObject[] influences = new GameObject[4];

    // Панелька помощи
    [SerializeField]
    private GameObject HelpPanel;

    void Start()
    {
        Defines.VisManager = this;
    }

    public void ShowHelpMenu()
    {
        HelpPanel.SetActive(true);
        Defines.CardBehaviour.SetPause(true);
    }

    public void CloseHelpMenu()
    {
        HelpPanel.SetActive(false);
        Defines.CardBehaviour.SetPause(false);
    }

    public void UpdateMainCard(string sprite, string description)
    {
        mainDescription.text = description;
        icon.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(@"Sprites/" + sprite);
        Debug.Log("Card Updated " + description + ' ' + sprite);
    }

    public void UpdateDescription(string description)
    {
        choiceDescrioption.text = description;
        Debug.Log("description Updated " + description);
    }

    public void UpdateParametres()
    {
        var newParams = Defines.GameManager.progressManager.progresses;
        for (int i = 0; i < 4; i++)
            parametres[i].CurrentValue = newParams[i];
    }

    public void UpdateInfliuences(float[] influence)
    {
        for (int i = 0; i < influence.Length; i++)
        {
            Color color = influences[i].GetComponent<Image>().color;
            color.a = System.Math.Abs(influence[i]) * 0.1f;
        }
    }
}
