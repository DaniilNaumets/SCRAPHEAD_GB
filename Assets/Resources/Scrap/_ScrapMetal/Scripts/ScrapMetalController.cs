using System.Collections.Generic;
using UnityEngine;

namespace Resources
{
    public class ScrapMetalController : MonoBehaviour
    {
        [Header("ScrapMetal parameters")]
        [SerializeField] private float hpAndMass;
        [SerializeField] private float damageDealt;

        [Header("Movements values")]
        [SerializeField] private float impulseStrength;
        [SerializeField] private float impulseRotation;

        [Header("ScrapMetal pickup")]
        [SerializeField] private bool isPickup;
        [SerializeField, ConditionalField("isPickup")] private int scrapMetalValue;
        [SerializeField, ConditionalField("isPickup")] private float collectionTime;

        [Header("ScrapMetal crumble")]
        [SerializeField] private bool isCrumble;
        [SerializeField, ConditionalField("isCrumble")] private List<GameObject> fragments;

        [Header("Components")]
        [SerializeField] private ScrapMovement scrapMovement;
        [SerializeField] private ScrapRotation scrapRotation;
        [SerializeField] private ScrapHealth scrapHealth;
        [SerializeField] private ScrapMass scrapMass;
        [SerializeField] private ScrapPickup scrapPickup;
        [SerializeField] private ScrapDamageDealt scrapDamageDealt;
        [SerializeField] private ScrapCrumble scrapCrumble;

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
            scrapCrumble.InitialCrumble(fragments, isCrumble);


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
