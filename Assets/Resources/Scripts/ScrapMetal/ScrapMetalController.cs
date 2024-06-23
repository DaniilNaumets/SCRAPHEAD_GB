using UnityEngine;

namespace Resources
{
    public class ScrapMetalController : MonoBehaviour
    {
        [Header("ScrapMetal parameters")]
        [SerializeField] private float hpAndMass;
        [SerializeField] public float DamageDealt;//

        [Header("Absolute values")]
        [SerializeField] private float impulseStrength;
        [SerializeField] private float impulseRotation;

        [Header("ScrapMetal pickup")]
        [SerializeField] private bool isPickup;
        [SerializeField, ConditionalField("isPickup")] private int scrapMetalValue;
        [SerializeField, ConditionalField("isPickup")] private float collectionTime;
        [SerializeField, ConditionalField("isPickup", true)] public int QuantityFragments;//     

        [Header("Components")]
        [SerializeField] private ScrapMetalMovement scrapMetalMovement;
        [SerializeField] private ScrapMetalRotation scrapMetalRotation;
        [SerializeField] private ScrapMetalHealth scrapMetalHealth;
        [SerializeField] private ScrapMetalMass scrapMetalMass;
        [SerializeField] private ScrapPickup scrapPickup;

        private void Awake()
        {
            InitializeScrapMetal();
        }

        private void InitializeScrapMetal()
        {
            scrapMetalMass.InitializeMass(hpAndMass);
            scrapMetalHealth.InitializeHealth(hpAndMass);
            
            impulseStrength = ChangeValueRelativeToMass(impulseStrength);
            impulseRotation = ChangeValueRelativeToMass(impulseRotation);

            scrapMetalMovement.InitializeImpulse(impulseStrength);
            scrapMetalRotation.InitializeRotation(impulseRotation);

            scrapPickup.SetTransmittedValue(scrapMetalValue, collectionTime);
        }

        private float ChangeValueRelativeToMass(float value)
        {
            return hpAndMass * value;
        }
    }
}

