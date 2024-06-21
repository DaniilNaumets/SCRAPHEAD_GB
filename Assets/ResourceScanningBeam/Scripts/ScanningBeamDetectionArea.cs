using Resources;
using System.Collections.Generic;
using UnityEngine;

namespace ScanningBeam
{
    public class ScanningBeamDetectionArea : MonoBehaviour
    {
        private Queue<ScrapPickup> resourcesQueue = new Queue<ScrapPickup>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ScrapPickup>(out var scrapPickup))
            {
                Debug.Log("+");
                resourcesQueue.Enqueue(scrapPickup);
                
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            
        }
    }
}

