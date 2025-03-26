using UnityEngine;

public class KartMovement : MonoBehaviour
{
    [Header("Mouvement")]
    [SerializeField] private float acceleration = 5f; // Puissance d'acc�l�ration
    [SerializeField] private float maxSpeed = 10f; // Vitesse max du Kart
    [SerializeField] private float rotationSpeed = 100f; // Vitesse de rotation

    private float currentSpeed = 0f; // Vitesse actuelle du kart
    private Rigidbody rb; // R�f�rence au Rigidbody du kart

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // R�cup�re le Rigidbody attach� au Kart
        rb.freezeRotation = true; // Emp�che le kart de se renverser
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        // R�cup�re l'entr�e verticale (fl�che haut/bas)
        float moveInput = Input.GetAxis("Vertical");

        if (moveInput > 0) // Acc�l�ration
        {
            currentSpeed += acceleration * Time.deltaTime;
        }
        else if (moveInput < 0) // Marche arri�re
        {
            currentSpeed -= acceleration * Time.deltaTime;
        }
        else // Ralentissement naturel
        {
            currentSpeed *= 0.98f; // Ajoute une l�g�re friction
        }

        // Clamp la vitesse pour �viter de d�passer les limites
        currentSpeed = Mathf.Clamp(currentSpeed, -maxSpeed / 2, maxSpeed);

        // Applique le mouvement au Rigidbody
        rb.linearVelocity = transform.forward * currentSpeed;
    }

    private void HandleRotation()
    {
        // R�cup�re l'entr�e horizontale (fl�che gauche/droite)
        float turnInput = Input.GetAxis("Horizontal");

        // Rotation uniquement si le kart bouge
        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            float turn = turnInput * rotationSpeed * Time.deltaTime * Mathf.Sign(currentSpeed);
            transform.Rotate(Vector3.up * turn);
        }
    }
}