using UnityEngine;

namespace Workspaces.Joel.Assets.Scripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float range = 0f;
        [SerializeField] private float rotateSpeed = 0f;
        [SerializeField] private float movementSpeed = 0f;
        [SerializeField] private LayerMask mask;
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
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * range, Color.red);
            }
        }
    }
}