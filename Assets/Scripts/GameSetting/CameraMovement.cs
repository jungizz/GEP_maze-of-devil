using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float moveDirection; //���� 1, ���� -1

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
        //ī�޶� �̵�
        if (enterDoor)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, destination, 0.08f);
        }
        if (mainCamera.transform.position == destination) enterDoor = false;
    }

    //private void FixedUpdate()
    //{
    //    //ī�޶� �̵� ���� ���� (ī�޶� ��ǥ�� �� ��ǥ ��)
    //    if (mainCamera.transform.position.x < transform.position.x) playerDirX = 1;
    //    else playerDirX = -1;

    //    if (mainCamera.transform.position.y < transform.position.y) playerDirY = 1;
    //    else playerDirY = -1;
    //}

    //���� ĳ���Ͱ� ����� �� ī�޶��� ������ ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (moveDirection == 1) //�������θ� �̵� ������ ��
            {
                if (mainCamera.transform.position.x < transform.position.x) playerDirX = 1;
                else playerDirX = -1;
                destination = new Vector3(mainCamera.transform.position.x + 17.7f * playerDirX, mainCamera.transform.position.y, mainCamera.transform.position.z);
            }
            else if (moveDirection == -1) //�������θ� �̵� ������ ��
            {
                if (mainCamera.transform.position.y < transform.position.y) playerDirY = 1;
                else playerDirY = -1;
                destination = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + 9.0f * playerDirY, mainCamera.transform.position.z);
            }

            enterDoor = true;
        }
    }
}
