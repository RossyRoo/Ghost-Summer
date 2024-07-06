using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    private GameTimeManager _gameTimeManager;

    [SerializeField] private TextMeshProUGUI _interactionPrompt_TMP;
    [SerializeField] private TextMeshProUGUI _gameDateDisplay_TMP;
    [SerializeField] private TextMeshProUGUI _gameTimeDisplay_TMP;


    private void Awake()
    {
        _gameTimeManager = FindObjectOfType<GameTimeManager>();
    }

    private void Update()
    {
        DisplayGameTime();
        DisplayGameDate();
    }

    public void SetInteractionPrompt(string promptText, bool isOn)
    {
        _interactionPrompt_TMP.gameObject.SetActive(isOn);
        _interactionPrompt_TMP.text = promptText;
    }

    public void DisplayGameTime()
    {
        _gameTimeDisplay_TMP.text = _gameTimeManager.DisplayGameTime();
        _gameTimeDisplay_TMP.gameObject.SetActive(CanDisplayTime());
    }

    public void DisplayGameDate()
    {
        _gameDateDisplay_TMP.text = _gameTimeManager.DisplayGameDate();
        _gameDateDisplay_TMP.gameObject.SetActive(CanDisplayDate());
    }

    public bool CanDisplayTime()
    {
        // TODO check if player has a watch and the day has started
        if (_gameTimeManager.dayHasStarted)
            return true;
        else
            return false;
    }

    public bool CanDisplayDate()
    {
        // TODO check if player has a planner
        //if (_gameTimeManager.dayHasStarted)
        //    return true;
        //else
        //    return false;
        return true;
    }
}