using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Object : MonoBehaviour
{
    public int scoreValue;
    public int coinValue;
    public ObjectType objectType;
    public enum ObjectType
    {
        JunkFood,
        WellbeingFood
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isPaused)
        {
            Vector2 position = transform.position;
            position.y = position.y - 500f * Time.deltaTime;
            transform.position = position;
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"플레이어: {transform.name} 획득!");
            // TODO: Food 종류에 따른 이벤트(점수 득실점, 코인 획득, 체력 증감)
            GameManager.Instance.ProcessingFoodCollision(this);
            
            Destroy(gameObject);
        }
        else if (other.CompareTag("Floor"))
        {
            Debug.Log("바닥 충돌");
            
            
            Destroy(gameObject);
        }
    }
}
