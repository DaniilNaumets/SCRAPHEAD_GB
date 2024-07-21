using ObjectPool;
using System.Collections;
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

        private bool isCollisionNow;

        private SpriteRenderer render;

        private void Awake()
        {
            render = gameObject.transform.parent.GetComponentInChildren<SpriteRenderer>();
        }

        public void InitializeHealth(float health)
        {
            currentHealth = health;
            objectsPoolManager = FindObjectOfType<ObjectsPoolManager>();
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (!isCollisionNow)
                StartCoroutine(Red());
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
        private IEnumerator Red()
        {
            isCollisionNow = true;
            render.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            render.color = Color.white;
            isCollisionNow = false;
        }
    }


}