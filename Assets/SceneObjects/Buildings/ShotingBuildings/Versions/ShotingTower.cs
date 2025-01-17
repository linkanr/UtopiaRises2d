using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotingTower : ShootingBuilding
{

    protected void Update()
    {

        if (targeter.target !=null)
        {
            //Debug.Log("rotataing");
            Transform targetTransform = targeter.target.transform;
            
            if (targetTransform != null) 
            {
                // IF IT HAS A TARGET THEN LOOK AT THAT
                Vector3 relativePos = targetTransform.position - GetStatsHandler().GetStats().sceneObjectTransform.position;
                float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), BattleClock.Instance.deltaValue * 10f);

            }

        }
    }

}
