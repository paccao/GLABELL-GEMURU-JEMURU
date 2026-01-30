namespace Workspaces.Joel.Assets.Scripts
{
    using UnityEngine;

    public abstract class EnemyState
    {
        protected Enemy enemy;

        public EnemyState(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
        }
    }

    // public class IdleState : EnemyState
    // {
    //
    //     public IdleState(EnemyState enemy) : base(enemy) { }
    //
    // }
    //
    // public class ChaseState : EnemyState
    // {
    //
    //     public ChaseState(EnemyState enemy) : base(enemy) { }
    //
    // }

}