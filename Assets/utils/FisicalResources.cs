using System.Diagnostics;
using UnityEngine;

public static class FisicalResources
{
	public static bool TryToBuy(int cost)
	{
		if (cost <= PlayerGlobalsManager.instance.influence)
		{
		//	UnityEngine.Debug.Log("influence cost is " + cost + " influence value is " + SceneObjectManager.Instance.influence);
            
			return true;
        }
		else
		{
			return false;
		}
	}
	public static void Buy(int cost)
	{
		
        PlayerGlobalsManager.instance.AddInfluence(-cost);
    }
}