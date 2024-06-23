using Resources;
using Resources.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScanningBeam
{
    public class ScanningBeamCollecting : MonoBehaviour
    {
        private Queue<ScrapPickup> scrapQueue = new Queue<ScrapPickup>();
        private Coroutine collectionCoroutine;

        public void AddToQueue(ScrapPickup scrapPickup)
        {
            if (!scrapQueue.Contains(scrapPickup))
            {
                scrapQueue.Enqueue(scrapPickup);
                Debug.Log("Upload " + scrapPickup.gameObject.name);

                if (collectionCoroutine == null)
                {
                    collectionCoroutine = StartCoroutine(Collecting());
                }
            }
        }

        public void RemoveFromQueue(ScrapPickup scrapPickup)
        {
            if (scrapQueue.Contains(scrapPickup))
            {
                scrapQueue = new Queue<ScrapPickup>(scrapQueue.Where(r => r != scrapPickup));
                Debug.Log("Delete " + scrapPickup.gameObject.name);

                if (scrapPickup.TryGetComponent<UIScrapCollectionProgress>(out var scrapCollectionProgressUI))
                {
                    scrapCollectionProgressUI.ResetFill(); // —брос заполнени€ при выходе из триггера
                }

                if (collectionCoroutine != null && scrapQueue.Count == 0)
                {
                    StopCoroutine(collectionCoroutine);
                    collectionCoroutine = null;
                    Debug.Log("StopCoroutine - Queue Empty");
                }
                else if (collectionCoroutine != null && scrapQueue.Count > 0)
                {
                    StopCoroutine(collectionCoroutine);
                    collectionCoroutine = StartCoroutine(Collecting());
                    Debug.Log("Restart Coroutine for Next Resource");
                }
            }
        }

        private IEnumerator Collecting()
        {
            while (scrapQueue.Count > 0)
            {
                ScrapPickup currentScrap = scrapQueue.Peek();
                currentScrap.TryGetComponent<UIScrapCollectionProgress>(out var scrapCollectionProgressUI);
                Debug.Log("Start Collecting " + currentScrap.gameObject.name);

                float collectionTime = currentScrap.GetCollectionTime();

                if (scrapCollectionProgressUI != null)
                {
                    scrapCollectionProgressUI.StartFill(collectionTime);
                }

                float elapsedTime = 0f;

                while (elapsedTime < collectionTime)
                {
                    if (!scrapQueue.Contains(currentScrap))
                    {
                        Debug.Log("Resource removed from queue during collection");
                        if (scrapCollectionProgressUI != null)
                        {
                            scrapCollectionProgressUI.ResetFill();
                        }

                        yield break;
                    }

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                Debug.Log("Resource Collect");//!!!
                if (scrapQueue.Contains(currentScrap))
                {
                    scrapQueue.Dequeue();
                    currentScrap.transform.parent.gameObject.SetActive(false);
                }

                Debug.Log("End Collecting " + currentScrap.gameObject.name);
                if (scrapCollectionProgressUI != null)
                {
                    scrapCollectionProgressUI.ResetFill();
                }
            }

            collectionCoroutine = null;
        }
    }
}








