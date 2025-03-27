using UnityEngine;

public class TerrainEffect : MonoBehaviour
{
    public enum TerrainType { Normal, Slow, Ice, Boost } // Ajout du terrain de boost
    public TerrainType terrainType = TerrainType.Normal;

    [Range(0f, 1f)] public float speedModifier = 0.5f; // Pour ralentir ou accélérer
    [Range(1f, 3f)] public float boostMultiplier = 1.5f; // Pour le boost de vitesse

    private Renderer terrainRenderer; // Pour changer la couleur

    void Start()
    {
        terrainRenderer = GetComponent<Renderer>(); // Récupère le Renderer
        UpdateTerrainColor(); // Applique la bonne couleur
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KartMovement kart = other.GetComponent<KartMovement>();
            if (kart != null)
            {
                ApplyTerrainEffect(kart);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            KartMovement kart = other.GetComponent<KartMovement>();
            if (kart != null)
            {
                kart.ResetSpeed(); // Remet les valeurs normales
            }
        }
    }

    private void ApplyTerrainEffect(KartMovement kart)
    {
        switch (terrainType)
        {
            case TerrainType.Slow:
                kart.ModifySpeed(speedModifier);
                break;
            case TerrainType.Ice:
                kart.FreezeSpeed();
                break;
            case TerrainType.Boost:
                kart.ApplyBoost(boostMultiplier);
                break;
            default:
                break;
        }
    }

    // 🎨 Change la couleur selon le type de terrain
    private void UpdateTerrainColor()
    {
        if (terrainRenderer == null) return;

        switch (terrainType)
        {
            case TerrainType.Slow:
                terrainRenderer.material.color = Color.green; // Herbe = Vert
                break;
            case TerrainType.Ice:
                terrainRenderer.material.color = Color.cyan; // Glace = Bleu clair
                break;
            case TerrainType.Boost:
                terrainRenderer.material.color = Color.red; // Boost = Rouge
                break;
            default:
                terrainRenderer.material.color = Color.gray; // Normal = Gris
                break;
        }
    }
}