using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Physics & Movement")]
    [SerializeField] private Rigidbody _rb; // Référence au Rigidbody du Kart
    [SerializeField] private LayerMask _layerMask; // Pour détecter le sol et les terrains

    [Header("Speed Settings")]
    [SerializeField] private float _maxSpeed = 3f; // Vitesse max
    [SerializeField] private float _accelerationFactor = 0.05f; // Facteur d'accélération
    [SerializeField] private float _rotationSpeed = 100f; // Vitesse de rotation
    [SerializeField] private AnimationCurve _accelerationCurve; // Courbe d'accélération

    private float _currentSpeed;
    private float _accelerationLerp;
    private float _rotationInput;
    private bool _isAccelerating;

    private float _terrainSpeedModifier = 1f;
    private bool _isBoosting = false;
    private float _boostMultiplier = 1f;
    private float _boostEndTime = 0f;

    void Update()
    {
        HandleTerrainEffect();
        HandleBoost();
    }

    private void FixedUpdate()
    {
        ApplyAcceleration();
        ApplyRotation();
        MoveKart();
    }

    // Fonction appelée par PlayerController pour mettre à jour les entrées
    public void SetInputs(float rotation, bool accelerating)
    {
        _rotationInput = rotation;
        _isAccelerating = accelerating;
    }

    // Gestion des effets de terrain (ex: herbe ralentit, route normale, etc.)
    private void HandleTerrainEffect()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out var hit, 1f, _layerMask))
        {
            Terrain terrain = hit.transform.GetComponent<Terrain>();
            _terrainSpeedModifier = terrain ? terrain.speedVariator : 1f;
        }
        else
        {
            _terrainSpeedModifier = 1f;
        }
    }

    // Gestion du boost (s'il est actif)
    private void HandleBoost()
    {
        if (_isBoosting && Time.time > _boostEndTime)
        {
            _isBoosting = false;
            _boostMultiplier = 1f;
        }
    }

    private void ApplyAcceleration()
    {
        _accelerationLerp = Mathf.Clamp01(_accelerationLerp + (_isAccelerating ? _accelerationFactor : -_accelerationFactor * 2));
        _currentSpeed = _accelerationCurve.Evaluate(_accelerationLerp) * _maxSpeed * _terrainSpeedModifier * _boostMultiplier;
    }

    private void ApplyRotation()
    {
        if (_currentSpeed > 0.1f)
        {
            transform.Rotate(Vector3.up * _rotationInput * _rotationSpeed * Time.deltaTime);
        }
    }

    private void MoveKart()
    {
        _rb.MovePosition(transform.position + transform.forward * _currentSpeed * Time.fixedDeltaTime);
    }

    // Fonction pour activer un boost
    public void ApplyBoost(float boostAmount, float duration)
    {
        _boostMultiplier = boostAmount;
        _isBoosting = true;
        _boostEndTime = Time.time + duration;
    }
}