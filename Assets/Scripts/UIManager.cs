using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Transform canvasTransform;
    public Slider timeSlider;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI playerHpText;

    public GameObject floatingScorePrefab;
    
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
            // isPause가 true이면 여기서 대기
            yield return new WaitWhile(() => GameManager.Instance.isPaused);
            
            GameManager.Instance.currentTime -= Time.deltaTime;   // 남은 시간을 감소
            timeSlider.value = GameManager.Instance.currentTime;  // 슬라이더 값 업데이트
            yield return null;                                    // 다음 프레임까지 대기
        }

        // 제한 시간 종료 시 추가 동작
        Debug.Log("Time is up!");
    }

    public void UpdateText()
    {
        scoreText.text = GameManager.Instance.score.ToString();
        coinText.text = GameManager.Instance.coin.ToString();
        playerHpText.text = GameManager.Instance.playerHp.ToString(); 
    }
    public void ShowFloatingScoreUI(int score)
    {
        Vector3 playerPos = PlayerController.Instance.player.position;

        float floatingTime = 0.5f; 
        GameObject ui = Instantiate(floatingScorePrefab, playerPos, Quaternion.identity, canvasTransform);
        ui.transform.DOMoveY(500f,floatingTime);
        ui.GetComponent<TextMeshProUGUI>().DOFade(0f, floatingTime).OnComplete(() =>
        {
            Destroy(ui);
        });
    }
}




