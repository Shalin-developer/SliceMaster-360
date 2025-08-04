using UnityEngine;
using UnityEngine.InputSystem;

public class XRControllerMapper : MonoBehaviour
{
    [Header("Tracked Objects")]
    public GameObject leftHandObject;   // Assign Left Hand GameObject
    public GameObject rightHandObject;  // Assign Right Hand GameObject

    [Header("Input Actions")]
    public InputActionProperty leftPositionAction;
    public InputActionProperty leftRotationAction;
    public InputActionProperty rightPositionAction;
    public InputActionProperty rightRotationAction;

    [Header("XR Origin (Assign XR Origin Object)")]
    public Transform xrOrigin; // XR Origin should be assigned in the Inspector

    private Vector3 leftHandInitialWorldPos;
    private Quaternion leftHandInitialWorldRot;
    private Vector3 rightHandInitialWorldPos;
    private Quaternion rightHandInitialWorldRot;

    void Start()
    {
        if (xrOrigin == null)
        {
            Debug.LogError("XR Origin is not assigned! Please assign the XR Origin in the inspector.");
            return;
        }

        if (leftHandObject == null || rightHandObject == null)
        {
            Debug.LogError("Left or Right Hand Object is missing! Assign them in the inspector.");
            return;
        }

        // **Store the initial world-space position of the controllers (set in Unity Editor)**
        leftHandInitialWorldPos = leftHandObject.transform.position;
        leftHandInitialWorldRot = leftHandObject.transform.rotation;

        rightHandInitialWorldPos = rightHandObject.transform.position;
        rightHandInitialWorldRot = rightHandObject.transform.rotation;

        // Enable input actions
        leftPositionAction.action.Enable();
        leftRotationAction.action.Enable();
        rightPositionAction.action.Enable();
        rightRotationAction.action.Enable();
    }

    void Update()
    {
        if (xrOrigin == null) return;

        UpdateHandTracking();
    }

    void UpdateHandTracking()
    {
        // LEFT HAND
        if (leftHandObject != null)
        {
            Vector3 leftPos = leftPositionAction.action.ReadValue<Vector3>();
            Quaternion leftRot = leftRotationAction.action.ReadValue<Quaternion>();

            // Move relative to the **originally placed position**
            leftHandObject.transform.position = leftHandInitialWorldPos + (xrOrigin.rotation * leftPos);
            leftHandObject.transform.rotation = leftHandInitialWorldRot * leftRot;
        }

        // RIGHT HAND
        if (rightHandObject != null)
        {
            Vector3 rightPos = rightPositionAction.action.ReadValue<Vector3>();
            Quaternion rightRot = rightRotationAction.action.ReadValue<Quaternion>();

            // Move relative to the **originally placed position**
            rightHandObject.transform.position = rightHandInitialWorldPos + (xrOrigin.rotation * rightPos);
            rightHandObject.transform.rotation = rightHandInitialWorldRot * rightRot;
        }
    }
}
