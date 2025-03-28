using UnityEngine;
public abstract class ItemEffect : MonoBehaviour
{
    public Sprite icon; // Image de l'objet
    public enum ItemType { Star, Mushroom }
    public ItemType itemType;

    public abstract void ActivateEffect(KartItem kart); // GARDE SEULEMENT CETTE MÉTHODE
}