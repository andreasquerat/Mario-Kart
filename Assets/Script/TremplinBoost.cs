using UnityEngine;

public class TremplinBoost : MonoBehaviour
{
    public float boostMultiplier = 2f; // Multiplicateur de vitesse
    public float boostDuration = 2f; // Durée du boost

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CarControler kart = other.GetComponent<CarControler>();
            if (kart != null)
            {
                kart.ApplyBoost(boostMultiplier, boostDuration);
            }
        }
    }
}