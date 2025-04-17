using System.Collections.Generic;
using UnityEngine;

public static class SceneObjectUtils
{
    public static List<SceneObject> GetObjectsAffectedByGlobalModifier(BasicGlobalVariableModifier modifier)
    {
        var result = new List<SceneObject>();

        if (SceneObjectManager.Instance == null)
        {
            Debug.LogWarning("SceneObjectManager is not available, probably not in a battle scene.");
            return result;
        }

        var allObjects = SceneObjectManager.Instance.RetriveSceneObjects(SceneObjectTypeEnum.playerbuilding);

        foreach (var obj in allObjects)
        {
            var stats = obj.GetStats();
            if (stats == null || stats.faction == null) continue;

            var query = new PlayerGlobalQuery(
                modifier.varType,
                0f,
                stats.faction
            );

            if (modifier.requirement?.MatchesQuery(query) == true)
            {
                result.Add(obj);
            }
        }

        return result;
    }

}
