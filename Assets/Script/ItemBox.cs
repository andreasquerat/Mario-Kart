using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public List<GameObject> possibleItems; // Liste des items possibles
    public GameObject boxModel; // Mod�le 3D de la box

    private void OnTriggerEnter(Collider other)
    {
        KartItem kart = other.GetComponent<KartItem>();
        if (kart != null && possibleItems.Count > 0)
        {
            // G�n�re un index valide
            int randomIndex = Random.Range(0, possibleItems.Count);

            // V�rifie que l'item existe bien
            if (possibleItems[randomIndex] != null)
            {
                GameObject itemObject = Instantiate(possibleItems[randomIndex]); // Cr�e l'item
                ItemEffect itemEffect = itemObject.GetComponent<ItemEffect>(); // R�cup�re l'effet

                if (itemEffect != null)
                {
                    kart.ReceiveItem(itemEffect); // Donne l'item au kart
                    Debug.Log($"Item re�u : {itemEffect.name}");
                }
                else
                {
                    Debug.LogError("L'item g�n�r� ne poss�de pas de ItemEffect !");
                }
                Debug.Log($"Item re�u : {itemEffect.name}");

                // D�sactive temporairement la box
                if (boxModel != null)
                {
                    boxModel.SetActive(false);
                }
                ItemRespawner.instance.RespawnItem(this, 5f);
            }
            else
            {
                Debug.LogError("L'item s�lectionn� est NULL !");
            }
        }
        else
        {
            Debug.LogError("La liste d'objets est vide !");
        }
    }
    private IEnumerator RespawnBoxRoutine()
    {
        yield return new WaitForSeconds(5f); // Temps de respawn
        if (boxModel != null)
        {
            boxModel.SetActive(true);
        }
    }
}