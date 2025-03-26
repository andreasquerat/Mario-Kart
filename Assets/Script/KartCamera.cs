using UnityEngine;

public class KartCamera : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private Transform target; // Référence au kart

    [Header("Position de la caméra")]
    [SerializeField] private Vector3 offset = new Vector3(0, 2, -4); // Légèrement en hauteur et derrière
    [SerializeField] private float followSpeed = 10f; // Vitesse de suivi

    [Header("Rotation & Alignement")]
    [SerializeField] private float rotationSpeed = 5f; // Vitesse de rotation
    [SerializeField] private float lookAheadDistance = 2f; // Distance de regard devant le kart

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        FollowKart();
    }

    private void FollowKart()
    {
        // Position cible (derrière le kart)
        Vector3 targetPosition = target.position + target.TransformDirection(offset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 1f / followSpeed);

        // Orientation : regarder toujours légèrement en avant
        Vector3 lookAtPosition = target.position + target.forward * lookAheadDistance;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookAtPosition - transform.position), rotationSpeed * Time.deltaTime);
    }
}