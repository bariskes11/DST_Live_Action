using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CollectionCanvas : MonoBehaviour
{
    [SerializeField] private Image sourceImageField;
    [SerializeField] private Sprite tree;
    [SerializeField] private Sprite rock;
    [SerializeField] private Canvas worldSpaceCanvas; // Assign the canvas in the inspector or dynamically
    [SerializeField] private float moveDuration = 1f; // Duration of the movement in seconds

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float elapsedTime = 0f;
    private Camera mainCam;
    private Canvas instantiatedCanvas;
    public Vector3 fixedCanvasScale = new Vector3(0.01f, 0.01f, 0.01f);
    public float fixedDistanceFromCamera = 5f;
    public void StartCreate(CommonVar.CollectableType collectType)
    {
        if (collectType == CommonVar.CollectableType.Tree)
        {
            sourceImageField.sprite = tree;
        }
        else
        {
            sourceImageField.sprite = rock;
        }

        instantiatedCanvas = this.worldSpaceCanvas;
        startPosition = worldSpaceCanvas.transform.position; 
        instantiatedCanvas.transform.localScale = fixedCanvasScale;
        
    }

    private void OnEnable()
    {
        mainCam=Camera.main;
        
    }

    void Update()
    {
        targetPosition = GetTopLeftCorner();
        // Move the canvas over time
        if (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration); //elapsedTime / moveDuration;
            // Lerp between start and target positions
            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, t);

            // Maintain a fixed distance from the camera
            Vector3 cameraDirection = (newPosition -mainCam.transform.position).normalized;
            instantiatedCanvas.transform.position = mainCam.transform.position + cameraDirection * fixedDistanceFromCamera;

            // Optional: Make the canvas face the camera
            instantiatedCanvas.transform.LookAt(mainCam.transform);
            instantiatedCanvas.transform.Rotate(0, 180, 0); // Optional: Flip if the canvas is backward
        }
        
    }
    
    Vector3 GetTopLeftCorner()
    {
        
        // Vector3 topLeftCorner = mainCam.ViewportToWorldPoint(new Vector3(0, 1, mainCam.nearClipPlane + 0.1f));
        //
        // // Adjust for the size of the canvas
        // RectTransform canvasRect = worldSpaceCanvas.GetComponent<RectTransform>();
        // Vector3 adjustedPosition = topLeftCorner + new Vector3(canvasRect.rect.width / 2, -canvasRect.rect.height / 2, 0);
        //
        // return adjustedPosition;
        // Get the top-left corner of the screen in world space
        Vector3 topLeftCorner = mainCam.ViewportToWorldPoint(new Vector3(0, 1, mainCam.nearClipPlane + 0.1f));

        // Adjust for the size of the canvas
        RectTransform canvasRect = worldSpaceCanvas.GetComponent<RectTransform>();
        Vector3 adjustedPosition = topLeftCorner + new Vector3(canvasRect.rect.width / 2, -canvasRect.rect.height / 2, 0);

        // Remove the downward shift by changing the sign of the Y-axis adjustment
        adjustedPosition = topLeftCorner + new Vector3(canvasRect.rect.width / 2, canvasRect.rect.height / 2, 0);

        return adjustedPosition;
    }
}
