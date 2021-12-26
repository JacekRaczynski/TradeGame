using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighestScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    private void Start()
    {
        entryContainer = transform.Find("highestScoreEntryContainer");
        entryTemplate = transform.Find("highestScoreEntryTemplate");
        entryTemplate.gameObject.SetActive(false);
        float templateHight = 30f;
        if (GameManager.instance != null)
        {
            int[] controledSelected = GameManager.instance.getControlerSelected();
            float[] time = GameManager.instance.getTime();
            int[] highestScore = GameManager.instance.getHighestScore();
            for (int i = 0; i < GameManager.levelNumber; i++)
            {

                Transform entryTransform = Instantiate(entryTemplate, this.gameObject.transform);
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
