using System;
using UnityEngine;

public interface IDamagableTimeComponent : IDamagableComponent
{
    public TimeHealthSystem healthSystem { get; set; }

}
