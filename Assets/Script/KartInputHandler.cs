using UnityEngine;

public class KartInputHandler : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private int playerID = 1; // ID du joueur (1 = Joueur 1, 2 = Joueur 2)

    private string horizontalAxis, accelerateKey;
    private float _rotationInput;
    private bool _isAccelerating;

    // Propriétés pour récupérer les entrées dans PlayerController
    public float RotationInput => _rotationInput;
    public bool IsAccelerating => _isAccelerating;

    void Start()
    {
        AssignControls();
    }

    void Update()
    {
        ReadInputs();
    }

    // Assigner les touches en fonction du joueur
    private void AssignControls()
    {
        if (playerID == 1)
        {
            horizontalAxis = "Horizontal"; // Flèches directionnelles
            accelerateKey = "Space"; // Barre espace pour accélérer
        }
        else if (playerID == 2)
        {
            horizontalAxis = "Horizontal_P2"; // Configurer "Horizontal_P2" dans Input Manager
            accelerateKey = "LeftShift"; // Maj gauche pour accélérer
        }
    }

    private void ReadInputs()
    {
        _rotationInput = Input.GetAxis(horizontalAxis);
        _isAccelerating = Input.GetKey(accelerateKey);
    }
}