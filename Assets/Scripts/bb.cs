using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class bb : MonoBehaviour
{
    public GameObject boundingBoxVisualPrefab; // Prefab with a transparent material
    private GameObject currentBoundingBox;
    private Vector3 startPoint;
    private bool isDragging = false;
    private XRController controller;

    void Start()
    {
        // You might need to find the controller based on whether it's the left or right hand.
        controller = FindObjectOfType<XRController>(); // Simplified way to find controller. Adapt as necessary.
    }

    void Update()
    {
        if (controller)
        {
            CheckInput();
            if (isDragging)
            {
                UpdateBoundingBox();
            }
        }
    }

    private void CheckInput()
    {
        if (controller.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out bool triggerValue) && triggerValue)
        {
            if (!isDragging)
            {
                StartDragging();
            }
        }
        else if (isDragging)
        {
            EndDragging();
        }
    }

    private void StartDragging()
    {
        startPoint = GetControllerPosition();
        currentBoundingBox = Instantiate(boundingBoxVisualPrefab, startPoint, Quaternion.identity);
        currentBoundingBox.transform.localScale = Vector3.zero;
        isDragging = true;
    }

    private void UpdateBoundingBox()
    {
        Vector3 currentPoint = GetControllerPosition();
        Vector3 center = (startPoint + currentPoint) / 2;
        currentBoundingBox.transform.position = center;
        currentBoundingBox.transform.localScale = new Vector3(Mathf.Abs(currentPoint.x - startPoint.x), Mathf.Abs(currentPoint.y - startPoint.y), Mathf.Abs(currentPoint.z - startPoint.z));
    }

    private void EndDragging()
    {
        isDragging = false;
    }

    private Vector3 GetControllerPosition()
    {
        // Assuming the controller has an attached Transform representing its physical position
        return controller.transform.position;
    }
}
