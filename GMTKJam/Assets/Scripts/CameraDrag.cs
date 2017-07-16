using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDrag : MonoBehaviour {

    private Vector3 lastFramePosition, currFramePosition;

    public float HorizontalSpeed = 40F;
    public float VerticalSpeed = 40F;

    private float cameraYPosition;
    private float cameraXPosition;
    private float cameraZPosition;


    void Update()
    {
        PanMouse();
        HandleZoom();
    }

    void PanMouse()
    {
        if (Input.GetMouseButton(2))
        {
            float h = HorizontalSpeed * Input.GetAxis("Mouse Y");
            float v = VerticalSpeed * Input.GetAxis("Mouse X");
            transform.Translate(v, h, 0);
            cameraXPosition = Camera.main.transform.position.x;
            cameraXPosition = Mathf.Clamp(cameraXPosition, 0, 800);
            cameraZPosition = Camera.main.transform.position.z;
            cameraZPosition = Mathf.Clamp(cameraZPosition, 0, 800);
            Camera.main.transform.position = new Vector3(cameraXPosition, Camera.main.transform.position.y, cameraZPosition);
        }

    }

    void HandleZoom()
    {
        cameraYPosition = Camera.main.transform.position.y;
        cameraYPosition -= cameraYPosition * Input.GetAxis("Mouse ScrollWheel"); //Zoom in and out
        cameraYPosition = Mathf.Clamp(cameraYPosition, 10, 100); //Clamps zoom
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, cameraYPosition, Camera.main.transform.position.z);
    }

}
