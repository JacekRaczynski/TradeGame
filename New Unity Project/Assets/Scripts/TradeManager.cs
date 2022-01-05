using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeManager : MonoBehaviour
{
    public HandleSystem bronzePriceTable;
    public HandleSystem silverPriceTable;
    public HandleSystem goldPriceTable;
    public TextAsset csvFileBronze;
    public TextAsset csvFileSilver;
    public TextAsset csvFileGold;
    public int [] tempTraderPrice = new int[GameManager.levelNumber];
    public float mutationValue = 0.4f;
    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ';'; // It defines field seperate character
    private string exchange;

    public Toggle BtoS;
    public Toggle BtoG;
    public Toggle StoB;
    public Toggle StoG;
    public Toggle GtoB;
    public Toggle GtoS;

    enum Exchange
    {
        BronzeToSilver,
        BronzeToGold,
        SilverToBronze,
        SilverToGold,
        GoldToBronze,
        GoldToSilver
    }


    void Start()
    {
        clearProfile();
        onMakeProfileSetUp();
        setValueFirstTime();

}
    public void OnSelected(string selected)
    {
        Debug.Log(gameObject.name+ " "+ selected);
        switch (selected)
        {
            case "BtoG":
                BtoS.isOn = false;
                //    BtoG.isOn = false;
                StoB.isOn = false;
                StoG.isOn = false;
                GtoB.isOn = false;
                GtoS.isOn = false;
                break;
        }


}       

    public void onMakeProfileSetUp() // stworzenie tabeli
    {
        bronzePriceTable.setUp();
        silverPriceTable.setUp();
        goldPriceTable.setUp();
    }
    public void clearProfile() // wyczyszczenie tabeli
    {
        bronzePriceTable.clearUp();
        silverPriceTable.clearUp();
        goldPriceTable.clearUp();
    }
    public void setValueFirstTime() // pobranie pierwszy i jedyny raz wartosci z plikow, mutacja ich.
    {
        string[] traderBronze = csvFileBronze.text.Split(lineSeperater); // trader
        for (int traderIterator = 0; traderIterator < traderBronze.Length-1; traderIterator++)
        {
            string[] level = traderBronze[traderIterator].Split(fieldSeperator); // price on lvl
            for (int levelIterator = 0; levelIterator < level.Length; levelIterator++)
            {
                Debug.Log(traderIterator + " : " + levelIterator);
                bronzePriceTable.Price[traderIterator * GameManager.levelNumber + levelIterator] = int.Parse(level[levelIterator]) * (1- (Random.Range(0f, mutationValue) - (mutationValue / 2)));
               
            }
        }
        string[] traderSilver = csvFileSilver.text.Split(lineSeperater); // trader
        for (int traderIterator = 0; traderIterator < traderSilver.Length - 1; traderIterator++)
        {
            string[] level = traderSilver[traderIterator].Split(fieldSeperator); // price on lvl
            for (int levelIterator = 0; levelIterator < level.Length; levelIterator++)
            {
                silverPriceTable.Price[traderIterator * GameManager.levelNumber + levelIterator] = int.Parse(level[levelIterator]) * (1 - (Random.Range(0f, mutationValue) - (mutationValue / 2)));
            }
        }
        string[] traderGold = csvFileGold.text.Split(lineSeperater); // trader
    
        for (int traderIterator = 0; traderIterator < traderGold.Length - 1; traderIterator++)
        {
            string[] level = traderGold[traderIterator].Split(fieldSeperator); // price on lvl
            for (int levelIterator = 0; levelIterator < level.Length; levelIterator++)
            {
                goldPriceTable.Price[traderIterator * GameManager.levelNumber + levelIterator] = int.Parse(level[levelIterator]) * (1 - (Random.Range(0f, mutationValue) - (mutationValue / 2)));
            }
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
