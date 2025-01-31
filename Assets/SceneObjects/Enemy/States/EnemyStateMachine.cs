using System;

public class EnemyStateMachine: BaseStateMachine<EnemyStateMachine>
{
    public Enemy enemy;
    
    protected override void Init()
    {
        enemy = GetComponent<Enemy>();
        enemy.idamageableComponent.OnDeath += StopState;
        
       
    }

    private void StopState(object sender, IdamageAbleArgs e)
    {
        Stopp();
    }
    protected override void OnDestroy()
    {
        if (enemy != null && enemy.idamageableComponent != null)
        {
            enemy.idamageableComponent.OnDeath -= StopState;
        }

        base.OnDestroy();
    }
}