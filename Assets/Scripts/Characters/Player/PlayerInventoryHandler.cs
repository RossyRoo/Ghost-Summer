using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryHandler : MonoBehaviour, IDataPersistence
{
    #region IDataPersistence
    public void LoadData(GameData data)
    {
        this.playerItemInventory = data.playerItemInventory;
    }

    public void SaveData(ref GameData data)
    {
        data.playerItemInventory = this.playerItemInventory;
    }
    #endregion

    public SerializableDictionary<string, int> playerItemInventory;

    
    public void AddItemToInventory(string id)
    {
        // If player already owns an item with this id, add another to their stock
        // Otherwise, add that id to the player's inventory and initialize stock to 1
        if(playerItemInventory.TryGetValue(id, out int count))
        {
            playerItemInventory[id] = count + 1;
        }
        else
            playerItemInventory.Add(id, 1);
    }

    public bool OwnsItem(string id)
    {
        // Check if the player owns at least one of the item
        if (playerItemInventory.ContainsKey(id))
            return true;
        else
            return false;
    }
}
