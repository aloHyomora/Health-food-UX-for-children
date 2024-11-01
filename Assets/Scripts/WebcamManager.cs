using UnityEngine;
using UnityEngine.UI;

public class WebcamWaveEffect : MonoBehaviour
{
    public RawImage rawImage;         // 웹캠 이미지를 표시할 UI 요소
    public Material waveMaterial;     // 물결 효과 셰이더가 적용된 머티리얼

    private WebCamTexture webCamTexture;

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
        	rawImage.texture = webCamTexture;      // UI 요소에 웹캠 텍스처 연결
    	    rawImage.material = waveMaterial;      // 물결 효과가 있는 머티리얼 적용
        }
        else
        {
            Debug.LogError("사용 가능한 웹캠이 없습니다.");
        }
    }

    void OnDestroy()
    {
        if (webCamTexture != null)
        {
            webCamTexture.Stop();
        }
    }

    void Update()
    {
        // 랜덤 물결 효과를 위해 주기적으로 주파수와 진폭을 업데이트
        //waveMaterial.SetFloat("_Frequency", Random.Range(0.5f, 2.0f));   // 주파수 랜덤 변경
        //waveMaterial.SetFloat("_Amplitude", Random.Range(0.02f, 0.1f));  // 진폭 랜덤 변경
        //waveMaterial.SetFloat("_Time", Time.time);  // 시간에 따라 물결 이동
    }
}
