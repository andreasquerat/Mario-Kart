using System.Collections;
using UnityEngine;

public class KartMovement : MonoBehaviour
{
    public string horizontalInput = "Horizontal"; // Défini dans l'Inspector
    public string verticalInput = "Vertical"; // Défini dans l'Inspector
    public KeyCode driftKey = KeyCode.LeftShift; // Touche pour drifter
    public KeyCode itemKey = KeyCode.E; // Touche pour utiliser l'item

    public float maxSpeed = 10f;
    public float acceleration = 5f;
    public float turnSpeed = 100f;
    private float currentSpeed = 0f;
    private bool isFrozen = false;
    private bool isInvincible = false;
    private bool isDrifting = false;

    private Rigidbody rb;
    private float originalTurnSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        originalTurnSpeed = turnSpeed; // Sauvegarde la vitesse de rotation de base
    }

    private void Update()
    {
        if (!isFrozen)
        {
            float moveInput = Input.GetAxis(verticalInput);
            Debug.Log(moveInput);
            float turnInput = Input.GetAxis(horizontalInput);
            currentSpeed += moveInput * acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

            transform.Rotate(0, turnInput * turnSpeed * Time.deltaTime, 0);

            // Gestion du drift
            if (Input.GetKeyDown(driftKey) && moveInput > 0)
            {
                StartCoroutine(Drift());
            }
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = transform.forward * currentSpeed;
    }

    public void ApplyBoost(float amount, float duration)
    {
        StartCoroutine(BoostCoroutine(amount, duration));
    }

    private IEnumerator BoostCoroutine(float amount, float duration)
    {
        float originalSpeed = maxSpeed;
        maxSpeed += amount;
        yield return new WaitForSeconds(duration);
        maxSpeed = originalSpeed;
    }

    public void SetFrozen(bool state)
    {
        isFrozen = state;
    }

    public void SetInvincible(float duration)
    {
        StartCoroutine(InvincibilityCoroutine(duration));
    }

    private IEnumerator InvincibilityCoroutine(float duration)
    {
        isInvincible = true;
        yield return new WaitForSeconds(duration);
        isInvincible = false;
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }

    private IEnumerator Drift()
    {
        isDrifting = true;
        Debug.Log("🔥 Drift activé !");

        turnSpeed *= 1.5f; // Augmente la rotation
        rb.linearDamping = 0.5f; // Réduit le frottement

        yield return new WaitForSeconds(1f); // Durée du drift

        turnSpeed = originalTurnSpeed;
        rb.linearDamping = 1f;
        isDrifting = false;
        Debug.Log("🛑 Drift terminé !");
    }
}