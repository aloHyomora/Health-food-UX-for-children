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

    [SerializeField] private float moveSpeed = 500f;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    void Update()
    {
        if(!GameManager.Instance.isPaused)
        {            
            // TODO: 태블릿 Touch Input을 Canvas 상의 위치로 반영
            if(Input.touchCount > 0){
                Touch touch = Input.GetTouch(0);
                HandleTouch(touch.position);
            }

            // 에디터나 PC에서 테스트를 위한 마우스 입력 지원
            #if UNITY_EDITOR || UNITY_STANDALONE
            else if(Input.mousePresent && Input.GetMouseButton(0))
            {
                HandleTouch(Input.mousePosition);
            }
            #endif
        }
    }

    private void HandleTouch(Vector2 inputPosition)
    {
        // 화면의 아래쪽 절반에서만 입력 처리
        if (inputPosition.y > Screen.height * 0.5f) return;
        
        // UI용 RectTransform 가져오기
        RectTransform rectTransform = player as RectTransform;
        
        // 스크린 좌표를 UI 좌표로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform.parent as RectTransform, // Canvas RectTransform
            inputPosition,
            null, // UI가 Screen Space - Overlay 모드일 경우 null
            out Vector2 localPoint
        );

        // 현재 위치에서 x값만 부드럽게 변경
        Vector2 currentPos = rectTransform.anchoredPosition;
        currentPos.x = Mathf.Lerp(currentPos.x, localPoint.x, moveSpeed * Time.deltaTime);
        
        // 위치 업데이트
        rectTransform.anchoredPosition = currentPos;
    }

    public void StartSpriteSwitch(ObjectType type)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine); // 기존 코루틴 중단
        currentCoroutine = StartCoroutine(SwitchSprite(type));
    }

    IEnumerator SwitchSprite(ObjectType type)
    {
        if(type == ObjectType.WellbeingFood)
        {            
            // 해당 스프라이트로 변경
            playerImage.sprite = sprites[1];
            yield return new WaitForSeconds(0.3f);
            playerImage.sprite = sprites[2];
            yield return new WaitForSeconds(0.3f); 

            // 기본 스프라이트로 복구 (리스트의 0번 스프라이트)
            playerImage.sprite = sprites[0];
            currentCoroutine = null; // 코루틴 종료 
        }
        else if(type == ObjectType.JunkFood)
        {
            // 해당 스프라이트로 변경
            playerImage.sprite = sprites[3];
            yield return new WaitForSeconds(0.3f);
            playerImage.sprite = sprites[4];
            yield return new WaitForSeconds(0.3f); 

            // 기본 스프라이트로 복구 (리스트의 0번 스프라이트)
            playerImage.sprite = sprites[0];
            currentCoroutine = null; // 코루틴 종료
        }
    }
}
