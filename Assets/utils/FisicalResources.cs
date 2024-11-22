using System.Diagnostics;
using UnityEngine;

public static class FisicalResources
{
	public static bool TryToBuy(int cost)
	{
		if (cost <= PlayerAssetsManager.Instance.influence)
		{
			UnityEngine.Debug.Log("influence cost is " + cost + " influence value is " + PlayerAssetsManager.Instance.influence);
            PlayerAssetsManager.Instance.AddInfluence(-cost);
			return true;
        }
		else
		{
			return false;
		}
	}
}