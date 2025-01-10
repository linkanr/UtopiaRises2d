using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : Building, IDamageAble

{


    protected override void OnObjectDestroyed()
    {
        Debug.LogWarning("game Over");
    }


}
