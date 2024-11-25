using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    public Transform player; 
    public Image playerImage;
    
    public List<Sprite> sprites; // 스프라이트 리스트 (0번: 기본, 나머지: 변경용)
    public Coroutine currentCoroutine; // 현재 실행 중인 코루틴을 추적

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        /*QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 10;*/
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isPaused)
        {
            // TODO: 태블릿 Touch Input을 Canvas 상의 위치로 반영
            float horizontal = Input.GetAxis("Horizontal");

            Vector2 position = player.position;
            position.x = position.x + 500f * horizontal * Time.deltaTime;
            player.position = position;
        }
    }

    public void StartSpriteSwitch(int spriteIndex)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine); // 기존 코루틴 중단
        currentCoroutine = StartCoroutine(SwitchSprite(spriteIndex));
    }

    IEnumerator SwitchSprite(int spriteIndex)
    {
        if (spriteIndex >= sprites.Count || spriteIndex < 0)
        {
            Debug.LogError("Invalid sprite index");
            yield break;
        }

        // 해당 스프라이트로 변경
        playerImage.sprite = sprites[spriteIndex];
        yield return new WaitForSeconds(1f); // 1초 대기

        // 기본 스프라이트로 복구 (리스트의 0번 스프라이트)
        playerImage.sprite = sprites[0];
        currentCoroutine = null; // 코루틴 종료
    }
}
