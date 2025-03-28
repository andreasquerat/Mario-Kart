using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image itemImage; // Image de l'UI pour l'item

    public void SetItem(ItemEffect itemEffect)
    {
        if (itemEffect == null || itemEffect.icon == null)
        {
            Debug.LogError("L'item ou son ic�ne est null !");
            itemImage.enabled = false;
            return;
        }

        itemImage.sprite = itemEffect.icon;
        itemImage.enabled = true;
    }

    public void ClearItem()
    {
        itemImage.sprite = null; // Supprime l�image
        itemImage.enabled = false; // D�sactive l�UI
    }
}