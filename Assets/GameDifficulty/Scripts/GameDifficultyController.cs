using UnityEngine;

namespace GameDifficulty
{
    public class GameDifficultyController : MonoBehaviour
    {
        [Header("Stages")]
        [SerializeField] public uint numberOfStages = 0;

        [Header("Percentage of aggressive enemies")]
        [SerializeField] private bool aggressionRegulator;
        [SerializeField, ConditionalField("aggressionRegulator")] public float[] aggressiveStateOnPercentage;//

        [Header("Toggle spawn enable")]
        [SerializeField] public bool[] enableSpawner;

        [Header("Amount of scrap for transition")]       
        [SerializeField] public int[] quantityOfScrapInStage = new int[0];

        [Header("Number of spawn points")]
        [SerializeField] public int[] numberOfSpawners = new int[0]; 

        [Header("Spawn time")]
        [SerializeField] public float[] minSpawnTime = new float[0];
        [SerializeField] public float[] maxSpawnTime = new float[0];

        [Header("Spawn amount")]
        [SerializeField] public int[] minAmount = new int[0];
        [SerializeField] public int[] maxAmount = new int[0];

        public bool GetAggressionRegulator() => aggressionRegulator; 
    }
}
