using UnityEngine;

public class MushroomItem : ItemEffect
{
    public float boostAmount = 2f;
    public float boostDuration = 3f;

    public override void ActivateEffect(KartItem kart)
    {
        kart.StartCoroutine(kart.ActivateBoost());
        Debug.Log("🚀 Boost activé !");
    }
}