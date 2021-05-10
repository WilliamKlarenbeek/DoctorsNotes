using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player Data 
[System.Serializable] public class PlayerData
{
    public int playerMoney = 0;
}

//PlayerPrefs is stored AppData\Local\Packages\[ProductPackageID]\LocalState\playerprefs.dat
public class GameDataManager 
{
    static PlayerData playerData = new PlayerData();

    //Methods for saving and retrieving player data
    public static int GetMoney()
    {
        return PlayerPrefs.GetInt("money");
    }

    public static void AddMoney(int gainedMoney)
    {
        PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money") + gainedMoney));
    }

    public static void RemoveMoney(int lostMoney)
    {
        PlayerPrefs.SetInt("money", (PlayerPrefs.GetInt("money") - lostMoney));
    }

    public static bool CanPurchase(int totalCost)
    {
        return (PlayerPrefs.GetInt("money") >= totalCost);
    }

    public static int LoadPlayerData()
    {
        return playerData.playerMoney = PlayerPrefs.GetInt("money");
    }

    static void SavePlayerData(int playerMoney)
    {
        PlayerPrefs.SetInt("money", playerMoney);
    }

    static void DeletePlayerDate()
    {
        PlayerPrefs.DeleteKey("money");
    }
}
