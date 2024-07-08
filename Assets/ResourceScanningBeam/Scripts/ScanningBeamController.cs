using UnityEngine;

namespace ScanningBeam
{
    public class ScanningBeamController : MonoBehaviour
    {
        [Header("Scanning beam vars")]
        [SerializeField] private Sprite resourcesScanningBeamSprite;

        public Sprite GetResourcesScanningBeamSprite
        { get { return resourcesScanningBeamSprite; } }
    }
}

