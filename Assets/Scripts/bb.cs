using UnityEngine;

public class bb : MonoBehaviour
{
    public GameObject cubePrefab; // Assign the cube prefab in the Unity inspector
    public Transform controllerTransform; // Assign this to the actual controller transform

    private GameObject currentCube = null;
    private bool isDragging = false;
    private Vector3 startPosition;

    void Update()
    {
        // Check if the trigger on the right controller is being pressed down
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            StartDragging();
        }

        // While the trigger is held down, update the cube's size and position
        if (isDragging && OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            UpdateDragging();
        }

        // When the trigger is released, finalize the size and position of the cube
        if (isDragging && OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            EndDragging();
        }
    }

    void StartDragging()
    {
        startPosition = controllerTransform.position; // Start position is the current position of the controller
        currentCube = Instantiate(cubePrefab, startPosition, Quaternion.identity);
        isDragging = true;
    }

    void UpdateDragging()
    {
        if (currentCube != null)
        {
            Vector3 currentPosition = controllerTransform.position;
            Vector3 size = currentPosition - startPosition;
            currentCube.transform.localScale = new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y), Mathf.Abs(size.z));
            currentCube.transform.position = startPosition + size / 2; // Position at the midpoint
        }
    }

    void EndDragging()
    {
        isDragging = false;
        // Optionally, you can finalize the cube or make any adjustments here
    }
}