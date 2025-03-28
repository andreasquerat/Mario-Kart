using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawner : MonoBehaviour
{
    public static ItemRespawner instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void RespawnItem(ItemBox box, float delay)
    {
        StartCoroutine(RespawnRoutine(box, delay));
    }

    private IEnumerator RespawnRoutine(ItemBox box, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (box.boxModel != null)
        {
            box.boxModel.SetActive(true);
        }
    }
}