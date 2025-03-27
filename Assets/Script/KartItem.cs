using UnityEngine;
using System.Collections;

public class KartItem : MonoBehaviour
{
    private enum ItemType { None, Mushroom, Star } // Types d'objets
    private ItemType currentItem = ItemType.None;

    [Header("Boost Settings")]
    [SerializeField] private float mushroomBoost = 2f; // Multiplicateur de vitesse
    [SerializeField] private float starDuration = 10f; // Durée de l'invincibilité

    private KartMovement kartMovement;
    private bool isStarActive = false;

    void Start()
    {
        kartMovement = GetComponent<KartMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Touche pour utiliser l'objet
        {
            UseItem();
        }
    }

    public void CollectItem(string itemName)
    {
        if (currentItem == ItemType.None) // Ne prend qu'un objet à la fois
        {
            if (itemName == "Mushroom") currentItem = ItemType.Mushroom;
            else if (itemName == "Star") currentItem = ItemType.Star;
        }
    }

    private void UseItem()
    {
        if (currentItem == ItemType.Mushroom)
        {
            kartMovement.ApplyBoost(mushroomBoost);
            currentItem = ItemType.None; // Supprime l'objet après usage
        }
        else if (currentItem == ItemType.Star)
        {
            StartCoroutine(ActivateStar());
            currentItem = ItemType.None;
        }
    }

    private IEnumerator ActivateStar()
    {
        isStarActive = true;
        kartMovement.SetInvincible(true); // Active l'invincibilité
        yield return new WaitForSeconds(starDuration);
        kartMovement.SetInvincible(false);
        isStarActive = false;
    }

    public bool IsStarActive()
    {
        return isStarActive;
    }
}