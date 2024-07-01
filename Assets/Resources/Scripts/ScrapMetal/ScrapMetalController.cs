using System.Collections.Generic;
using UnityEngine;

namespace Resources
{
    public class ScrapMetalController : MonoBehaviour
    {
        [Header("ScrapMetal parameters")]
        [SerializeField] private float hpAndMass;
        [SerializeField] private float damageDealt;

        [Header("Absolute values")]
        [SerializeField] private float impulseStrength;
        [SerializeField] private float impulseRotation;

        //[Header("Randomize parameters")]
        //[SerializeField][Range(1, 10)] private int spawnFrequency;

        [Header("ScrapMetal pickup")]
        [SerializeField] private bool isPickup;
        [SerializeField, ConditionalField("isPickup")] private int scrapMetalValue;
        [SerializeField, ConditionalField("isPickup")] private float collectionTime;

        [Header("ScrapMetal crumble")]
        [SerializeField] private bool isCrumble;
        [SerializeField, ConditionalField("isCrumble")] private List<GameObject> fragments;

        [Header("Components")]
        [SerializeField] private ScrapMetalMovement scrapMetalMovement;
        [SerializeField] private ScrapMetalRotation scrapMetalRotation;
        [SerializeField] private ScrapMetalHealth scrapMetalHealth;
        [SerializeField] private ScrapMetalMass scrapMetalMass;
        [SerializeField] private ScrapPickup scrapPickup;
        [SerializeField] private ScrapDamageDealt scrapDamageDealt;
        [SerializeField] private ScrapCrumble scrapCrumble;
       // [SerializeField] private ScrapSpawnFrequency scrapSpawnFrequency;

        private void Awake()
        {
            InitializeScrapMetal();
        }

        public void InitializeScrapMetal()
        {
            scrapMetalMass.InitializeMass(hpAndMass);
            scrapMetalHealth.InitializeHealth(hpAndMass);

            float adjustedImpulseStrength = ChangeValueRelativeToMass(impulseStrength);
            float adjustedImpulseRotation = ChangeValueRelativeToMass(impulseRotation);

            scrapMetalMovement.InitializeImpulse(adjustedImpulseStrength);
            scrapMetalRotation.InitializeRotation(adjustedImpulseRotation);

            scrapDamageDealt.InitializeDamage(damageDealt);
            scrapCrumble.InitialCrumble(fragments, isCrumble);

            //scrapSpawnFrequency.InitializedSpawnFrequency(spawnFrequency);

            if (isPickup)
            {
                scrapPickup.SetTransmittedValue(scrapMetalValue, collectionTime);
            }
            else
            {
                Destroy(scrapPickup);
            }
        }

        private float ChangeValueRelativeToMass(float value)
        {
            return hpAndMass * value;
        }
    }
}
