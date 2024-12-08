using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Rendering;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawnManager : MonoBehaviour
{
    public static ObjectSpawnManager Instance;
    
    private RectTransform _canvasRectTransform;
    private Vector2 _canvasSize;
    
    public Transform spawnTransform;
    public GameObject[] objectPrefabs;
    
    private void Awake()
    {
        if(Instance == null) Instance = this;
        _canvasRectTransform = spawnTransform.root.GetComponent<RectTransform>();    
        _canvasSize = _canvasRectTransform.rect.size;
        Debug.Log(_canvasSize);
    }
    
    // seconds 동안 랜덤하게 오브젝트를 생성함.
    public IEnumerator SpawnObjects()
    {
        while (GameManager.Instance.currentTime >= 0)
        {
            yield return new WaitWhile(() => GameManager.Instance.isPaused);
            
            Vector3 spawnOffset = new Vector3(Random.Range(-_canvasSize.x / 2, _canvasSize.x / 2), 100, 0);
            Instantiate(objectPrefabs[Random.Range(0, objectPrefabs.Length)], 
                spawnTransform.position + spawnOffset, 
                quaternion.identity,
                spawnTransform);
            
            float waitTime = GameManager.Instance.level switch
            {
                1 => Random.Range(1f, 1.4f),
                2 => Random.Range(0.7f, 0.9f),
                3 => Random.Range(0.5f, 0.7f),
                _ => 1f
            };
            
            yield return new WaitForSeconds(waitTime);
        }
    }
}
