using UnityEngine;

namespace Resources
{
    public class ScrapMetalController : MonoBehaviour
    {
        [Header("ScrapMetal parameters")]
        [SerializeField] private float hpAndMass;
        [SerializeField] private int value;//
        [SerializeField] public float DamageDealt;
        [SerializeField] public int QuantityFragments;

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

            scrapPickup.SetValue(value);
        }

        private float ChangeValueRelativeToMass(float value)
        {
            return hpAndMass * value;
        }
    }
}

