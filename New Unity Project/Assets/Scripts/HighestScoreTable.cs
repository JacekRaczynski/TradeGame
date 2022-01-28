using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighestScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private float templateHight;
    private static List<Transform> list = new List<Transform>();
    private void Start()
    {
        entryContainer = transform.Find("highestScoreEntryContainer");
        entryTemplate = transform.Find("highestScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);
        templateHight = 30f;
        setValue(PlayerPrefs.GetInt("Control", 0));

    }
    public void setValue(int index)
        {
        if (GameManager.instance != null)
        {
            int[] controledSelected;
            float[] time;
            int[] highestScore;
            if (index == 0)
            {
                controledSelected = GameManager.instance.getControlerSelected();
                time = GameManager.instance.getTime();
                highestScore = GameManager.instance.getHighestScore();
            }
            else if (index == 1)
            {
                controledSelected = GameManager.instance.getControlerSelected1();
                time = GameManager.instance.getTime1();
                highestScore = GameManager.instance.getHighestScore1();
            }
          else
            {
                controledSelected = GameManager.instance.getControlerSelected2();
                time = GameManager.instance.getTime2();
                highestScore = GameManager.instance.getHighestScore2();
            }
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Debug.Log(list.Count);
                    //   GameObject.Destroy(list[i].gameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().gameObject);
                    //GameObject.Destroy(list[i].gameObject.GetComponent<RectTransform>().gameObject);
                    //  GameObject.Destroy(list[i].gameObject);
                    list[i].gameObject.SetActive(false); //works

               //     Destroy((list[i].gameObject));
                }
            }
            catch (Exception e)
            {
                print("error");

            }


            for (int i = 0; i < GameManager.levelNumber; i++)
            {

                Transform entryTransform = Instantiate(entryTemplate, this.gameObject.transform);
                list.Add(entryTransform);
                RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
                entryRectTransform.anchoredPosition = new Vector2(0, -templateHight * i);

                entryTemplate.gameObject.SetActive(true);
                entryTemplate.Find("lvlText").GetComponent<TMPro.TextMeshProUGUI>().text = (i + 1).ToString();
                entryTemplate.Find("controlText").GetComponent<TMPro.TextMeshProUGUI>().text = controledSelected[i].ToString();
                entryTemplate.Find("timeText").GetComponent<TMPro.TextMeshProUGUI>().text = time[i].ToString();
                entryTemplate.Find("highestScore").GetComponent<TMPro.TextMeshProUGUI>().text = highestScore[i].ToString();
                if (i == GameManager.levelNumber - 1)
                {
                    entryTemplate.Find("lvlText").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                    entryTemplate.Find("controlText").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                    entryTemplate.Find("timeText").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                    entryTemplate.Find("highestScore").GetComponent<TMPro.TextMeshProUGUI>().text = "";
                }
            }
        }
    }


    
}
