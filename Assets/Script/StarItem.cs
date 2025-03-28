using UnityEngine;

public class StarItem : ItemEffect
{
    public float duration = 10f;

    public override void ActivateEffect(KartItem kart)
    {
        kart.SetInvincible(duration);
        Debug.Log("✨ Invincibilité activée !");
    }
}