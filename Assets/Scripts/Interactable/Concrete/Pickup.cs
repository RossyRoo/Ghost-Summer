using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : Interactable
{
    PlayerInventoryHandler playerInventoryHandler;

    public Item _pickup;

    private void Awake()
    {
        playerInventoryHandler = FindObjectOfType<PlayerInventoryHandler>();
    }

    private void Update()
    {
        if (playerInventoryHandler.OwnsItem(_pickup.dataID) && _pickup.isUnique)
            Destroy(gameObject);
    }


    public void TakePickup()
    {
        playerInventoryHandler.AddItemToInventory(_pickup.dataID);
        Destroy(gameObject);
    }
}
