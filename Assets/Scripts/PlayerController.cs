using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        /*QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 10;*/
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: 태블릿 Touch Input을 Canvas 상의 위치로 반영
        // 임시로 변경
        float horizontal = Input.GetAxis("Horizontal");
        // float vertical = Input.GetAxis("Vertical");

        Vector2 position = player.position;
        position.x = position.x + 500f * horizontal * Time.deltaTime;
        player.position = position;
    }
}
