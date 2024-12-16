using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotingTower : ShootingBuilding
{

    protected override void Update()
    {
        base.Update();
        if (target !=null)
        {
            //Debug.Log("rotataing");
            Transform targetTransform = target.transform;
            
            if (targetTransform != null) 
            {
                // IF IT HAS A TARGET THEN LOOK AT THAT
                Vector3 relativePos = targetTransform.position - stats.GetTransform(StatsInfoTypeEnum.objectToFollow).position;
                float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward),Time.deltaTime*10f);

            }

        }
    }

}
