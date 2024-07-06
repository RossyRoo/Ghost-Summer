using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int lastScene;

    public bool dayHasStarted;

    public int currentDay;
    public double currentTimeOfDay;
    public int currentDayOfWeek;

    public SerializableDictionary<string, int> playerItemInventory;



    // The values defined in this constructor will be the default values
    // when the player starts a new game
    public GameData()
    {
        this.lastScene = 0;
        dayHasStarted = false;
        this.currentDay = 1;
        this.currentTimeOfDay = 25200.0f; // 7am
        this.currentDayOfWeek = 0;
        this.playerItemInventory = new SerializableDictionary<string, int>();
    }
}
