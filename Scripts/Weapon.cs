using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Start() - 게임 객체를 비활성화 했다가 다시 활성화 하는 경우 호출
// update() - 게임 객체를 무한으로 호출
public class Weapon : MonoBehaviour
{
    // unity에서 직접 값을 바꿀수 있게 하는 방법
    [SerializeField]
    private float moveSpeed = 10;
    public float damage = 1f;
    void Start()
    {
        // gameObject가 1초후에 사라짐
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
}
