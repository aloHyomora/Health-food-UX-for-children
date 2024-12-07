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
    public int playerHp = 10;
    public int score = 0;
    public int level = 1;
    public int coin = 0;
    
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }
    private void Start()
    {
        GameStart();
    }

    [ContextMenu("Game Start")]
    public void GameStart()
    {
        ResetGame();
        UIManager.Instance.Init();
        StartCoroutine(GameRoutine());
    }
    IEnumerator GameRoutine()
    {
        // TODO: 메인 루틴 코드 추가
        StartCoroutine(ObjectSpawnManager.Instance.SpawnObjects());
        StartCoroutine(UIManager.Instance.TimerRoutine());
        
        yield return null;
    }

    public void ProcessingFoodCollision(FoodObject food)
    {
        // 정�� 업데이트
        switch (food.objectType)
        {
            case ObjectType.WellbeingFood:
                score += food.scoreValue;
                coin += food.coinValue;

                PlayerController.Instance.StartSpriteSwitch(food.objectType);
                UIManager.Instance.ShowFloatingScoreUI(food.scoreValue);
                break;
            
            case ObjectType.JunkFood:
                playerHp--;
                PlayerController.Instance.StartSpriteSwitch(food.objectType);
                // HP가 0이 되면 게임 오버
                if (playerHp <= 0)
                {
                    GameOver();
                }
                break;
        }
        
        UIManager.Instance.UpdateText();
    }

    public void ResetGame()
    {
        // 게임 상태 초기화
        playerHp = 10;
        score = 0;
        coin = 0;
        currentTime = timeLimit;
        isPaused = false;
        
        // UI 초기화
        UIManager.Instance.Init();
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

    public void GameOver()
    {
        isPaused = true;
        
        // 실행 중인 코루틴들 정지
        StopAllCoroutines();
        ObjectSpawnManager.Instance.StopAllCoroutines();
        UIManager.Instance.StopAllCoroutines();
        
        // TODO: 게임오버 UI 표시
        // TODO: 재시작 버튼 표시
    }
}
