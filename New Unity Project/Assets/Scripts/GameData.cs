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
    private int[] controlSelected1;
    private int[] highestScore1;
    private float[] time1;
    private int[] controlSelected2;
    private int[] highestScore2;
    private float[] time2;

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
        time1 = game.getTime1();
        controlSelected1 = game.getControlerSelected1();
        highestScore1 = game.getHighestScore1();
        time2 = game.getTime2();
        controlSelected2 = game.getControlerSelected2();
        highestScore2 = game.getHighestScore2();
        nick = game.getNick();
        ID = game.getId();
    }
    public void loadGameData()
    {
     if(GameManager.instance != null)
        GameManager.instance.setData(generalBronzeCoins, generalSilverCoins, generalGoldCoins, level, levelUnclocked, time,nick,controlSelected,highestScore,ID,time1,controlSelected1,highestScore1,time2,controlSelected2,highestScore2);
    }
    public string ToString()
    {
        string napis = "";
        napis += "Coin: bronze: " + generalBronzeCoins + " silver: " + generalSilverCoins + " gold: " + generalGoldCoins + "| level: " + level + " | unlockedLvls: " + levelUnclocked.ToString() + " | times: " + time + " | nick: " + nick + "\n | Control Selected : ";
        for (int i = 0; i < controlSelected.Length; i++)
            napis += "| " + controlSelected[i];
        napis += "\n  | Hightest score : ";
        for (int i = 0; i < highestScore.Length; i++)
            napis += "| " + highestScore[i];
        napis += "\n--------------------------------------";
        napis += "\n | Control Selected : ";
        for (int i = 0; i < controlSelected1.Length; i++)
            napis += "| " + controlSelected1[i];
        napis += "\n  | Hightest score : ";
        for (int i = 0; i < highestScore1.Length; i++)
            napis += "| " + highestScore1[i];
         napis += "\n--------------------------------------";
        napis += "\n | Control Selected : ";
        for (int i = 0; i < controlSelected2.Length; i++)
            napis += "| " + controlSelected2[i];
        napis += "\n  | Hightest score : ";
        for (int i = 0; i < highestScore2.Length; i++)
            napis += "| " + highestScore2[i];
        return napis; //"Coin: bronze: "+ generalBronzeCoins + " silver: "+ generalSilverCoins + " gold: " + generalGoldCoins+ "| level: " + level +" | unlockedLvls: "+ levelUnclocked.ToString() + " | times: " +  time + " | nick: " + nick + "\n | Control Selected : " + controlSelected[0].ToString() + "\n  | Hightest score : " + highestScore.ToString() +" id user: "+ ID;
    }

}
