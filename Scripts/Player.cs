using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 직접 값 입력가능
    [SerializeField]
    private float moveSpeed;

    // GameObject 설정
    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;

    // unity의 shootTransform을 바로 사용 가능
    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.05f;
    private float lastShotTime = 0f;
    void Update()
    {
        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // Vector3 moveTo = new Vector3(horizontalInput, 0f, 0f);
        // 위아래도 움직이게 하기
        // float verticalInput = Input.GetAxisRaw("Vertical");
        // Vector3 moveTo = new Vector3(horizontalInput, verticalInput, 0f);
        // Time.deltaTime - 사양이 다른 컴퓨터도 같은 속도로 움직이도록 할 수 있음
        // transform.position += moveTo * moveSpeed * Time.deltaTime;

        // 다른 방법으로 좌우로 움직이기
        // Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        // if(Input.GetKey(KeyCode.LeftArrow)) {
        //     transform.position -= moveTo;
        // } else if(Input.GetKey(KeyCode.RightArrow)) {
        //     transform.position += moveTo;
        // }

        //마우스 위치의 x,y,z값을 출력
        // Input.mousePosition;

        // 마우스 위치의 x,y,z값을 출력(중앙의 x,y좌표 값이 0임)
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // y와 x는 플레이어의 값을 그대로 가져오고 x값만 마우스가 향하는 곳으로 값이 변경
        // 마우스에 따라 위치를 변경하기 때문에 충돌영역을 무시한다.
        // transform.position = new Vector3(mousePos.x, transform.position.y, transform.position.z);
        // mousePos.x 값의 최솟값은 -2.35f, 최댓값은 2.35f로 지정하여 캐릭터가 화면 밖으로 나가지 않게 지정
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        // mousePos.x -> toX로 변경
        transform.position = new Vector3(toX, transform.position.y, transform.position.z);
        Shoot();
    }
    // GameObject를 쏘는 방법
    // shootTransform.position - 시작 위치
    // Quaternion.identity - 회전없이 일자로 날라가기
    void Shoot() {
        // Time.time - 게임이 시작된 이후로 현재까지 흐른 시간
        if(Time.time - lastShotTime > shootInterval) {
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss") {
            Debug.Log("Game Over");
            Destroy(gameObject);
        } else if(other.gameObject.tag == "Coin") {
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    public void Upgrade() {
        weaponIndex += 1;
        if(weaponIndex >= weapons.Length) {
            weaponIndex = weapons.Length - 1;
        }
    }
}
