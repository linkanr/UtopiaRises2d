using System.Diagnostics;
using UnityEngine;

public static class FisicalResources
{
	public static bool TryToBuy(int cost)
	{
		if (cost <= SceneObjectManager.Instance.influence)
		{
			UnityEngine.Debug.Log("influence cost is " + cost + " influence value is " + SceneObjectManager.Instance.influence);
            SceneObjectManager.Instance.AddInfluence(-cost);
			return true;
        }
		else
		{
			return false;
		}
	}
}