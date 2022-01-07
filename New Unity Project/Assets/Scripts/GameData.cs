using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    private string nick;
    private string ID;
    private int generalBronzeCoins;
    private int generalSilverCoins;
    private int generalGoldCoins;
    private int level;
    private bool[] levelUnclocked;
    private int[] controlSelected;
    private int[] highestScore; 
    private float[] time;

    public GameData(GameManager game)
    {
        generalBronzeCoins = game.getGeneralBronzeCoins();
        generalSilverCoins = game.getGeneralSilverCoins();
        generalGoldCoins = game.getGeneralGoldCoins();
        level = game.getlevelPlayer();
        levelUnclocked = game.getlevelUnclockedPlayer();
        time = game.getTime();
        controlSelected = game.getControlerSelected();
        highestScore = game.getHighestScore();
        nick = game.getNick();
        ID = game.getId();
    }
    public void loadGameData()
    {
     if(GameManager.instance != null)
        GameManager.instance.setData(generalBronzeCoins, generalSilverCoins, generalGoldCoins, level, levelUnclocked, time,nick,controlSelected,highestScore,ID);
    }
    public string ToString()
    {
        return "Coin: bronze: "+ generalBronzeCoins + " silver: "+ generalSilverCoins + " gold: " + generalGoldCoins+ "| level: " + level +" | unlockedLvls: "+ levelUnclocked.ToString() + " | times: " +  time + " | nick: " + nick + " | Control Selected : " + controlSelected.ToString() + " | Hightest score : " + highestScore.ToString() +" id user: "+ ID;
    }

}
