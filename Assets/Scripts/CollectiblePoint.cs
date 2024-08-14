using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.PlayCollectSound();
            }

            if (PointManager.Instance != null)
            {
                PointManager.Instance.AddPoint();
            }

            Destroy(gameObject);
        }
    }
}
