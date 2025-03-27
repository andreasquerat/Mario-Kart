using UnityEngine;

public class KartMovement : MonoBehaviour
{
    [Header("Physics & Speed Settings")]
    [SerializeField] private float maxSpeed = 15f; // Vitesse maximale
    [SerializeField] private float accelerationRate = 5f; // Accélération par seconde
    [SerializeField] private float decelerationRate = 3f; // Ralentissement par seconde
    [SerializeField] private float turnSpeed = 100f; // Vitesse de rotation
    [SerializeField] private float turnPenalty = 0.95f; // Réduction de vitesse en tournant

    private Rigidbody rb;
    private float currentSpeed = 0f;
    private float accelerationProgress = 0f; // Suivi de l'accélération progressive
    private bool isFrozen = false;
    private float originalMaxSpeed;
    private float boostMultiplier = 1f;
    private bool isInvincible = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalMaxSpeed = maxSpeed;
    }
    public void SetInvincible(bool state)
    {
        isInvincible = state;
    }
    public void ModifySpeed(float modifier)
    {
        if (!isInvincible) // Si on n'a pas l'étoile, on applique les effets du terrain
        {
            maxSpeed *= modifier;
        }
        {
            maxSpeed = originalMaxSpeed * modifier;
        }
    }
    public void ApplyBoost(float multiplier)
    {
        boostMultiplier = multiplier;
        maxSpeed *= boostMultiplier; // Augmente la vitesse
    }
    public void ResetSpeed()
    {
        maxSpeed = originalMaxSpeed;
        boostMultiplier = 1f;
        isFrozen = false;
    }
    public void FreezeSpeed()
    {
        isFrozen = true;
        rb.linearVelocity = rb.linearVelocity; // On garde la vitesse actuelle
    }
 
    void Update()
    {
        HandleAcceleration();
        HandleTurning();
    }

    private void FixedUpdate()
    {
        MoveKart();
    }

    void HandleAcceleration()
    {
        bool isAccelerating = Input.GetKey(KeyCode.UpArrow);

        if (isAccelerating)
        {
            // On accélère progressivement jusqu'à la vitesse max
            accelerationProgress += Time.deltaTime * accelerationRate;
            accelerationProgress = Mathf.Clamp(accelerationProgress, 0f, maxSpeed);
        }
        else
        {
            // Si on relâche l'accélérateur, on ralentit doucement
            accelerationProgress -= Time.deltaTime * decelerationRate;
            accelerationProgress = Mathf.Clamp(accelerationProgress, 0f, maxSpeed);
        }
    }

    void HandleTurning()
    {
        float turnInput = Input.GetAxis("Horizontal"); // Flèches gauche/droite

        if (turnInput != 0)
        {
            // Réduction de la vitesse si on tourne brusquement
            accelerationProgress *= turnPenalty;
        }

        transform.Rotate(Vector3.up * turnInput * turnSpeed * Time.deltaTime);
    }

    void MoveKart()
    {
        // Appliquer le mouvement vers l'avant
        rb.linearVelocity = transform.forward * accelerationProgress;
    }
}