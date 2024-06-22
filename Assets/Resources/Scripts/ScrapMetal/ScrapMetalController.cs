using UnityEngine;

namespace Resources
{
    public class ScrapMetalController : MonoBehaviour
    {
        [Header("ScrapMetal parameters")]
        [SerializeField] private float hpAndMass;
        [SerializeField] private int scrapMetalValue;//
        [SerializeField] private float collectionTime;
        [SerializeField] public int QuantityFragments;//
        [SerializeField] public float DamageDealt;//     

        [Header("Absolute values")]
        [SerializeField] private float impulseStrength;
        [SerializeField] private float impulseRotation;

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

