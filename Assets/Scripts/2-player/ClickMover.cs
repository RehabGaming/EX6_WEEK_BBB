using UnityEngine;
using UnityEngine.InputSystem;

/**
 * This component allows the player to move to a point in the screen by clicking it. 
 * Uses BFS to find the shortest path from the current location to the new location.
 */
public class ClickMover: TargetMover {

    [SerializeField] InputAction moveTo = new InputAction(type: InputActionType.Button);
    private int zero = 0;
    [SerializeField]
    [Tooltip("Determine the location to 'moveTo'.")]
    InputAction moveToLocation = new InputAction(type: InputActionType.Value, expectedControlType: "Vector2");

    void OnValidate() {
        // Provide default bindings for the input actions.
        // Based on answer by DMGregory: https://gamedev.stackexchange.com/a/205345/18261
        if (moveTo.bindings.Count == zero)
            moveTo.AddBinding("<Mouse>/leftButton");
        if (moveToLocation.bindings.Count == zero)
            moveToLocation.AddBinding("<Mouse>/position");
    }
    private void OnEnable()
    {
        // Enable the "moveTo" and "moveToLocation" input actions when the script is enabled.
        moveTo.Enable();
        moveToLocation.Enable();
    }

    private void OnDisable()
    {
        // Disable the "moveTo" and "moveToLocation" input actions when the script is disabled.
        moveTo.Disable();
        moveToLocation.Disable();
    }

    void Update()
    {
        // Check if the "moveTo" action was performed in this frame.
        if (moveTo.WasPerformedThisFrame())
        {
            // Get the screen position from the "moveToLocation" input action.
            Vector3 newTarget = Camera.main.ScreenToWorldPoint(moveToLocation.ReadValue<Vector2>());

            // Set the new target position (convert screen position to world position).
            SetTarget(newTarget);
        }
    }

}
