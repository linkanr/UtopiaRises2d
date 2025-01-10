using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
[CreateAssetMenu(menuName = "Cards/Attilery")]

public class CardDamageArea : SoCardBase, IHasClickEffect

{
    public int damage;
    public float delay;
    public float diameter;
    public VisualEffect visualEffect;
    public Sprite clickSprite;

    public override bool ActualEffect(Vector3 position, out string failureReason)
    {
        AreaDamage.Create(position,diameter, visualEffect, damage, delay);
        failureReason = "";
        return true;
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
