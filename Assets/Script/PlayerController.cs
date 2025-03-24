using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private KartInputHandler _inputHandler;
    private CarController _carController;

    void Start()
    {
        _inputHandler = GetComponent<KartInputHandler>();
        _carController = GetComponent<CarController>();
    }

    void Update()
    {
        if (_inputHandler != null && _carController != null)
        {
            _carController.SetInputs(_inputHandler.RotationInput, _inputHandler.IsAccelerating);
        }
    }
}