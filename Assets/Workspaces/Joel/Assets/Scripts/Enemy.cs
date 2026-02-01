using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Workspaces.Joel.Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float range = 0f;
        [SerializeField] private float rotateSpeed = 0f;
        [SerializeField] private float movementSpeed = 0f;
        [SerializeField] private float attackOffset = 2f;
        [SerializeField] private LayerMask maskToHit;
        private GameManager.EnemySpawnPhaseConfig currentPhaseConfig;
        private EnemyHealth healthComponent;
        private GameObject player;

        [SerializeField] private float directionChangeTimer = 3f;
        private float directionChangeDuration;
        private Vector3 currentRandomDirection;
        
        private bool isAttacking = false;
        public GameObject hitBox;

        [Header("Sound stuffs")] 
        [SerializeField] public FiskLjud fiskLjud;

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
            
            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.Normalize();
            DetectPlayer(directionToPlayer);
        }

        private void DetectPlayer(Vector3 directionToPlayer)
        {
            Ray ray = new Ray(transform.position, directionToPlayer);
            RaycastHit hitInfo;

            // Enemy in range of player
            if (Physics.Raycast(ray, out hitInfo, range, maskToHit))
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.green);
                
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
                }
                else if(!isAttacking)
                {
                    DoAttack();
                }
            }
            
            // Enemy not in range of player
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * range, Color.red);

                // Update direction change timer
                directionChangeTimer += Time.deltaTime;

                // Check if it's time to change direction
                if (directionChangeTimer >= directionChangeDuration)
                {
                    // Reset timer and set a new random direction
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
        }
        
        void DoAttack()
        {
            if (isAttacking) return;
            StartCoroutine(AttackCooldown());
        }

        IEnumerator AttackCooldown()
        {
            yield return new WaitForSeconds(1);
            isAttacking = true;
            hitBox.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            hitBox.SetActive(false);
            isAttacking = false;
        }
        
        void SetNewRandomDirection()
        {
            // Generate random direction in X and Y plane
            currentRandomDirection = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                0f
            ).normalized;

            // Reset the timer
            directionChangeTimer = 0f;

            // Set a new random duration between 2.5 and 3.5 seconds
            directionChangeDuration = Random.Range(2.5f, 3.5f);
        }
    }
}
