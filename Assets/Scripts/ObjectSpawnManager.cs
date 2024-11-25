using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ObjectSpawnManager : MonoBehaviour
{
    public static ObjectSpawnManager Instance;
    
    public GameObject[] objectPrefabs;
    
    private void Awake()
    {
        if(Instance == null) Instance = this;
    }
    
    // seconds 동안 랜덤하게 오브젝트를 생성함.
    public IEnumerator SpawnObjects()
    {

        yield return null;
    }

}
