using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveDirection; //수평 1, 수직 -1

    private GameObject mainCamera;
    private Vector3 destination;

    private bool enterDoor = false;

    private float playerDirX=0;
    private float playerDirY=0;

    private void Start()
    {
        mainCamera = Camera.main.gameObject;
    }

    private void Update()
    {
        //카메라 이동
        if (enterDoor)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, destination, 0.08f);
        }
        if (mainCamera.transform.position == destination) enterDoor = false;
    }

    //private void FixedUpdate()
    //{
    //    //카메라 이동 방향 설정 (카메라 좌표와 문 좌표 비교)
    //    if (mainCamera.transform.position.x < transform.position.x) playerDirX = 1;
    //    else playerDirX = -1;

    //    if (mainCamera.transform.position.y < transform.position.y) playerDirY = 1;
    //    else playerDirY = -1;
    //}

    //문과 캐릭터가 닿았을 때 카메라의 목적지 설정
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (moveDirection == 1) //수평으로만 이동 가능한 문
            {
                if (mainCamera.transform.position.x < transform.position.x) playerDirX = 1;
                else playerDirX = -1;
                destination = new Vector3(mainCamera.transform.position.x + 17.7f * playerDirX, mainCamera.transform.position.y, mainCamera.transform.position.z);
            }
            else if (moveDirection == -1) //수직으로만 이동 가능한 문
            {
                if (mainCamera.transform.position.y < transform.position.y) playerDirY = 1;
                else playerDirY = -1;
                destination = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + 9.0f * playerDirY, mainCamera.transform.position.z);
            }

            enterDoor = true;
        }
    }
}
