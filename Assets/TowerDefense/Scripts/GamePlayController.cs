using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;

    [SerializeField] int amountEnemy;
    [SerializeField] float speedBegin = 2.5f;
    [SerializeField] float timeSpawnEnemy = 0.2f;

    [SerializeField] EnemyMove enemy;
    public List<GameObject> listPointDirection = new List<GameObject>();
    int countEnemy = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        StartCoroutine(SpawnEnemyAutomatical());
    }

    IEnumerator SpawnEnemyAutomatical()
    {
        while(countEnemy <= amountEnemy)
        {
            yield return new WaitForSeconds(timeSpawnEnemy);
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {
        countEnemy++;
        EnemyMove _enemy = Instantiate(enemy, listPointDirection[0].transform.position,Quaternion.identity);
        _enemy.speedMove = speedBegin;
    }
    public void On_Click_SpawnEnemy()
    {
        SpawnEnemy();
    }
}
