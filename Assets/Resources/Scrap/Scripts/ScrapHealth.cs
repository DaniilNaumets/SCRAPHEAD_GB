using ObjectPool;
using UnityEngine;

namespace Resources
{
    public class ScrapHealth : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ScrapCrumble scrapCrumble;
        [SerializeField] private ScrapTurnOff scrapTurnOff;
        [SerializeField] private GameObject smokePrefab;

        private ObjectsPoolManager objectsPoolManager;
        private float currentHealth;

        public void InitializeHealth(float health)
        {
            currentHealth = health;
            objectsPoolManager = FindObjectOfType<ObjectsPoolManager>();
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;          

            if (currentHealth <= 0) 
            {
                if (smokePrefab != null)
                {
                    GameObject smoke = GameObject.Instantiate(smokePrefab, transform.parent.position, transform.parent.rotation);
                    smoke.transform.localScale = gameObject.transform.parent.transform.localScale;
                    smoke.transform.localScale *= 50;
                }
                if (scrapCrumble != null)
                {
                    scrapCrumble.SeparateScrap();
                }

                objectsPoolManager.ReturnToPool(transform.parent.gameObject);
            }           
        }
    }
}