using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour, IDataPersistence
{
    #region IDataPersistence

    public int _lastScene;

    public void LoadData(GameData data)
    {
        this._lastScene = data.lastScene;
    }

    public void SaveData(ref GameData data)
    {
        data.lastScene = this._lastScene;
    }
    #endregion


    public void ChangeScenes(int nextScene)
    {
        // Update "last scene" to the one you are exiting
        _lastScene = SceneManager.GetActiveScene().buildIndex;

        // Save the game every time you enter a new scene
        FindObjectOfType<DataPersistenceManager>().SaveGame();

        // Load the next scene
        SceneManager.LoadScene(nextScene);
    }
}
