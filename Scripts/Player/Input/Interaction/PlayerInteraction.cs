using UnityEngine;
using UnityEngine.InputSystem;

// Ok, this script is mainly just for when there is an object for the player to interact with, like a door, 
// or a prep counter, or whatever. Shouldn't be too complex. Might also add more interaction things in the future, 
// although it seems pretty fine at the moment.


public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction _instance { get; private set; } // Singleton instance

    [SerializeField]
    private GameObject _interactBauble;
    [SerializeField]
    private GameObject _player; // Reference to the player GameObject
    [SerializeField]
    private Camera _camera; // Reference to the player GameObject
    [SerializeField]
    private float _interactionRange; // Maximum range for interaction

    private PlayerController _playerController;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; // Set the singleton instance to this object
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances of this script
            return;
        }

        _playerController = new PlayerController();
    }

    // OnEnable and OnDisable
    #region
    private void OnEnable()
    {
        _playerController.Enable();
    }
    private void OnDisable()
    {
        _playerController.Disable();
    }
    #endregion

    public bool Interact()
    {
        return _playerController.Inputs.Interact.triggered; // check if the interact button is pressed
    }

    public GameObject ClosestInteractable()
    {
        // Find all colliders within a sphere around the player
        Collider[] colliders = Physics.OverlapSphere(_player.transform.position, _interactionRange);

        GameObject closestInteractable = null;
        Vector3 interactBaublePos = new Vector3(0, -100, 0);
        float closestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.layer == 3)
            {
                float distance = Vector3.Distance(collider.transform.position, _player.transform.position);

                if (distance < closestDistance)
                {
                    closestInteractable = collider.gameObject;
                    closestDistance = distance;

                    interactBaublePos = new Vector3(
                                closestInteractable.transform.position.x,
                                closestInteractable.transform.position.y + 1f,
                                closestInteractable.transform.position.z
                                ); ;
                }
            }
        }

        _interactBauble.transform.position = interactBaublePos;

        return closestInteractable;
    }

    public bool HoverOnObject(GameObject interactionObject)
    {
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _interactionRange))
        {
            if (hitInfo.collider.gameObject == interactionObject)
            {
                return true;
            }
        }
        return false;
    }
}
