using UnityEngine;

namespace GameDifficulty
{
    public class GameDifficultyController : MonoBehaviour
    {
        [Header("Stages")]
        [SerializeField] public uint numberOfStages = 0;

        [Header("Toggle spawn enable")]
        [SerializeField] private bool enable;

        [Header("Amount of scrap for transition")]
        [SerializeField] public uint[] quantityOfScrap = new uint[0];

        [Header("Number of spawn points")]
        [SerializeField] public uint[] numberOfSpawners = new uint[0];

        [Header("Spawn time")]
        [SerializeField] public float[] minSpawnTime = new float[0];
        [SerializeField] public float[] maxSpawnTime = new float[0];

        [Header("Spawn amount")]
        [SerializeField] public uint[] minAmount = new uint[0];
        [SerializeField] public uint[] maxAmount = new uint[0];
    }
}
