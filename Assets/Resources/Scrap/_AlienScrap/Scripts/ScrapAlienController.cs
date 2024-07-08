using UnityEngine;

namespace Resources
{
    public class ScrapAlienController : MonoBehaviour
    {
        [Header("ScrapAlien parameters")]
        [SerializeField] private float hpAndMass;
        [SerializeField] private float damageDealt;

        [Header("Movements values")]
        [SerializeField] private float impulseStrength;
        [SerializeField] private float impulseRotation;

        [Header("ScrapAlien pickup")]      
        [SerializeField] private int scrapMetalValue;
        [SerializeField] private float collectionTime;  

        [Header("Components")]
        [SerializeField] private ScrapMovement scrapMovement;
        [SerializeField] private ScrapRotation scrapRotation;
        [SerializeField] private ScrapHealth scrapHealth;
        [SerializeField] private ScrapMass scrapMass;
        [SerializeField] private ScrapPickup scrapPickup;
        [SerializeField] private ScrapDamageDealt scrapDamageDealt;

        private void Awake()
        {
            InitializeScrapMetal();
        }

        public void InitializeScrapMetal()
        {
            scrapMass.InitializeMass(hpAndMass);
            scrapHealth.InitializeHealth(hpAndMass);

            float adjustedImpulseStrength = ChangeValueRelativeToMass(impulseStrength);
            float adjustedImpulseRotation = ChangeValueRelativeToMass(impulseRotation);

            scrapMovement.InitializeImpulse(adjustedImpulseStrength);
            scrapRotation.InitializeRotation(adjustedImpulseRotation);

            scrapDamageDealt.InitializeDamage(damageDealt);
            scrapPickup.SetTransmittedValue(scrapMetalValue, collectionTime);
        }

        private float ChangeValueRelativeToMass(float value)
        {
            return hpAndMass * value;
        }
    }
}

