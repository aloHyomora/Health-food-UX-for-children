using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    public Transform player; 
    
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

    
}
