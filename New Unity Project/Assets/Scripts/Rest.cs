using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : MonoBehaviour
{
    string URL = "https://student:student@foka.umg.edu.pl/~s44864/file10.php";

   public  void request(string id, string systemsterowania, string nrpoziomu, float czas, string wynik,
        string systemsterowania1, string nrpoziomu1, string czas1, string wynik1,
        string systemsterowania2, string nrpoziomu2, string czas2, string wynik2)
    {
        StartCoroutine(connectToDB(id,systemsterowania,nrpoziomu,czas,wynik,systemsterowania1,nrpoziomu1,czas1,wynik1,systemsterowania2,nrpoziomu2,czas2,wynik2));
    }

    IEnumerator connectToDB(string id, string systemsterowania,string nrpoziomu,float czas,string wynik,
        string systemsterowania1, string nrpoziomu1, string czas1, string wynik1, 
        string systemsterowania2, string nrpoziomu2, string czas2, string wynik2)
    {
        WWWForm form = new WWWForm();
        form.AddField("id", id);
        form.AddField("systemsterowania", systemsterowania);
        form.AddField("nrpoziomu", nrpoziomu);
        form.AddField("czas",czas.ToString().Replace(',','.'));
        form.AddField("wynik", wynik);
        //       form.AddField("bronze", bronze);
        //       form.AddField("silver", silver);
        //       form.AddField("gold", gold);;
        //       form.AddField("lives", lives);;
        form.AddField("systemsterowania1", systemsterowania1);
        form.AddField("nrpoziomu1", nrpoziomu1);
        form.AddField("czas1", czas1.ToString().Replace(',', '.'));
        form.AddField("wynik1", wynik1);
        //       form.AddField("bronze1", "22");
        //        form.AddField("silver1", "22");
        //       form.AddField("gold1", "22");
        //        form.AddField("lives1", "22");
        form.AddField("systemsterowania2", systemsterowania2);
        form.AddField("nrpoziomu2", nrpoziomu2);
        form.AddField("czas2", czas2.ToString().Replace(',', '.')); 
        form.AddField("wynik2", wynik2);
        //      form.AddField("bronze2", "33");
        //      form.AddField("silver2", "33");
        //      form.AddField("gold2", "33");
        //      form.AddField("lives2", "33");



        WWW www = new WWW(URL,form);

        yield return www;
        Debug.Log(www.text);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
