using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Transform canvasTransform;
    public Slider timeSlider;
    public Image[] levelImages;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI playerHpText;

    public GameObject floatingScorePrefab;

    [Header("Page")]
    public GameObject page1;
    public GameObject page2;
    public Image[] countDownImages;
    public Image[] levelUpImages;
    public Image backgroundImage;
    public Image[] gameResultImages;
    public GameObject gameOverPanel;
    private void Awake()
    {
        if(Instance == null) Instance = this;

        ShowPage(1);
    }
    public void Init()
    {
        GameManager.Instance.currentTime = GameManager.Instance.timeLimit;
        timeSlider.maxValue = GameManager.Instance.timeLimit;
        timeSlider.value = GameManager.Instance.timeLimit;

        gameOverPanel.SetActive(false);

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

        GameManager.Instance.GameOver();
        if(GameManager.Instance.playerHp > 0)
        {
            GameManager.Instance.LevelUp();
        }
    }
    public void ShowLevelUp(int level)
    {
        if(level > levelImages.Length) return;
        HideLevelUp();
        var image = levelImages[level - 1];
        image.gameObject.SetActive(true);
    }
    public void ShowFloatingImage(Image image){
        image.gameObject.SetActive(true);
        image.transform.localScale = Vector3.zero;
        image.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }
    public IEnumerator ShowLevelUpRoutine(int level)
    {
        HideLevelUpImage();
        GameManager.Instance.Pause();
        backgroundImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        if(level == 2)
        {
            ShowFloatingImage(levelUpImages[1]);
            yield return new WaitForSeconds(2f);
            HideLevelUpImage(); 

            AudioManger.Instance.PlaySfx(AudioManger.Instance.levelUpClip);
            ShowFloatingImage(levelUpImages[0]);
            yield return new WaitForSeconds(2f);
        }
        else if(level == 3)
        {
            ShowFloatingImage(levelUpImages[2]);
            yield return new WaitForSeconds(2f);
            HideLevelUpImage(); 

            AudioManger.Instance.PlaySfx(AudioManger.Instance.levelUpClip);
            ShowFloatingImage(levelUpImages[0]);
            yield return new WaitForSeconds(2f);
        }
        HideLevelUpImage();
        backgroundImage.gameObject.SetActive(false);
        GameManager.Instance.Play();
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
        Vector3 spawnPos = playerPos + Vector3.up * 100f; // 플레이어 위치보다 100유닛 위에서 생성
        GameObject ui = Instantiate(floatingScorePrefab, spawnPos, Quaternion.identity, canvasTransform);
        TextMeshProUGUI textMesh = ui.GetComponent<TextMeshProUGUI>();
        textMesh.text = $"+{score}P";

        ui.transform.DOMoveY(500f,floatingTime);
        textMesh.DOFade(0.5f, floatingTime).OnComplete(() =>
        {
            Destroy(ui);
        });
    }

    public void ShowPage(int page)
    {
        page1.SetActive(page == 1);
        page2.SetActive(page == 2);
    }

    public void ShowPage2()
    {
        StartCoroutine(ShowPage2Routine());
    }
    IEnumerator ShowPage2Routine()
    {
        yield return new WaitForSeconds(1f);
        ShowPage(2);

        HideCountDown();
        yield return new WaitForSeconds(5f);
        ShowPage(-1);

        ShowCountDown(0);
        yield return new WaitForSeconds(1f);
        ShowCountDown(1);
        yield return new WaitForSeconds(1f);
        ShowCountDown(2);
        yield return new WaitForSeconds(1f);
        ShowCountDown(3);
        yield return new WaitForSeconds(0.5f);     
        HideCountDown();
        GameManager.Instance.GameStart();
    }
    public void ShowCountDown(int count)
    {
        if(count != 3)
        {
            AudioManger.Instance.PlaySfx(AudioManger.Instance.countDownClip);
        }else{
            AudioManger.Instance.PlaySfx(AudioManger.Instance.startClip);
        }
        HideCountDown();
        var image = countDownImages[count];
        image.gameObject.SetActive(true);
        image.transform.localScale = Vector3.zero;
        image.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);
    }
    public void HideCountDown()
    {
        foreach (var image in countDownImages)
        {
            image.gameObject.SetActive(false);
        }
    }
    public void HideLevelUp()
    {
        foreach (var image in levelImages)
        {
            image.gameObject.SetActive(false);
        }
    }
    public void HideLevelUpImage(){
        foreach (var image in levelUpImages)
        {
            image.gameObject.SetActive(false);
        }
    }

    public void GoToHome()
    {
        // 현재 씬을 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        // 또는 특정 씬으로 이동하려면:
        // SceneManager.LoadScene("MainMenuScene");
    }
    public void ShowGameResult(int result)
    {
        if(result > gameResultImages.Length) return;

        gameOverPanel.SetActive(true);  
        foreach (var image1 in gameResultImages)
        {
            image1.gameObject.SetActive(false);
        }
        var image = gameResultImages[result];
        image.gameObject.SetActive(true);
    }
}




