using UnityEngine;

namespace Workspaces.Joel.Assets.Scripts
{
    public abstract class EnemyState
    {
        protected Enemy enemy;

        public EnemyState(Enemy enemy)
        {
            this.enemy = enemy;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }

    public class EnemyIdleState : EnemyState
    {
        public EnemyIdleState(Enemy enemy) : base(enemy) { }
    }

    public class EnemySeekingState : EnemyState
    {
        public EnemySeekingState(Enemy enemy) : base(enemy) { }

        public override void Update()
        {
            Debug.Log("Seeking inside!");
            if (enemy.isChasing)
            {
                enemy.ChangeState(new EnemyChasingState(enemy));
            }

            // Update direction change timer
            enemy.directionChangeTimer += Time.deltaTime;

            // Check if it's time to change direction
            if (enemy.directionChangeTimer >= enemy.directionChangeDuration)
            {
                SetNewRandomDirection();
            }

            Debug.Log(enemy.player);

            // Fish "floaty" movement behaviour
            enemy.transform.position = Vector3.MoveTowards(
                enemy.transform.position,
                enemy.player.transform.position,
                enemy.movementSpeed * Time.deltaTime
            );
            enemy.transform.position += enemy.currentRandomDirection * enemy.movementSpeed * Time.deltaTime;
        }

        void SetNewRandomDirection()
        {
            enemy.currentRandomDirection = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                0f
            ).normalized;

            enemy.directionChangeTimer = 0f;
            enemy.directionChangeDuration = Random.Range(2.5f, 3.5f);
        }
    }

    public class EnemyChasingState : EnemyState
    {
        public EnemyChasingState(Enemy enemy) : base(enemy) { }

        public override void Update()
        {
            if (enemy.isRecharging)
            {
                enemy.ChangeState(new EnemyRechargingState(enemy));
            }

            float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);

            // Smoothly rotate towards player
            var targetRotation = Quaternion.LookRotation(enemy.player.transform.position - enemy.transform.position);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, targetRotation, enemy.rotateSpeed * Time.deltaTime);

            // Move towards player only if not at attack offset
            if (distanceToPlayer > enemy.attackOffset)
            {
                Vector3 directionToPlayerNormalized = (enemy.player.transform.position - enemy.transform.position).normalized;
                Vector3 targetPosition = enemy.player.transform.position - (directionToPlayerNormalized * enemy.attackOffset);

                enemy.transform.position = Vector3.MoveTowards(
                    enemy.transform.position,
                    targetPosition,
                    enemy.movementSpeed * Time.deltaTime
                );

                if (!enemy.isAttacking)
                {
                    enemy.DoAttack();
                }
            }
        }
    }

    public class EnemyRechargingState : EnemyState
    {
        public EnemyRechargingState(Enemy enemy) : base(enemy) { }

        public override void Update()
        {
            if (enemy.isChasing)
            {
                enemy.ChangeState(new EnemyChasingState(enemy));
            }
            else if (enemy.isSeeking)
            {
                enemy.ChangeState(new EnemySeekingState(enemy));
            }
        }
    }
}
