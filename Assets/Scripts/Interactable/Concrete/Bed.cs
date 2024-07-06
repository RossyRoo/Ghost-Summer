using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : Interactable
{
    public void UseBed()
    {
        FindObjectOfType<GameTimeManager>().EndDay();
    }
}
