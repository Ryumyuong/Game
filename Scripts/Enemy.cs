using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;

    [SerializeField]
    private float moveSpeed = 10f;

    private float minY = -7f;
    [SerializeField]
    private float hp = 1f;

    public void SetMoveSpeed(float moveSpeed) {
        this.moveSpeed = moveSpeed;
    }
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if(transform.position.y < minY) {
            Destroy(gameObject);
        }
    }

    // is Trigger가 체크 되있으면 사용
    private void OnTriggerEnter2D(Collider2D other) {
        // 태그가 Weapon인것과 충돌 할 때
        if(other.gameObject.tag == "Weapon") {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;
            if(hp <= 0) {
                // Enemy 삭제
                Destroy(gameObject);
                // Instantiate - 새로운 객체 만들기
                Instantiate(coin, transform.position, Quaternion.identity);
            }
            // 충돌한 미사일 삭제
            Destroy(other.gameObject);
        }
    }

    // is Trigger가 체크 되어 있지 않으면 사용
    // private void OnCollisionEnter2D(Collision2D other) {
        
    // }
}
