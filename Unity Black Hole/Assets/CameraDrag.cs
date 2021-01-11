using UnityEngine;
 
public class CameraDrag : MonoBehaviour
{

    public float dragSpeed = 2f;
    public float scrollSensitivity = 1f;
    private float distance = 5;

    private Vector3 dragOrigin;
    private float lastPhi = 0;
    private float lastTheta = 0;
    
    public Transform camera;
 
    void Update()
    {
        camera.localPosition = new Vector3(0, 0, -distance);
        camera.localRotation = Quaternion.identity;

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastPhi = camera.rotation.eulerAngles.y;
            lastTheta = camera.rotation.eulerAngles.x;
        }
 
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        distance *= Mathf.Exp(-Input.mouseScrollDelta.y * scrollSensitivity);
        Debug.Log(Input.mouseScrollDelta.y);
 
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
        Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

        float theta = lastTheta - pos.y * dragSpeed;
        // theta = Mathf.Clamp(theta, -180, 180);
        float phi = lastPhi + pos.x * dragSpeed;
 
        transform.rotation = Quaternion.Euler(theta, phi, 0);
    }
}