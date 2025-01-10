using System;

public class EnemyStateMachine: BaseStateMachine<EnemyStateMachine>
{
    public Enemy enemy;
    
    protected override void Init()
    {
        enemy = GetComponent<Enemy>();
        
       
    }

    private void OnDisable()
    {
        
    }



}