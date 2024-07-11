using Entity;
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
        [SerializeField] private EntityInventory inventory;
        [SerializeField] private SpriteRenderer scanningBeamSpriteRenderer;

        private Queue<ScrapPickup> scrapQueue = new Queue<ScrapPickup>();
        private Coroutine collectionCoroutine;

        private void Awake()
        {
            scanningBeamSpriteRenderer.enabled = false;
        }

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
                    scanningBeamSpriteRenderer.enabled = false;
                    StopCoroutine(collectionCoroutine);
                    collectionCoroutine = null;
                }
            }
        }

        private IEnumerator Collecting()
        {
            scanningBeamSpriteRenderer.enabled = true;

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
                        inventory?.AddScrapMetalToInventory(currentScrap.GetValueScrap());
                    }
                    else if (currentScrap.GetComponentInParent<ScrapAlienController>())
                    {
                        inventory?.AddScrapAlienToInventory(currentScrap.GetValueScrap());
                    }

                    scrapQueue.Dequeue();
                    currentScrap.transform.parent.gameObject.SetActive(false); // objectPool

                    if (scrapCollectionProgressUI != null)
                    {
                        scrapCollectionProgressUI.ResetFill();
                    }
                }
            }

            scanningBeamSpriteRenderer.enabled = false;
            collectionCoroutine = null;
        }
    }
}
