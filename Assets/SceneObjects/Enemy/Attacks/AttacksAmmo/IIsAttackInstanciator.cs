using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIsAttackInstanciator 
{
    public void Trigger(Enemy enemy, IDamageable idamageableByEnememy);
}
