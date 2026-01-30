using UnityEngine;

namespace Workspaces.Joel.Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float range = 0f;
        [SerializeField] private float rotateSpeed = 0f;
        [SerializeField] private float movementSpeed = 0f;
        [SerializeField] private LayerMask mask;
        [SerializeField] private float idleDirectionChangeInterval = 3f;
        private GameObject player;
        

        private void Start()
        {
            player = GameManager.Instance.Player;
        }
        
        private void FixedUpdate()
        {
            Vector3 directionToPlayer = player.transform.position - transform.position;
            directionToPlayer.Normalize();
            DetectPlayer(directionToPlayer);
        }

        private void DetectPlayer(Vector3 directionToPlayer)
        {
            Ray ray = new Ray(transform.position, directionToPlayer);
            RaycastHit hitInfo;

            // Enemy in range of player
            if (Physics.Raycast(ray, out hitInfo, range, mask))
            {
                Debug.DrawLine(ray.origin, hitInfo.point, Color.green);

                // Smoothly rotate towards player
                var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                
                // Move towards player
                transform.position = Vector3.MoveTowards(
                    transform.position, 
                    player.transform.position, 
                    movementSpeed * Time.deltaTime
                );
            }
            // Enemy not in range of player
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * range, Color.red);

                // Rotate towards origin (0,0,0)
                Vector3 originDirection = -transform.position.normalized;
                var targetRotation = Quaternion.LookRotation(originDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);

                // Generate random direction in X and Y plane
                Vector3 randomDirection = new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f),
                    0f
                ).normalized;

                // Move in this random direction
                transform.position += randomDirection * movementSpeed * Time.deltaTime;
            }
        }
    }
}
