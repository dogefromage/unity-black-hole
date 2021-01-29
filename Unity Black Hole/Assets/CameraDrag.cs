using UnityEngine;
 
public class CameraDrag : MonoBehaviour
{
    public float dragSpeed = 2f;
    public float scrollSensitivity = 1f;
    private float distance = 5;

    private Vector3 dragOrigin;
    //private float lastPhi = 0;
    //private float lastTheta = 0;
    private Quaternion lastRotation;
    
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
            //lastPhi = camera.rotation.eulerAngles.y;
            //lastTheta = camera.rotation.eulerAngles.x;
            lastRotation = transform.rotation;
        }
 
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        distance *= Mathf.Exp(-Input.mouseScrollDelta.y * scrollSensitivity);
 
        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);

        //float theta = lastTheta - pos.y * dragSpeed;
        //float phi = lastPhi + pos.x * dragSpeed;
        //transform.rotation = Quaternion.Euler(theta, phi, 0);

        float zFactor = 1f - 2f * Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
        float xFactorLol = 1f - Mathf.Abs(zFactor);

        Vector3 move = new Vector3(-pos.y * xFactorLol, pos.x, pos.y * zFactor * 0.5f);
        move *= dragSpeed;
        transform.rotation = lastRotation * Quaternion.Euler(move);
    }
}
