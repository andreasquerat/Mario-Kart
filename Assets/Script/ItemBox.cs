using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [Header("Item Box Settings")]
    [SerializeField] private GameObject boxModel; // Modèle visuel de la boîte
    [SerializeField] private List<GameObject> possibleItems; // Liste des objets possibles
    [SerializeField] private float respawnTime = 5f; // Temps avant réapparition

    private bool isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            KartItem kartItem = other.GetComponent<KartItem>();
            if (kartItem != null && !kartItem.HasItem())
            {
                GiveRandomItem(kartItem);
                StartCoroutine(RespawnBox());
            }
        }
    }

    private void GiveRandomItem(KartItem kartItem)
    {
        if (possibleItems.Count > 0)
        {
            int randomIndex = Random.Range(0, possibleItems.Count);
            GameObject selectedItem = possibleItems[randomIndex];
            kartItem.ReceiveItem(selectedItem);
        }
    }

    private IEnumerator RespawnBox()
    {
        isActive = false;
        boxModel.SetActive(false);
        yield return new WaitForSeconds(respawnTime);
        boxModel.SetActive(true);
        isActive = true;
    }
}