using Resources;
using UnityEngine;

namespace ScanningBeam
{
    public class ScanningBeamDetectionArea : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ScanningBeamCollecting scanningBeamCollecting;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ScrapPickup scrapPickup = collision.GetComponentInChildren<ScrapPickup>();

            if (scrapPickup != null && !scrapPickup.GetComponentInChildren<ScrapPickup>().isGoing)
            {             
                scanningBeamCollecting.AddToQueue(scrapPickup);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            ScrapPickup scrapPickup = collision.GetComponentInChildren<ScrapPickup>();

            if (scrapPickup != null)
            {              
                scanningBeamCollecting.RemoveFromQueue(scrapPickup);
                scrapPickup.isGoing = false;
            }
        }
    }
}

