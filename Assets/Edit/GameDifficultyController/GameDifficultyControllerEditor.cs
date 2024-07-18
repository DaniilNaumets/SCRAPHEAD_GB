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

        if (controller.numberOfStages != controller.quantityOfScrap.Length)
        {
            Array.Resize(ref controller.quantityOfScrap, (int)controller.numberOfStages);
            Array.Resize(ref controller.minSpawnTime, (int)controller.numberOfStages);
            Array.Resize(ref controller.maxSpawnTime, (int)controller.numberOfStages);
            Array.Resize(ref controller.minAmount, (int)controller.numberOfStages);
            Array.Resize(ref controller.maxAmount, (int)controller.numberOfStages);
            Array.Resize(ref controller.numberOfSpawners, (int)controller.numberOfStages);
        }
    }
}
#endif
