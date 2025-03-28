using UnityEngine;

public class KartCamera : MonoBehaviour
{
    public Transform kart;
    public Vector3 offset;
    public float smoothSpeed = 5f;

    private void LateUpdate()
    {
        if (kart != null)
        {
            Vector3 desiredPosition = kart.position + kart.TransformDirection(offset);
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.LookAt(kart);
        }
    }
}