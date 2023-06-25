using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject boss;

    // 각 x값 배열 생성
    private float[] arrPosX = {-2.2f, -1.1f, 0f, 1.1f, 2.2f};

    [SerializeField]
    private float spawnInterval = 1.5f;
    
    void Start()
    {
        StartEnemyRoutine();
    }

    void StartEnemyRoutine() {
        StartCoroutine("EnemyRoutine");
    }

    public void StopEnemyRoutine() {
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine() {
        // 3초는 기다리고 실행
        yield return new WaitForSeconds(3f);

        float moveSpeed = 5f;
        int spawnCount = 0;
        int enemyIndex = 0;

        while(true) {
            foreach(float posX in arrPosX) {
                // int index = Random.Range(0, enemies.Length);
                SpawnEnemy(posX, enemyIndex, moveSpeed);
            }
            spawnCount++;
            if (spawnCount % 10 == 0) {
                enemyIndex += 1;
                moveSpeed += 2;
            }

            if(enemyIndex >= enemies.Length) {
                SpawnBoss();
                // 레벨 1이 다시 나옴
                enemyIndex = 0;
                moveSpeed = 5f;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy(float posX, int index, float moveSpeed) {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);

        // 20%의 확률로 index 1 증가
        if(Random.Range(0,5) == 0) {
            index += 1;
        }

        if(index >= enemies.Length) {
            index= enemies.Length-1;
        }
        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetMoveSpeed(moveSpeed);
        
    }

    void SpawnBoss() {
        Instantiate(boss, transform.position, Quaternion.identity);
    }
}
