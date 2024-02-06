using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public float speed = 2f; // 이동 속도 조절
    public float stopYPosition = 5f; // 멈추기를 원하는 Y 위치

    private bool shouldMove = false; // 이동 여부를 제어하는 플래그

    void Update()
    {
        if (shouldMove)
        {
            // 이미지를 천천히 위로 이동시키기
            transform.Translate(Vector3.up * speed * Time.deltaTime);

            // 특정 Y 위치에 도달하면 이동 중지
            if (transform.position.y >= stopYPosition)
            {
                shouldMove = false;
                // 여기에서 필요한 추가 작업 수행 가능
            }
        }
    }

    public void ToggleMovement()
    {
        shouldMove = !shouldMove;

        if (!shouldMove)
        {
            // 이동 중이 아니라면 원래 위치로 되돌아가기
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }
}
