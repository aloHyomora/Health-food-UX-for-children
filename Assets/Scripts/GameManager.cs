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

    public void ProcessingFoodCollision(Object food)
    {
        // 정보 업데이트
        switch (food.objectType)
        {
            case Object.ObjectType.WellbeingFood:
                score += food.scoreValue;
                coin += food.coinValue;

                PlayerController.Instance.StartSpriteSwitch(1);
                UIManager.Instance.ShowFloatingScoreUI(food.scoreValue);
                break;
            
            case Object.ObjectType.JunkFood:
                playerHp--;
                PlayerController.Instance.StartSpriteSwitch(2);
                break;
        }
        
        UIManager.Instance.UpdateText();
    }

    public void ResetGame()
    {
        // play 정보 리셋
        playerHp = 10;
        score = 0;
        coin = 0;
        
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
