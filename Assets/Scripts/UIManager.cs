using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Slider timeSlider;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI healthText;
    
    
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        GameManager.Instance.currentTime = GameManager.Instance.timeLimit;
        timeSlider.maxValue = GameManager.Instance.timeLimit;
        timeSlider.value = GameManager.Instance.timeLimit;
    }
    public IEnumerator TimerRoutine()
    {
        while (GameManager.Instance.currentTime > 0)
        {
            GameManager.Instance.currentTime -= Time.deltaTime;   // 남은 시간을 감소
            timeSlider.value = GameManager.Instance.currentTime;  // 슬라이더 값 업데이트
            yield return null;                                    // 다음 프레임까지 대기
        }

        // 제한 시간 종료 시 추가 동작
        Debug.Log("Time is up!");
    }
}
