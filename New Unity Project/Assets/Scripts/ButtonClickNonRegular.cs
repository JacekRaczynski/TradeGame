using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonClickNonRegular : MonoBehaviour
{
    public float alphaMinimumThreshold = 0.1f;
    public string name;
    public int number;

    private void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = alphaMinimumThreshold;
    }
}
