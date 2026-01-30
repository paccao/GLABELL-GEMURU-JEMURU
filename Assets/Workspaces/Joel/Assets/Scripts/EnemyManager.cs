using UnityEngine;

namespace Workspaces.Joel.Assets.Scripts
{
    public class EnemyManager : MonoBehaviour
    {
        public GameObject enemy;
        public Transform enemySpawnFolder;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Instantiate(enemy, new Vector3(9,9,0), Quaternion.identity, enemySpawnFolder );
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}