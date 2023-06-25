using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float minY = -7f;

    // Start is called before the first frame update
    void Start()
    {
        Jump();
    }
    void Jump() {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        // 실수값으로 랜덤
        float radomJumpforce = Random.Range(4f, 8f);
        // Vector2 - x,y 값만 표시
        Vector2 jumpVelocity = Vector2.up * radomJumpforce;
        // x값도 랜덤으로 출력
        jumpVelocity.x = Random.Range(-2f, 2f);
        // rigidBody.AddForce(힘 크기, 힘 모드)
        rigidBody.AddForce(jumpVelocity, ForceMode2D.Impulse);
    }

    void Update()
    {
        if(transform.position.y < minY) {
            Destroy(gameObject);
        }
    }
}
