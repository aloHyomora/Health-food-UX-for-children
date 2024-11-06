using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WebcamManager : MonoBehaviour
{
    public RawImage rawImage;         // 웹캠 이미지를 표시할 UI 요소
    public Material waveMaterial;     // 물결 효과 셰이더가 적용된 머티리얼

    private WebCamTexture webCamTexture;
    private float nextWaveTime;       // 다음 물결 효과 시간이 될 시간
    private float minInterval = 5f;   // 최소 간격 (5초)
    private float maxInterval = 10f;  // 최대 간격 (10초)
    private bool isWaving = false;    // 물결 효과가 활성화 중인지 확인하는 플래그

    void Start()
    {
        // 사용 가능한 웹캠 장치 목록 가져오기
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > 0)
        {
            // 첫 번째 웹캠 장치 사용
            webCamTexture = new WebCamTexture(devices[0].name);
            rawImage.texture = webCamTexture;
            webCamTexture.Play();

            // 웹캠 텍스처를 물결 효과 머티리얼에 적용
            waveMaterial.mainTexture = webCamTexture;
            rawImage.material = waveMaterial;
        }
        else
        {
            Debug.LogError("사용 가능한 웹캠이 없습니다.");
        }

        // 초기 다음 물결 시간 설정
        SetNextWaveTime();
    }

    void Update()
    {
        // 시간이 지나서 물결 효과를 활성화할 때가 되었을 때
        if (Time.time >= nextWaveTime)
        {
            StartWaveEffect();
            SetNextWaveTime();
        }

        // 물결 효과가 진행 중이라면 시간에 따라 업데이트
        if (isWaving)
        {
            waveMaterial.SetFloat("_Time", Time.time);  // 시간에 따라 물결 이동
        }
    }

    void StartWaveEffect()
    {
        // 주파수와 진폭 설정하여 물결 효과 적용
        waveMaterial.SetFloat("_Frequency", Random.Range(0.5f, 2.0f));   // 주파수 랜덤 변경
        waveMaterial.SetFloat("_Amplitude", Random.Range(0.05f, 0.1f));  // 진폭 랜덤 변경
        isWaving = true;

        // 5초 후에 부드럽게 물결 효과를 종료하도록 설정
        Invoke("SmoothStopWaveEffect", 5f);
    }

    void SmoothStopWaveEffect()
    {
        // 진폭을 서서히 0으로 줄여 부드럽게 효과가 끝나도록 설정
        DOTween.To(() => waveMaterial.GetFloat("_Amplitude"), x => waveMaterial.SetFloat("_Amplitude", x), 0f, 1f)
               .OnComplete(() => isWaving = false);  // 부드럽게 감소 후 isWaving을 false로 설정
    }

    void SetNextWaveTime()
    {
        // 5~10초 사이의 간격으로 다음 물결 시간 설정
        nextWaveTime = Time.time + Random.Range(minInterval, maxInterval);
    }

    void OnDestroy()
    {
        if (webCamTexture != null)
        {
            webCamTexture.Stop();
        }
    }
}
