#if UNITY_EDITOR
using UnityEditor;
using System;

[CustomEditor(typeof(GameDifficulty.GameDifficultyController))]
public class GameDifficultyControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameDifficulty.GameDifficultyController controller = (GameDifficulty.GameDifficultyController)target;

        if (controller.numberOfStages != controller.quantityOfScrapInStage.Length)
        {
            Array.Resize(ref controller.quantityOfScrapInStage, (int)controller.numberOfStages);
            Array.Resize(ref controller.minSpawnTime, (int)controller.numberOfStages);
            Array.Resize(ref controller.maxSpawnTime, (int)controller.numberOfStages);
            Array.Resize(ref controller.minAmount, (int)controller.numberOfStages);
            Array.Resize(ref controller.maxAmount, (int)controller.numberOfStages);
            Array.Resize(ref controller.numberOfSpawners, (int)controller.numberOfStages);
            Array.Resize(ref controller.aggressiveStateOnPercentage, (int)controller.numberOfStages);
            Array.Resize(ref controller.enableSpawner, (int)controller.numberOfStages);
        }
    }
}
#endif
