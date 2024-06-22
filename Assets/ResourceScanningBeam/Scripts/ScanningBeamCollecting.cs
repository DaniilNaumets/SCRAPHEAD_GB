using Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScanningBeam
{
    public class ScanningBeamCollecting : MonoBehaviour
    {
        private Queue<ScrapPickup> scrapQueue = new Queue<ScrapPickup>();
        private bool isProcessing = false;

        public void StartCollecting(ScrapPickup scrapPickup)
        {
            scrapQueue.Enqueue(scrapPickup);         
            Debug.Log("Upload " + scrapPickup.gameObject.name);

            if (!isProcessing)
            {
                isProcessing = true;
                StartCoroutine(PickupCoroutine(scrapQueue.Peek()));
            }        
        }

        public void StopCollecting(ScrapPickup scrapPickup)
        {
            scrapQueue.Dequeue();           
            Debug.Log("Delete " + scrapPickup.gameObject.name);

            if (isProcessing && scrapPickup == scrapQueue.Peek()) 
            {
                StopAllCoroutines();
            }
            
        }

        private IEnumerator PickupCoroutine(ScrapPickup scrapPickup)
        {
            Debug.Log("Start C " + scrapPickup.gameObject.name);
            yield return new WaitForSeconds(scrapPickup.GetCollectionTime());
            isProcessing = false;
            scrapQueue.Dequeue();
            Debug.Log("End C " + scrapPickup.gameObject.name);            
        }
    }
}


