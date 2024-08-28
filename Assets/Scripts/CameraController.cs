using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float PlayerCameraDistance { get; set; }
    public Transform cameraTarget;

    Camera playerCamara;
    float zoomSpeed = 35f;

    private void Start()
    {
        PlayerCameraDistance = 10f;
        playerCamara = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0) 
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            playerCamara.fieldOfView -= scroll * zoomSpeed;
            playerCamara.fieldOfView = Mathf.Clamp(playerCamara.fieldOfView, 1, 179);
            
        }
        transform.position = new Vector3(cameraTarget.position.x, 
            cameraTarget.position.y + PlayerCameraDistance, cameraTarget.position.z - PlayerCameraDistance); 
    }
}
