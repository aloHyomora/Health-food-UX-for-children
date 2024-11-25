using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    
    [Space]
    [Header("Game Variables")]
    [SerializeField] float timeLimit = 60f;
    public float currentTime;
    [SerializeField] int playerhp = 5;
    [SerializeField] int score = 0;
    
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    private void Start()
    {
        StartCoroutine(GameRoutine());
    }

    IEnumerator GameRoutine()
    {
        // TODO: 메인 루틴 코드 추가
        StartCoroutine(ObjectSpawnManager.Instance.SpawnObjects());
        // StartCoroutine(UIManager.Instance.TimerRoutine());
        
        yield return null;
    }
    
}
