using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Interactable
{
    public Transform _entrywayPosition;
    public int _nextScene;

    
    public void UseDoor()
    {
        // If the player is leaving the bedroom, start the day
        if (SceneManager.GetActiveScene().buildIndex == FindObjectOfType<GameTimeManager>().BedroomSceneIndex)
            FindObjectOfType<GameTimeManager>().dayHasStarted = true;

        // Use SceneChangeManager to load the next scene
        FindObjectOfType<SceneChangeManager>().ChangeScenes(_nextScene);
    }
}
