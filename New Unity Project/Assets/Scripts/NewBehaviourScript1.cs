using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript1 : MonoBehaviour
{
    string URL = "https://student:student@foka.umg.edu.pl/~s44864/file6.php";



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD");
        Debug.Log("" + URL);
        StartCoroutine(connectToDB());
    }
    IEnumerator connectToDB()
    {
        WWWForm form = new WWWForm();


        WWW www = new WWW(URL, form);

        yield return www;
        Debug.Log(www.text);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
