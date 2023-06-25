using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    
    [SerializeField]
    // using TMPro를 설정해야 사용 가능
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject gameOverPanel;

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

        if(coin % 30 == 0) {
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
        // 지정한 시간 뒤에 메소드 실행
        Invoke("showGameOverPanel", 1f);
    }

    void showGameOverPanel() {
        // gameOverPanel 활성화
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain() {
        // using UnityEngine.SceneManagement 추가 Scene 다시 로딩
        SceneManager.LoadScene("SampleScene");
    }
}
