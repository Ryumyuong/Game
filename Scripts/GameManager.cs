using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
    [SerializeField]
    // using TMPro를 설정해야 사용 가능
    private TextMeshProUGUI text;
    private int coin = 0;

    // [SerializeField]와 반대로 숨기는 기능
    [HideInInspector]
    public bool isGameOver = false;

    // start보다 더 빨리 호출되는 메소드
    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void IncreaseCoin() {
        coin += 1;
        text.SetText(coin.ToString());

        if(coin % 10 == 0) {
            // project 내에서 객체 찾기
            Player player = FindObjectOfType<Player>();
            if(player != null) {
                player.Upgrade();
            }
        }
    }

    // bool istrue를 이용하면 성공, 실패 구분 가능
    public void SetGameOver() {
        isGameOver = true;
        
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if(enemySpawner != null) {
            enemySpawner.StopEnemyRoutine();
        }
    }
}
