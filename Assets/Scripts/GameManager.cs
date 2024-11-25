using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    
    [Space]
    [Header("Game Variables")]
    public float timeLimit = 60f;
    public float currentTime;
    public bool isPaused = false;
    
    [Header("GamePlay Information")]
    public int playerHp = 5;
    public int score = 0;
    public int level = 1;
    public int coin = 0;
    
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    private void Start()
    {
        StartCoroutine(GameRoutine());
    }

    IEnumerator GameRoutine()
    {
        // TODO: 메인 루틴 코드 추가
        StartCoroutine(ObjectSpawnManager.Instance.SpawnObjects());
        StartCoroutine(UIManager.Instance.TimerRoutine());
        
        yield return null;
    }

    public void ProcessingFoodCollision(Object food)
    {
        // 정보 업데이트
        switch (food.objectType)
        {
            case Object.ObjectType.WellbeingFood:
                score += food.scoreValue;
                coin += food.coinValue;
                
                UIManager.Instance.ShowFloatingScoreUI(food.scoreValue);
                break;
            
            case Object.ObjectType.JunkFood:
                playerHp--;
                break;
        }
        
        UIManager.Instance.UpdateText();
    }


    public void Pause()
    {
        isPaused = true;
    }
    public void Play()
    {
        isPaused = false;
    }
}
