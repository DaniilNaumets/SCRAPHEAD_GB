using Resources;
using UnityEngine;

namespace ScanningBeam
{
    public class ScanningBeamDetectionArea : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ScanningBeamCollecting scanningBeamCollecting;
        [SerializeField] private SpriteRenderer scanningBeamSpriteRenderer;

        private void Awake()
        {
            scanningBeamSpriteRenderer.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ScrapPickup scrapPickup = collision.GetComponentInChildren<ScrapPickup>();

            if (scrapPickup != null)
            {
                scanningBeamCollecting.AddToQueue(scrapPickup);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            ScrapPickup scrapPickup = collision.GetComponentInChildren<ScrapPickup>();

            if (scrapPickup != null)
            {
                //scanningBeamSpriteRenderer.enabled = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            ScrapPickup scrapPickup = collision.GetComponentInChildren<ScrapPickup>();

            if (scrapPickup != null)
            {
                //scanningBeamSpriteRenderer.enabled = false;
                scanningBeamCollecting.RemoveFromQueue(scrapPickup);
            }
        }
    }
}

