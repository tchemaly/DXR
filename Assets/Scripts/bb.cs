using UnityEngine;
using UnityEngine.InputSystem;

public class bb : MonoBehaviour
{
    public GameObject boundingBoxVisualPrefab; // Prefab with a transparent material
    private GameObject currentBoundingBox;
    private Vector3 startPoint;
    private bool isDragging = false;
    private InputAction selectAction;

    void Awake()
    {
        // Initialize the input action from the Input Actions asset
        var inputActionAsset = (InputActionAsset)Resources.Load("InputActions"); // Ensure this matches your created Input Actions asset's name
        selectAction = inputActionAsset.FindActionMap("XRRig").FindAction("Select");

        selectAction.started += ctx => StartDragging();
        selectAction.canceled += ctx => EndDragging();
    }

    void OnEnable()
    {
        selectAction.Enable();
    }

    void OnDisable()
    {
        selectAction.Disable();
    }

    private void StartDragging()
    {
        startPoint = GetControllerPosition();
        currentBoundingBox = Instantiate(boundingBoxVisualPrefab, startPoint, Quaternion.identity);
        currentBoundingBox.transform.localScale = Vector3.zero;
        isDragging = true;
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector3 currentPoint = GetControllerPosition();
            Vector3 center = (startPoint + currentPoint) / 2;
            currentBoundingBox.transform.position = center;
            currentBoundingBox.transform.localScale = new Vector3(Mathf.Abs(currentPoint.x - startPoint.x), Mathf.Abs(currentPoint.y - startPoint.y), Mathf.Abs(currentPoint.z - startPoint.z));
        }
    }

    private void EndDragging()
    {
        isDragging = false;
    }

    private Vector3 GetControllerPosition()
    {
        // Implement this to return the current VR controller position
        // This may involve directly accessing the Transform of the controller or using more specific OpenXR input data
        return new Vector3(); // Return the actual position
    }
}
