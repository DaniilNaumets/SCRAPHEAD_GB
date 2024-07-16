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

        private float currentHealth;
        private bool isDestroy;

        public void InitializeHealth(float health)
        {
            currentHealth = health;
            isDestroy = false;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;          

            if (currentHealth <= 0 && !isDestroy) 
            {
                isDestroy = true;
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
                
                scrapTurnOff.TurnOff();
            }           
        }


    }
}

