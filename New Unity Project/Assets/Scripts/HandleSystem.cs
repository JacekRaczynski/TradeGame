using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponInfo", menuName = "HandleSystem", order = 0)]
public class HandleSystem : ScriptableObject
{
    [SerializeField]
    private float[] price; // wiersz kolumna
    public float[] Price => price;
    public void setUp()
    {
        price = new float[GameManager.levelNumber * GameManager.levelNumber];
    }
    public void clearUp()
    {
        price = null;
    }

    public void setPrice(int[,] price)
    {
       // this.price = price;
    }
}
