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
        [Header("Components")]
        [SerializeField] private PlayerInventory playerInventory;

        private Queue<ScrapPickup> scrapQueue = new Queue<ScrapPickup>();
        private Coroutine collectionCoroutine;

        public void AddToQueue(ScrapPickup scrapPickup)
        {
            if (!scrapQueue.Contains(scrapPickup))
            {
                scrapQueue.Enqueue(scrapPickup);

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

                if (scrapPickup.TryGetComponent<UIScrapCollectionProgress>(out var scrapCollectionProgressUI))
                {
                    scrapCollectionProgressUI.ResetFill();
                }

                if (scrapQueue.Count == 0)
                {
                    StopCoroutine(collectionCoroutine);
                    collectionCoroutine = null;
                }
            }
        }

        private IEnumerator Collecting()
        {
            while (scrapQueue.Count > 0)
            {
                ScrapPickup currentScrap = scrapQueue.Peek();
                currentScrap.TryGetComponent<UIScrapCollectionProgress>(out var scrapCollectionProgressUI);
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
                        if (scrapCollectionProgressUI != null)
                        {
                            scrapCollectionProgressUI.ResetFill();
                        }
                        break;
                    }

                    elapsedTime += Time.deltaTime;
                    yield return null;
                }

                if (scrapQueue.Contains(currentScrap))
                {
                    if (currentScrap.GetComponentInParent<ScrapMetalController>())
                    {
                        playerInventory.AddScrapMetalToInventory(currentScrap.GetValueScrap());
                    }
                    else if (currentScrap.GetComponentInParent<ScrapAlienController>())
                    {
                        playerInventory.AddScrapAlienToInventory(currentScrap.GetValueScrap());
                    }

                    scrapQueue.Dequeue();
                    currentScrap.transform.parent.gameObject.SetActive(false); // objectPool

                    if (scrapCollectionProgressUI != null)
                    {
                        scrapCollectionProgressUI.ResetFill();
                    }
                }
            }

            collectionCoroutine = null;
        }
    }
}
