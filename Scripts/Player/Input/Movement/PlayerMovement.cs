using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // This script handles the movement of the player

    // Components + Variables

    #region
    // Getting the PlayerInput Component
    [SerializeField]
    private PlayerInput _playerInput;

    // Getting the player for their transform and other stuff :D
    [SerializeField]
    private GameObject _player;

    // Getting the camera to set it following the player
    [SerializeField]
    private Camera _camera;

    // Setting up the player movement
    [SerializeField]
    private float _moveSpeed;

    // Move action from Action Map
    private InputAction _moveAction;

    // Player Rigidbody
    private Rigidbody _rigidBody;

    // Checking which direction the player is moving
    private Vector2 _moveInput;
    #endregion

    private void Awake()
    {
        // Initializing the input actions
        _moveAction = _playerInput.actions["Movement"];

        // Initializing the variables
        _rigidBody = _player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Gets the movement of the player
        _moveInput = _moveAction.ReadValue<Vector2>();

        // Calculate the movement direction based on the camera's forward direction
        Vector3 cameraForward = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 inputDirection = _moveInput.x * _camera.transform.right + _moveInput.y * cameraForward;

        // Calculate the desired movement vector
        Vector3 moveVector = inputDirection * _moveSpeed;

        // Apply the movement to the Rigidbody velocity
        _rigidBody.velocity = new Vector3(moveVector.x, _rigidBody.velocity.y, moveVector.z);

        float targetAngle = Mathf.Atan2(_moveInput.x, _moveInput.y) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, targetAngle, 0f);
        _player.transform.rotation = Quaternion.Lerp(_player.transform.rotation, targetRotation, 10f * Time.deltaTime);
    }

    private void LateUpdate()
    {
        // Set the camera's position in lateupdate to avoid jittering
        _camera.transform.position = new Vector3(
            _player.transform.position.x,
            _player.transform.position.y + .5f,
            _player.transform.position.z - 2f
            );
    }
}
