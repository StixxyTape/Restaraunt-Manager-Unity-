using UnityEngine;
using UnityEngine.InputSystem;

// The InputManager script manages player input and provides access to input-related functionality.
// It serves as a singleton to provide a single instance of the InputManager throughout the game.
// The script handles player interactions and triggers specific actions based on input events.
// It subscribes to input events for the interact button and spawn customer button.
// The InputManager also interacts with the CustomerSpawner script to spawn customers.
// It provides a method to check if the interact button is currently pressed.
// The InputManager is responsible for enabling and disabling the player input actions.

public class InputManager : MonoBehaviour
{
    public static InputManager _instance { get; set; } // Singleton instance of the InputManager

    private PlayerController _playerControls; // Reference to the PlayerController input actions

    private CustomerSpawner _customerSpawner; // Reference to the CustomerSpawner script

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; // Set this as the singleton instance
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _customerSpawner = FindObjectOfType<CustomerSpawner>(); // Find and assign the CustomerSpawner script
        _playerControls = new PlayerController(); // Create a new instance of the PlayerController input actions
    }
    private void OnEnable()
    {
        _playerControls.Enable();
        _playerControls.Inputs.SpawnCustomertemp.performed += OnSpawnCustomerPerformed; // Subscribe to the spawn customer button event
    }

    private void OnDisable()
    {
        _playerControls.Disable();
        _playerControls.Inputs.SpawnCustomertemp.performed += OnSpawnCustomerPerformed; // Unsubscribe to the spawn customer button event
    }

    private void OnSpawnCustomerPerformed(InputAction.CallbackContext context)
    {
        //_customerSpawner.SpawnCustomer(); // Trigger the spawn customer method in the CustomerSpawner script
        //Debug.Log("spawnbutton");
    }
}
