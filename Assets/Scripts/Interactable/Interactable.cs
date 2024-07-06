using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [HideInInspector]public string _interactionPrompt;
    public string _interactableName;


    private void Start()
    {
        SetInteractionPromptNames();
    }

    private void SetInteractionPromptNames()
    {
        if (GetType() == typeof(Door))
            _interactionPrompt = $"Go to {_interactableName}";
        else if (GetType() == typeof(Bed))
            _interactionPrompt = $"End Day";
        else if (GetType() == typeof(Pickup))
            _interactionPrompt = $"Pick up {_interactableName}";
        else
            _interactionPrompt = $"Interact with {_interactableName}";
    }
}
