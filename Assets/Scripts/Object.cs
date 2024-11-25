using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position.y = position.y - 500f * Time.deltaTime;
        transform.position = position;
    }
    
    // TODO: 맵 밖으로 나갔을 때 삭제 처리

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"플레이어: {transform.name} 획득!");
            // TODO: Food 종류에 따른 이벤트(점수 득실점, 코인 획득, 체력 증감)
            
            
            Destroy(gameObject);
        }
        else if (other.CompareTag("Floor"))
        {
            Debug.Log("바닥 충돌");
            
            
            Destroy(gameObject);
        }
    }
    
}
