using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    
    [Space]
    [Header("Game Variables")]
    public float level1TimeLimit = 20f;
    public float level2TimeLimit = 30f;
    public float level3TimeLimit = 40f;
    public float timeLimit = 60f;
    public float currentTime;
    public bool isPaused = false;
    
    [Header("GamePlay Information")]
    public int playerHp = 10;
    public int score = 0;
    public int level = 1;
    public int coin = 0;
    
    private const int MAX_LEVEL = 3;
    
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }
    private void Start()
    {
        // GameStart();
    }

    [ContextMenu("Game Start")]
    public void GameStart()
    {
        ResetGame();
        UIManager.Instance.Init();
        StartCoroutine(GameRoutine());
    }
    public void LevelUp()
    {
        if (level >= MAX_LEVEL)
        {
            GameOver();
            return;
        }
        
        Debug.Log("Level Up");
        level++;
        if(level == 2)
        {
            timeLimit = level2TimeLimit;
        }else if(level == 3)
        {
            timeLimit = level3TimeLimit;
        }
        currentTime = timeLimit;
        isPaused = true;
        UIManager.Instance.ShowLevelUp(level);
        UIManager.Instance.Init();
        StartCoroutine(UIManager.Instance.ShowLevelUpRoutine(level));
        StartCoroutine(GameRoutine(5));
    }
    IEnumerator GameRoutine(float delay = 0f)
    {
        isPaused = false;
        yield return new WaitForSeconds(delay);
        // TODO: 메인 루틴 코드 추가
        StartCoroutine(ObjectSpawnManager.Instance.SpawnObjects());
        StartCoroutine(UIManager.Instance.TimerRoutine());
        
        yield return null;
    }

    public void ProcessingFoodCollision(FoodObject food)
    {
        // 정 업데이트
        switch (food.objectType)
        {
            case ObjectType.WellbeingFood:
                score += food.scoreValue;
                coin += food.coinValue;
                AudioManger.Instance.PlaySfx(AudioManger.Instance.goodFoodClip);
                PlayerController.Instance.StartSpriteSwitch(food.objectType);
                UIManager.Instance.ShowFloatingScoreUI(food.scoreValue);
                break;
            
            case ObjectType.JunkFood:
                AudioManger.Instance.PlaySfx(AudioManger.Instance.badFoodClip);
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
        playerHp = 5;
        score = 0;
        coin = 0;
        level = 1;
        if(level == 1)
        {
            timeLimit = level1TimeLimit;
        }else if(level == 2)
        {
            timeLimit = level2TimeLimit;
        }else if(level == 3)
        {
            timeLimit = level3TimeLimit;
        }
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
        Debug.Log("Game Over");
        isPaused = true;
        
        // 실행 중인 코루틴들 정지
        StopAllCoroutines();
        ObjectSpawnManager.Instance.StopAllCoroutines();
        UIManager.Instance.StopAllCoroutines();
        
        // TODO: 게임오버 UI 표시
        if(level >= MAX_LEVEL){
            if(playerHp > 0)
            {
                AudioManger.Instance.PlaySfx(AudioManger.Instance.gameClearClip);
                UIManager.Instance.ShowGameResult(0);
            }else{
                AudioManger.Instance.PlaySfx(AudioManger.Instance.gameOverClip);
                UIManager.Instance.ShowGameResult(1);
            }
        }else if(playerHp <= 0)
        {
            AudioManger.Instance.PlaySfx(AudioManger.Instance.gameOverClip);
            UIManager.Instance.ShowGameResult(1);
        }       
            
        
        
        // TODO: 재시작 버튼 표시
    }
}
