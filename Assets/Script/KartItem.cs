using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartItem : MonoBehaviour
{
    public ItemUI itemUI; // UI pour afficher l'item
    private ItemEffect currentItem; // L'item en stock
    private bool isInvincible = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Utiliser l'item avec E (change si nécessaire)
        {
            UseItem();
        }
    }

    public void ReceiveItem(ItemEffect item)
    {
        if (currentItem != null) return; // Empêche d'avoir plusieurs items en même temps

        currentItem = item;
        itemUI.SetItem(item);
    }

    public void UseItem()
    {
        if (currentItem == null) return;

        Debug.Log($" Utilisation de l'item : {currentItem.itemType}");

        currentItem.ActivateEffect(this); // Appelle la méthode propre à l'item

        itemUI.ClearItem(); // Supprime l'affichage de l'item
        currentItem = null; // Vide l'item en stock
    }

    // 🟢 Méthode rendue publique pour StarItem
    public void SetInvincible(float duration)
    {
        if (isInvincible) return; // Empêche les doubles effets
        StartCoroutine(InvincibilityRoutine(duration));
    }

    private IEnumerator InvincibilityRoutine(float duration)
    {
        isInvincible = true;
        Debug.Log("✨ Invincible !");

        float elapsedTime = 0f;
        Renderer kartRenderer = GetComponent<Renderer>();

        while (elapsedTime < duration)
        {
            kartRenderer.material.color = new Color(Random.value, Random.value, Random.value);
            yield return new WaitForSeconds(0.2f);
            elapsedTime += 0.2f;
        }

        kartRenderer.material.color = Color.white;
        isInvincible = false;
        Debug.Log("💀 Fin de l'invincibilité !");
    }

    // 🟢 Méthode rendue publique pour MushroomItem
    public IEnumerator ActivateBoost()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null) yield break;

        Debug.Log("🚀 Boost activé !");
        float boostForce = 20f;
        float duration = 3f;

        rb.AddForce(transform.forward * boostForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(duration); // Attendre la durée du boost

        rb.linearVelocity = Vector3.zero; // Arrêter le boost après le temps imparti
        Debug.Log("🛑 Fin du boost !");
    }
}