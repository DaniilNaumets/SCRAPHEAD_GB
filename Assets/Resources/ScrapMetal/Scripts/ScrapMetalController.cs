using UnityEngine;

namespace Resources
{
    public class ScrapMetalController : MonoBehaviour
    {
        [Header("Scriptable Object")]
        [SerializeField] private ScrapMetal scrapMetal;

        [Header("Monitored Parameters")]
        [ReadOnly][SerializeField] private float hpAndMass;
        [ReadOnly][SerializeField] private float value;
        [ReadOnly][SerializeField] private float impulseStrength;
        [ReadOnly][SerializeField] private float rotationalSpeed;
        [ReadOnly][SerializeField] private float damageDealt;
        [ReadOnly][SerializeField] private int quantityFragments;

        [Header("Components")]
        [SerializeField] private Rigidbody2D rigidbodyScrapMetal;
        [SerializeField] private SpriteRenderer spriteRendererScrapMetal;

        public float Value
        { get { return value; } }

        public float ImpulseStrength
        { get { return impulseStrength; } }

        public float RotationalSpeed
        { get { return rotationalSpeed; } }

        private void Awake()
        {
            InitializationScrapMetal();
        }

        private void InitializationScrapMetal()
        {
            transform.parent.name = scrapMetal.name;
            hpAndMass = scrapMetal.hpAndMass;
            value = scrapMetal.value;
            impulseStrength = AverageValueOverMass(scrapMetal.impulseStrength);
            rotationalSpeed = AverageValueOverMass(scrapMetal.rotationForce);
            damageDealt = scrapMetal.damageDealt;
            quantityFragments = scrapMetal.quantityFragments;
            ConfigureComponents();
        }

        private void ConfigureComponents()
        {
            rigidbodyScrapMetal.mass = hpAndMass;
            spriteRendererScrapMetal.sprite = scrapMetal.sprite;
        }

        private float AverageValueOverMass(float value)
        {
            return hpAndMass * value;
        }
    }
}

