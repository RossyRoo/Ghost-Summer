using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionHandler : MonoBehaviour
{
    private HUDManager _hudManager;

    private float _interactionRange = 1f;
    [SerializeField] private Transform _feetTarget;
    [SerializeField] private LayerMask _interactableLayers;
    private Interactable _activeInteractable;

    private void Awake()
    {
        _hudManager = FindObjectOfType<HUDManager>();
    }

    public void CheckForInteractables()
    {
        // Detect colliders within range
        List<Collider2D> interactablesInRange = new List<Collider2D>(Physics2D.OverlapCircleAll(_feetTarget.position, _interactionRange, _interactableLayers));

        // Loop through the colliders and make the closest one the Active Interactable
        Interactable closestInteractableInRange = null;
        float shortestDistance = Mathf.Infinity;
        for (int i = 0; i < interactablesInRange.Count; i++)
        {
            if (Vector2.Distance(_feetTarget.position, interactablesInRange[i].transform.position) < shortestDistance)
            {
                closestInteractableInRange = interactablesInRange[i].GetComponent<Interactable>();
                shortestDistance = Vector2.Distance(_feetTarget.position, closestInteractableInRange.transform.position);
            }
        }
        _activeInteractable = closestInteractableInRange;

        // Set the interaction prompt appropriately
        if (_activeInteractable != null)
            _hudManager.SetInteractionPrompt(_activeInteractable._interactionPrompt, true);
        else
            _hudManager.SetInteractionPrompt("", false);
    }

    public void TryInteraction()
    {
        if (_activeInteractable == null)
            return;

        if (_activeInteractable.GetType() == typeof(Door))
            _activeInteractable.GetComponent<Door>().UseDoor();
        else if (_activeInteractable.GetType() == typeof(Bed))
            _activeInteractable.GetComponent<Bed>().UseBed();
        else if (_activeInteractable.GetType() == typeof(Pickup))
            _activeInteractable.GetComponent<Pickup>().TakePickup();
        else
            Debug.LogWarning("You are trying to interact with a generic interactable. This will not do anything.");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_feetTarget.position, _interactionRange);
    }
}
