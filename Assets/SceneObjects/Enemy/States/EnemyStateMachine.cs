using System;

public class EnemyStateMachine: BaseStateMachine<EnemyStateMachine>
{
    public Enemy enemy;
    
    protected override void Init()
    {
        enemy = GetComponent<Enemy>();
        enemy.iDamageableComponent.OnDeath += StopState;
        
       
    }

    private void StopState(object sender, IdamageAbleArgs e)
    {
        Stopp();
    }
    protected override void OnDestroy()
    {
        if (enemy != null && enemy.iDamageableComponent != null)
        {
            enemy.iDamageableComponent.OnDeath -= StopState;
        }

        base.OnDestroy();
    }
}