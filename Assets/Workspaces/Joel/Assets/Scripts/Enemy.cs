using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Workspaces.Joel.Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] internal float range = 0f;
        [SerializeField] internal float rotateSpeed = 0f;
        [SerializeField] internal float movementSpeed = 0f;
        [SerializeField] internal float attackOffset = 2f;
        [SerializeField] private LayerMask maskToHit;
        private GameManager.EnemySpawnPhaseConfig currentPhaseConfig;
        private EnemyHealth healthComponent;
        internal GameObject player;

        [SerializeField] internal float directionChangeTimer = 3f;
        internal float directionChangeDuration;
        internal Vector3 currentRandomDirection;

        public GameObject hitBox;

        // TODO: remove serializeField
        [SerializeField] private EnemyState currentState;
        [SerializeField] internal bool isIdle = false;
        [SerializeField] internal bool isSeeking = false;
        [SerializeField] internal bool isChasing = false;
        [SerializeField] internal bool isAttacking = false;
        [SerializeField] internal bool isRecharging = false;

        [Header("Sound stuffs")]
        [SerializeField] public FiskLjud fiskLjud;

        private void Start()
        {
            player = GameManager.Instance.Player;
            currentPhaseConfig = GameManager.Instance.GetCurrentPhaseConfig();
            healthComponent = GetComponent<EnemyHealth>();

            movementSpeed *= currentPhaseConfig.enemySpeedMultiplier;
            healthComponent.maxHealth *= currentPhaseConfig.enemyHealthMultiplier;

            // Set random enemy behaviour
            int randomInitialState = Random.Range(0, 1);
            if (randomInitialState == 0)
            {
                isSeeking = true;
                currentState = new EnemySeekingState(this);
            }
            else if (randomInitialState == 1)
            {
                // If the enemy gets idle state they will not change behaviour, e.g chase the player
                isIdle = true;
                currentState = new EnemyIdleState(this);
            }
            Debug.Log(randomInitialState);

            currentState.Enter();
        }

        private void FixedUpdate()
        {
            if (player == null)
            {
                GameManager.Instance.UpdatePlayerReference();
            }

            if (!isIdle) DetectPlayer();
        }

        public void ChangeState(EnemyState newState)
        {
            currentState.Exit();
            currentState = newState;
            currentState.Enter();
        }

        private void DetectPlayer()
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.Normalize();

            Ray ray = new Ray(transform.position, directionToPlayer);

            // Enemy in range of player
            if (Physics.Raycast(ray, out RaycastHit hitInfo, range, maskToHit))
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.green);

                isSeeking = false;
                isRecharging = false;

                isChasing = true;
            }
            // Enemy not in range of player
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * range, Color.red);

                isChasing = false;
                isRecharging = false;

                Debug.Log("Seeking!");
                isSeeking = true;
            }
        }

        internal void DoAttack()
        {
            if (isAttacking) return;
            StartCoroutine(AttackCooldown());
        }

        internal IEnumerator AttackCooldown()
        {
            yield return new WaitForSeconds(1);
            isAttacking = true;
            hitBox.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            hitBox.SetActive(false);
            isAttacking = false;
        }
    }
}
