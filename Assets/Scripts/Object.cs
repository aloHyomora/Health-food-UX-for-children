using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position.y = position.y - 500f * Time.deltaTime;
        transform.position = position;
    }
    
    // TODO: 맵 밖으로 나갔을 때 삭제 처리
}
