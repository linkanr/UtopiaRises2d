using System;

public class EnemyStateMachine: BaseStateMachine<EnemyStateMachine>
{
    public Enemy enemy;
    
    protected override void Init()
    {
        enemy = GetComponent<Enemy>();
        enemy.healthSystem.OnKilled += StopState;
        
       
    }

    private void StopState(object sender, OnSceneObjectDestroyedArgs e)
    {
        Stopp();
    }
    protected override void OnDestroy()
    {
        if (enemy != null && enemy.healthSystem != null)
        {
            enemy.healthSystem.OnKilled -= StopState;
        }

        base.OnDestroy();
    }
}