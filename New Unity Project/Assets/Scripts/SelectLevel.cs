using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.EventSystems;
public class SelectLevel : MonoBehaviour
{
    [SerializeField]
    private Image showImage;
    [SerializeField]
    private TMPro.TextMeshProUGUI showDescription;
    [SerializeField]
    private TMPro.TextMeshProUGUI showID;
    [SerializeField]
    private TMPro.TextMeshProUGUI showTitle;
    [SerializeField]
    private Image colorUnlocked;
    [SerializeField]
    private Sprite[] images = new Sprite[GameManager.levelNumber];
    [SerializeField]
    private string[] description = new string[GameManager.levelNumber];
    public enum enumProvince{
        POMORSKIE = 0,
        WARMIÑSKO_MAZURSKIE = 1,
        PODLASKIE = 2,
        MAZOWIECKIE = 3,
        LUBELSKIE = 4,
        PODKARPACKIE = 5,
        ŒWIÊTOKRZYSKIE = 6,
        MA£OPOLSKIE = 7,
        £ÓDZKIE = 8,
        ŒL¥SKIE = 9,
        OPOLSKIE = 10,
        DOLNOŒL¥SKIE = 11,
        WIELKOPOLSKIE = 12,
        ZACHODNIOPOMORSKIE = 13,
        KUJAWSKO_POMORSKIE = 14,
        LUBUSKIE = 15,
    }

    public void Select(int index)
    {
        var province = EventSystem.current.currentSelectedGameObject;

        showTitle.text = province.name;
        showID.text = index.ToString();
        showImage.sprite = images[index];
        Debug.Log(showDescription.text);
        showDescription.text = description[index];
        colorUnlocked.color = GameManager.instance.getlevelUnclockedPlayer()[index] ? Color.green : Color.green;

    }
}

public class Province
{
    int id;
    string name;
    string describe;
    bool isUnlocked;
}