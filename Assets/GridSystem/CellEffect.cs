using System;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CellEffect
{
    public CellEffectEnum cellTerrainEnum;
    public float walkPenalty;
    public float damageMulti;
    public float spreadChance;
    public float deathChance;
    public int damagePerSecond;
    private int age;
    public int life;
    public Sprite sprite;
    public Cell parent;
    public float nAge { get { return (float)age / (float) life; } }

    internal void OnSecondUpdate()
    {
        TakeDamage();
        UpdateAge();
        RandomGrowDeath();

    }

    private void RandomGrowDeath()
    {
        float rand1 = UnityEngine.Random.Range(0.0f, 1.0f);
        if (rand1 < spreadChance * (1 - nAge))
        {
            Cell cell = parent.gridRef.GetRandomNeighbour(parent);
            if (cell != null)
            {
                if (cell.cellEffect == null)
                {
                    cell.CreateCellEffect(cellTerrainEnum);
                }
            }
        }
        float rand2 = UnityEngine.Random.Range(0.0f, 1.0f);
        if (rand2 < deathChance * nAge)
        {

            RemoveCellEffect();
        }
    }

    private void UpdateAge()
    {
        age++;
        if (age >= life)
        {

            RemoveCellEffect();
            return;
        }
    }

    private void TakeDamage()
    {
        List<SceneObject> sceneObjects = SceneObjectManager.Instance.sceneObjectGetter.GetSceneObjects(parent.worldPosition, maxDistance: 1.5f, onlyDamageables: true);
        foreach (SceneObject sceneObject in sceneObjects)
        {
            IDamageAble damageAble = sceneObject as IDamageAble;
            damageAble.idamageableComponent.TakeDamage(damagePerSecond);

        }
    }

    public void RemoveCellEffect()
    {
        TimeActions.OnSecondChange -= OnSecondUpdate;
        parent.RemoveCellEffect();
        CellActions.UpdateCellEffect.Invoke(new CellEffectUpdateArgs { cell = parent, cellEffect = CellEffectEnum.None });
    }
}


internal class CellEffectCreator
{
    internal static CellEffect CreateCellEffect(CellEffectEnum cellEffectEnum, Cell parent)
    {
        CellEffect cellEffect = new CellEffect();
        cellEffect.parent = parent;
        switch (cellEffectEnum)
        {
            
            case CellEffectEnum.Gas:
                cellEffect.cellTerrainEnum = CellEffectEnum.Gas;
                cellEffect.walkPenalty = .5f;
                cellEffect.damageMulti = 1;
                cellEffect.spreadChance = 0.0f;
                cellEffect.deathChance = 0.0f;
                cellEffect.damagePerSecond = 2 * PlayerGlobalsManager.instance.playerGlobalVariables.gasDamageMulti * BattleSceneManager.instance.playerGlobalVariables.gasDamageMulti;
                cellEffect.life = 10 * PlayerGlobalsManager.instance.playerGlobalVariables.gasLifeTimeMulit * BattleSceneManager.instance.playerGlobalVariables.gasLifeTimeMulit;
                break;
            case CellEffectEnum.Fire:
                cellEffect.cellTerrainEnum = CellEffectEnum.Fire;
                cellEffect.walkPenalty = .5f;
                cellEffect.damageMulti = 1;
                cellEffect.spreadChance = 0.1f;
                cellEffect.deathChance = 0.15f;
                cellEffect.damagePerSecond = 2 * PlayerGlobalsManager.instance.playerGlobalVariables.fireDamageMulti * BattleSceneManager.instance.playerGlobalVariables.fireDamageMulti;
                cellEffect.life = 10;
                break;
        }
        TimeActions.OnSecondChange += cellEffect.OnSecondUpdate;
        return cellEffect;
    }
}
