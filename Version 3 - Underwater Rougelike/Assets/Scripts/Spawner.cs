using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnRate;

    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private bool canSpawn = true;

    // Check for player periodically
    [SerializeField] private float checkForPlayer = 1f; // Interval to check for player
    [SerializeField] private float noPlayerDestroyEnemies = 2f; // Time without player before enemies are destroyed

    private float timeWithoutPlayer = 0f;
    private GameObject player;
    private bool playerPresent = true;

    private void Start()
    {
        StartCoroutine(Spawn());
        StartCoroutine(checkingPlayer());
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);

        while (canSpawn)
        {
            yield return wait;

            int rand = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyToSpawn = enemyPrefabs[rand];

            GameObject spawnedEnemy = Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
            spawnedEnemy.GetComponent<Enemies>().StartFollowing();
        }
    }

    private IEnumerator checkingPlayer()
    {
        WaitForSeconds wait = new WaitForSeconds(checkForPlayer);

        while (true)
        {
            yield return wait;

            player = GameObject.FindGameObjectWithTag("Player");

            if (player == null)
            {
                if (playerPresent)
                {
                    playerPresent = false;
                    timeWithoutPlayer = 0f;
                }
                timeWithoutPlayer += checkForPlayer;

                if (timeWithoutPlayer >= noPlayerDestroyEnemies)
                {
                    DestroyAllEnemies();
                }
            }
            else
            {
                playerPresent = true;
                timeWithoutPlayer = 0f;
            }
        }
    }

    private void DestroyAllEnemies()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("ExploFish");

        foreach (GameObject enemy in allEnemies)
        {
            Destroy(enemy);
        }

        canSpawn = false;
        Debug.Log("All enemies destroyed due to player absence.");
    }
}
