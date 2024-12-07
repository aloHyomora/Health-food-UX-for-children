using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using DG.Tweening;
public class FoodObject : MonoBehaviour
{
    public int scoreValue;
    public int coinValue;
    public ObjectType objectType;
    

    [Header("Rotation & Movement")]
    public float rotationSpeed = 4f;
    public float moveSpeed = 500f;
    private void Awake()
    {
        transform.DOLocalRotate(new Vector3(0, 0, 360), rotationSpeed
        , RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental);
    }


    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isPaused)
        {
            Vector2 position = transform.position;
            position.y = position.y - moveSpeed * Time.deltaTime;
            transform.position = position;
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // TODO: Food 종류에 따른 이벤트(점수 득실점, 코인 획득, 체력 증감)
            GameManager.Instance.ProcessingFoodCollision(this);
            
            Destroy(gameObject);
        }
        else if (other.CompareTag("Floor"))
        {
            
            Destroy(gameObject);
        }
    }
}
public enum ObjectType
    {
        JunkFood,
        WellbeingFood
    }