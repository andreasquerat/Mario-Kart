using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public List<GameObject> possibleItems; // Liste des items possibles
    public GameObject boxModel; // Modèle 3D de la box

    private void OnTriggerEnter(Collider other)
    {
        KartItem kart = other.GetComponent<KartItem>();
        if (kart != null && possibleItems.Count > 0)
        {
            // Génère un index valide
            int randomIndex = Random.Range(0, possibleItems.Count);

            // Vérifie que l'item existe bien
            if (possibleItems[randomIndex] != null)
            {
                GameObject itemObject = Instantiate(possibleItems[randomIndex]); // Crée l'item
                ItemEffect itemEffect = itemObject.GetComponent<ItemEffect>(); // Récupère l'effet

                if (itemEffect != null)
                {
                    kart.ReceiveItem(itemEffect); // Donne l'item au kart
                    Debug.Log($"Item reçu : {itemEffect.name}");
                }
                else
                {
                    Debug.LogError("L'item généré ne possède pas de ItemEffect !");
                }
                Debug.Log($"Item reçu : {itemEffect.name}");

                // Désactive temporairement la box
                if (boxModel != null)
                {
                    boxModel.SetActive(false);
                }
                ItemRespawner.instance.RespawnItem(this, 5f);
            }
            else
            {
                Debug.LogError("L'item sélectionné est NULL !");
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