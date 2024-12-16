using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
[CreateAssetMenu(menuName = "ScriptableObjects/Cards/Attilery")]

public class SoCardDamageArea : SoCardBase, IHasClickEffect

{
    public int damage;
    public float delay;
    public float diameter;
    public VisualEffect visualEffect;
    public Sprite clickSprite;

    public override void ActualEffect(Vector3 position)
    {
        AreaDamage.Create(position,diameter, visualEffect, damage, delay);
    }

    public Sprite GetSprite()
    {
        return clickSprite;
    }

    public float GetSpriteSize()
    {
        return diameter;
    }
}
