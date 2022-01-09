using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    string URL = "https://student:student@foka.umg.edu.pl/~s44864/file10.php";

    void Start()
    {
        Debug.Log("" +URL);
        StartCoroutine(connectToDB());
    }
    IEnumerator connectToDB()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", "1114");
        form.AddField("systemsterowania", "12");
        form.AddField("nrpoziomu", "12");
        form.AddField("czas", "12");
        form.AddField("wynik", "12");
        form.AddField("bronze", "12");
        form.AddField("silver", "12");
        form.AddField("gold", "12");;
        form.AddField("lives", "12");;
        form.AddField("systemsterowania1", "22");
        form.AddField("nrpoziomu1", "22");
        form.AddField("czas1", "22");
        form.AddField("wynik1", "22");
        form.AddField("bronze1", "22");
        form.AddField("silver1", "22");
        form.AddField("gold1", "22");
        form.AddField("lives1", "22");
        form.AddField("systemsterowania2", "33");
        form.AddField("nrpoziomu2", "33");
        form.AddField("czas2", "33");
        form.AddField("wynik2", "33");
        form.AddField("bronze2", "33");
        form.AddField("silver2", "33");
        form.AddField("gold2", "33");
        form.AddField("lives2", "33");



        WWW www = new WWW(URL,form);

        yield return www;
        Debug.Log(www.text);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
