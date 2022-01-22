using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TradeManager : MonoBehaviour
{
    public HandleSystem bronzePriceTable;
    public HandleSystem silverPriceTable;
    public HandleSystem goldPriceTable;
    public TextAsset csvFileBronze;
    public TextAsset csvFileSilver;
    public TextAsset csvFileGold;
    public int [] tempTraderPrice = new int[GameManager.levelNumber];
    public const float mutationValue = 0.4f;
    public const float changePrice= 2f;

    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ';'; // It defines field seperate character
    private int exchangeIn;
    private int exchangeOut;

    public Toggle BtoS;
    public Toggle BtoG;
    public Toggle StoB;
    public Toggle StoG;
    public Toggle GtoB;
    public Toggle GtoS;
    public Slider slider;
    public string current;
    public TMP_InputField deposit;
    public TMP_Text withdraw;
    public TMP_Text maxValue;
    


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
    public void OnEnable()
    {
        BtoS.isOn = false;
        BtoG.isOn = false;
        StoB.isOn = false;
        StoG.isOn = false;
        GtoB.isOn = false;
        GtoS.isOn = false;
        slider.value = 0;
        slider.maxValue = 1;
        current = "";
        deposit.text = "00";
        withdraw.text = "00";
        BtoS.interactable = false;
        BtoG.interactable = false;
        StoB.interactable = false;
        StoG.interactable = false;
        GtoB.interactable = false;
        GtoS.interactable = false;
        deposit.interactable = false;
        slider.interactable = false;
    }
    public void OnSelected(string selected)
    {
    
            deposit.interactable = true;
            slider.interactable = true;
      
        switch (selected)
        {
            case "BtoS":
                if (!BtoS.isOn) break;
                slider.maxValue = GameManager.instance.getGeneralBronzeCoins();
                current = selected;
                //    BtoS.isOn = false;
                    BtoG.isOn = false;
                    StoB.isOn = false;
                    StoG.isOn = false;
                    GtoB.isOn = false;
                    GtoS.isOn = false;
                break;
            case "BtoG":
                if (!BtoG.isOn) break;
                slider.maxValue = GameManager.instance.getGeneralBronzeCoins();
                current = selected;
                BtoS.isOn = false;
            //    BtoG.isOn = false;
                StoB.isOn = false;
                StoG.isOn = false;
                GtoB.isOn = false;
                GtoS.isOn = false;
                break;
            case "StoB":
                if (!StoB.isOn) break;
                slider.maxValue = GameManager.instance.getGeneralSilverCoins();
                current = selected;
                BtoS.isOn = false;
                BtoG.isOn = false;
               // StoB.isOn = false;
                StoG.isOn = false;
                GtoB.isOn = false;
                GtoS.isOn = false;
                break;
            case "StoG":
                if (!StoG.isOn) break;
                slider.maxValue = GameManager.instance.getGeneralSilverCoins();
                current = selected;
                BtoS.isOn = false;
                BtoG.isOn = false;
                StoB.isOn = false;
             //   StoG.isOn = false;
                GtoB.isOn = false;
                GtoS.isOn = false;
                break;
            case "GtoB":
                if (!GtoB.isOn) break;
                slider.maxValue = GameManager.instance.getGeneralGoldCoins();
                current = selected;
                BtoS.isOn = false;
                BtoG.isOn = false;
                StoB.isOn = false;
                StoG.isOn = false;
              //  GtoB.isOn = false;
                GtoS.isOn = false;
                break;
            case "GtoS":
                if (!GtoS.isOn) break;
                slider.maxValue = GameManager.instance.getGeneralGoldCoins();
                current = selected;
                BtoS.isOn = false;
                BtoG.isOn = false;
                StoB.isOn = false;
                StoG.isOn = false;
                GtoB.isOn = false;
                //GtoS.isOn = false;
                break;
        }
 
        exchange();
        maxValue.text = slider.maxValue.ToString();

    }       
    public void sliderOnSelected()
    {
        deposit.text = (System.Math.Round(slider.value)).ToString();
      
    }
    public void InputOnChanged()
    {
        float value;
        if (float.TryParse(deposit.text, out value))
        {
            value = (float) System.Math.Round(value);
            slider.value = value;
            exchangeIn = (int)value;
            exchange();
        }
    }

   
    public void exchange()
    {
      
        if (exchangeIn >0)
        {
            switch (current)
            {
                case "BtoG":
                    exchangeOut = (int) System.Math.Floor((double)( exchangeIn* bronzePriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()]/
                        goldPriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()]));
                    break;
                case "BtoS":
                    exchangeOut = (int)System.Math.Floor((double)(exchangeIn * bronzePriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()] /
                        silverPriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()]));
                    break;
                case "StoB":
                    exchangeOut = (int)System.Math.Floor((double)(exchangeIn * silverPriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()] /
                        bronzePriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()]));

                    break;
                case "StoG":
                    exchangeOut = (int)System.Math.Floor((double)(exchangeIn * silverPriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()] /
                        goldPriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()]));
                    break;
                case "GtoB":
                    exchangeOut = (int)System.Math.Floor((double)(exchangeIn * goldPriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()] /
                        bronzePriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()]));
                    break;
                case "GtoS":
                    exchangeOut = (int)System.Math.Floor((double)(exchangeIn * goldPriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()] /
                        silverPriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()]));
                    break;
            }
            withdraw.text = exchangeOut.ToString();
            Debug.Log("SelectLevel.selected: " + SelectLevel.selected + " |  GameManager.instance.getlevelPlayer(): "+ GameManager.instance.getlevelPlayer());
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
    public void transaction()
    {
        if (exchangeOut > 0)
        {
            switch (current)
            {
                case "BtoG":
                    GameManager.instance.setGenrealBronzeCoins(
                        GameManager.instance.getGeneralBronzeCoins() - exchangeIn);
                    GameManager.instance.setGenrealGoldCoins(
                       GameManager.instance.getGeneralGoldCoins() + exchangeOut);
                    bronzePriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()] *= (1 - Mathf.Log(exchangeIn * changePrice) / 10);
                    goldPriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()] *= (1 + Mathf.Log(exchangeOut * changePrice) / 10);
                    break;
                case "BtoS":
                    GameManager.instance.setGenrealBronzeCoins(
                       GameManager.instance.getGeneralBronzeCoins() - exchangeIn);
                    GameManager.instance.setGenrealSilverCoins(
                       GameManager.instance.getGeneralSilverCoins() + exchangeOut);


                    bronzePriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()] *= (1 - Mathf.Log(exchangeIn * changePrice) / 10);
                    silverPriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()] *= (1 + Mathf.Log(exchangeOut * changePrice) / 10);
                    break;
                case "StoB":
                    GameManager.instance.setGenrealSilverCoins(
                     GameManager.instance.getGeneralSilverCoins() - exchangeIn);
                    GameManager.instance.setGenrealBronzeCoins(
                       GameManager.instance.getGeneralBronzeCoins() + exchangeOut);

                    silverPriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()] *= (1 - Mathf.Log(exchangeIn * changePrice) / 10);
                    bronzePriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()] *= (1 + Mathf.Log(exchangeOut * changePrice) / 10);
                    break;
                case "StoG":
                    GameManager.instance.setGenrealSilverCoins(
                        GameManager.instance.getGeneralSilverCoins() - exchangeIn);
                    GameManager.instance.setGenrealGoldCoins(
                       GameManager.instance.getGeneralGoldCoins() + exchangeOut);

                    silverPriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()] *= (1 - Mathf.Log(exchangeIn * changePrice) / 10);
                    goldPriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()] *= (1 + Mathf.Log(exchangeOut * changePrice) / 10);
                    break;
                case "GtoB":
                    GameManager.instance.setGenrealGoldCoins(
                       GameManager.instance.getGeneralGoldCoins() - exchangeIn);
                    GameManager.instance.setGenrealBronzeCoins(
                       GameManager.instance.getGeneralBronzeCoins() + exchangeOut);

                    goldPriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()] *= (1 - Mathf.Log(exchangeIn * changePrice) / 10);
                    bronzePriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()] *= (1 + Mathf.Log(exchangeOut * changePrice) / 10);
                    break;
                case "GtoS":
                    GameManager.instance.setGenrealGoldCoins(
                       GameManager.instance.getGeneralGoldCoins() - exchangeIn);
                    GameManager.instance.setGenrealSilverCoins(
                       GameManager.instance.getGeneralSilverCoins() + exchangeOut);

                    goldPriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()] *= (1 - Mathf.Log(exchangeIn * changePrice) / 10);
                    silverPriceTable.Price[SelectLevel.selected * GameManager.levelNumber + GameManager.instance.getlevelPlayer()] *= (1 + Mathf.Log(exchangeOut * changePrice) / 10);
                    break;
            }
            OnEnable();
        }
    }
    public void setValueFirstTime() // pobranie pierwszy i jedyny raz wartosci z plikow, mutacja ich.
    {
        string[] traderBronze = csvFileBronze.text.Split(lineSeperater); // trader
        for (int traderIterator = 0; traderIterator < traderBronze.Length-1; traderIterator++)
        {
            string[] level = traderBronze[traderIterator].Split(fieldSeperator); // price on lvl
            for (int levelIterator = 0; levelIterator < level.Length; levelIterator++)
            {
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

}
