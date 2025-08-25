using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instance : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects = new List<GameObject>();
    [SerializeField] private Transform[] _Transform;
    private int currentEnemyCount = 3;
    private int aliveEnemies = 0;
    //private bool isSpawning = false;
    //private GameObject _gameObject;
    //private GameObject[] count;
    private void Start()
    {
        Spawn(currentEnemyCount);
    }
    private void Spawn(int count)
    {        
        for (int i = 0; i < count; i++)
        {
            int random_position = Random.Range(0, _Transform.Length);
            int random_enemy = Random.Range(0, objects.Count);
            GameObject enemy = Instantiate(objects[random_enemy], _Transform[random_position].position,Quaternion.identity);
            enemy.SetActive(true);
            enemy.GetComponent<Enemy>().OnEnemyDeath += OnEnemyDeath;            
        }
        aliveEnemies = count;
                
    }
    private IEnumerator SpawnEnemiesWithDelay(int count, float delay)
    {
        yield return new WaitForSeconds(delay);
        Spawn(count);
    }

    private void OnEnemyDeath()
    {
        aliveEnemies--;
        if(aliveEnemies == 0)
        {
            currentEnemyCount++;
            StartCoroutine(SpawnEnemiesWithDelay(currentEnemyCount, 1f));
        }
    }
}
