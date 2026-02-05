using System.Collections;
using System.Collections.Generic;
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

        [SerializeField] GameObject hitBox;

        [Header("Sound stuffs")]
        [SerializeField] public FiskLjud fiskLjud;

        private bool isAttacking = false;

        private void Start()
        {
            player = GameManager.Instance.Player;
            currentPhaseConfig = GameManager.Instance.GetCurrentPhaseConfig();
            healthComponent = GetComponent<EnemyHealth>();

            movementSpeed *= currentPhaseConfig.enemySpeedMultiplier;
            healthComponent.maxHealth *= currentPhaseConfig.enemyHealthMultiplier;
        }

        private void FixedUpdate()
        {
            if (player == null)
            {
                GameManager.Instance.UpdatePlayerReference();
            }

            DetectPlayer();
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
                HandleChasingState();
            }
            // Enemy not in range of player
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * range, Color.red);
                HandleSeekingState();
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

        private void SetNewRandomDirection()
        {
            currentRandomDirection = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                0f
            ).normalized;

            directionChangeTimer = 0f;
            directionChangeDuration = Random.Range(2.5f, 3.5f);
        }

        private void HandleIdleState() {

        }

        private void HandleSeekingState() {
            // Update direction change timer
            directionChangeTimer += Time.deltaTime;

            // Check if it's time to change direction
            if (directionChangeTimer >= directionChangeDuration)
            {
                SetNewRandomDirection();
            }

            // Fish "floaty" movement behaviour
            transform.position = Vector3.MoveTowards(
                transform.position,
                player.transform.position,
                movementSpeed * Time.deltaTime
            );
            transform.position += currentRandomDirection * movementSpeed * Time.deltaTime;
        }

        private void HandleChasingState() {

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            // Smoothly rotate towards player
            var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

            // Move towards player only if not at attack offset
            if (distanceToPlayer > attackOffset)
            {
                Vector3 directionToPlayerNormalized = (player.transform.position - transform.position).normalized;
                Vector3 targetPosition = player.transform.position - (directionToPlayerNormalized * attackOffset);

                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPosition,
                    movementSpeed * Time.deltaTime
                );

                if (!isAttacking)
                {
                    DoAttack();
                }
            }
        }
    }
}
