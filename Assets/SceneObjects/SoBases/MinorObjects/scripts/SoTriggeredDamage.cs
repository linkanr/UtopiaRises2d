using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
[CreateAssetMenu(menuName = "ScriptableObjects/MinorObject/DamageTriggered")]
public class SoTriggeredDamage : SoMinorObject
{
   



    protected override Stats GetStatsInernal(Stats stats)
    {
        base.GetStatsInernal(stats);

       
        return stats;
    }


}
