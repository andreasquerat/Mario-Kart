using UnityEngine;

public class TerrainModifier : MonoBehaviour
{
    public enum TerrainType { Normal, Slow, Ice, Boost }
    public TerrainType terrainType;
    public float speedModifier = 0.5f;
    public float boostAmount = 3f;

    private void OnTriggerEnter(Collider other)
    {

        KartMovement kart = other.GetComponent<KartMovement>();

        if (kart != null)
        {
            switch (terrainType)
            {
                case TerrainType.Slow:
                    kart.ApplyBoost(-kart.maxSpeed * (1 - speedModifier), 0);
                    break;
                case TerrainType.Ice:
                    kart.SetFrozen(true);
                    break;
                case TerrainType.Boost:
                    kart.ApplyBoost(boostAmount, 2f);
                    break;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        KartMovement kart = other.GetComponent<KartMovement>();

        if (kart != null && terrainType == TerrainType.Ice)
        {
            kart.SetFrozen(false);
        }
    }

}