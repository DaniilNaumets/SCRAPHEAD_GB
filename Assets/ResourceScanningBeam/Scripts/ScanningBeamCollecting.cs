using Entity;
using ObjectPool;
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

        private ObjectsPoolManager objectsPoolManager;
        private Queue<ScrapPickup> scrapQueue = new Queue<ScrapPickup>();
        private Coroutine collectionCoroutine;

        private void Awake()
        {
            if (scanningBeamSpriteRenderer == null)
            {
                Debug.LogError("scanningBeamSpriteRenderer is not assigned.");
            }
            else
            {
                scanningBeamSpriteRenderer.enabled = false;
            }

            if (inventory == null)
            {
                Debug.LogError("Inventory is not assigned.");
            }

            objectsPoolManager = FindObjectOfType<ObjectsPoolManager>();
            if (objectsPoolManager == null)
            {
                Debug.LogError("ObjectsPoolManager is not found on the scene.");
            }
        }

        public void AddToQueue(ScrapPickup scrapPickup)
        {
            if (scrapPickup == null)
            {
                Debug.LogError("scrapPickup is null.");
                return;
            }

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
            if (scrapPickup == null)
            {
                Debug.LogError("scrapPickup is null.");
                return;
            }

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
                    if (collectionCoroutine != null)
                    {
                        StopCoroutine(collectionCoroutine);
                        collectionCoroutine = null;
                    }
                }
            }
        }

        private IEnumerator Collecting()
        {
            if (scanningBeamSpriteRenderer != null)
            {
                scanningBeamSpriteRenderer.enabled = true;
            }

            while (scrapQueue.Count > 0)
            {
                ScrapPickup currentScrap = scrapQueue.Peek();

                if (currentScrap == null)
                {
                    scrapQueue.Dequeue();
                    continue;
                }

                currentScrap.TryGetComponent<UIScrapCollectionProgress>(out var scrapCollectionProgressUI);
                float collectionTime = currentScrap.GetCollectionTime();

                if (scrapCollectionProgressUI != null)
                {
                    scrapCollectionProgressUI.StartFill(collectionTime);
                }

                float elapsedTime = 0f;

                while (elapsedTime < collectionTime)
                {
                    if (currentScrap == null || !scrapQueue.Contains(currentScrap))
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

                if (currentScrap != null && scrapQueue.Contains(currentScrap))
                {
                    if (inventory != null)
                    {
                        if (currentScrap.GetComponentInParent<ScrapMetalController>())
                        {
                            inventory.AddScrapMetalToInventory(currentScrap.GetValueScrap());
                        }
                        else if (currentScrap.GetComponentInParent<ScrapAlienController>())
                        {
                            inventory.AddScrapAlienToInventory(currentScrap.GetValueScrap());
                        }
                    }

                    scrapQueue.Dequeue();

                    if (objectsPoolManager != null)
                    {
                        objectsPoolManager.ReturnToPool(currentScrap.transform.parent.gameObject);
                    }
                    else
                    {
                        Debug.LogError("ObjectsPoolManager is not assigned.");
                    }

                    if (scrapCollectionProgressUI != null)
                    {
                        scrapCollectionProgressUI.ResetFill();
                    }
                }
            }

            if (scanningBeamSpriteRenderer != null)
            {
                scanningBeamSpriteRenderer.enabled = false;
            }
            collectionCoroutine = null;
        }
    }
}