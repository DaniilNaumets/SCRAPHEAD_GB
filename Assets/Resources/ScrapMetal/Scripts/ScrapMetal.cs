using UnityEngine;

namespace Resources
{
    [CreateAssetMenu(fileName = "NewScrapMetal", menuName = "ScrapMetal")]
    public class ScrapMetal : ScriptableObject
    {
        [SerializeField] public new string name;
        [SerializeField] public float value;
        [SerializeField] public float hpAndMass;
        [SerializeField] public float impulseStrength;
        [SerializeField] public float rotationForce;
        [SerializeField] public float damageDealt;
        [SerializeField] public int quantityFragments;
        [SerializeField] public Sprite sprite;
    }
}

