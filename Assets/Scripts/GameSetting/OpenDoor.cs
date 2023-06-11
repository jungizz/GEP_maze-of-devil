using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OpenDoor : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Key; //키 이미지 UI

    //닫힌, 열린 문 타일
    public Tilemap tilemap;
    public TileBase closeDoorTile1;
    public TileBase closeDoorTile2;
    public TileBase closeDoorTile3;
    public TileBase closeDoorTile4;
    public TileBase openDoorTile1;
    public TileBase openDoorTile2;
    public TileBase openDoorTile3;
    public TileBase openDoorTile4;

    private bool openByEnemy = false;
    private bool openByKey = false;


    //카메라 이동 관련 변수
    public float moveDirection; //수평 1, 수직 -1
    private GameObject mainCamera;
    private Vector3 destination;
    private bool enterDoor = false;
    private float playerDirX = 0;
    private float playerDirY = 0;

    private AudioSource doorSound;

    private void Start()
    {
        if (Enemy != null) openByEnemy = true;
        if (Key != null) openByKey = true;

        mainCamera = Camera.main.gameObject;
        destination = mainCamera.transform.position;

        doorSound = GameObject.Find("OpenDoor").GetComponent<AudioSource>();
    }

    private void Update()
    {
        //각 던전에 적이 다 죽었을 때 문이 열리는 경우
        if (openByEnemy &&  Enemy.transform.childCount < 1) 
        {
            Open();
            openByEnemy = false;
        }
    }

    private void FixedUpdate()
    {
        //카메라 이동
        if (enterDoor)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, destination, 0.1f);
        }
        if (mainCamera.transform.position == destination) enterDoor = false;
    }

    //문 열기
    public void Open()
    {
        doorSound.Play();

        //콜라이더의 Trigger를 활성화해서 통과 가능하도록
        GetComponent<BoxCollider2D>().isTrigger = true;

        //닫힌 문 타일을 열린 문 타일로 바꾸기
        tilemap.SwapTile(closeDoorTile1, openDoorTile1);
        tilemap.SwapTile(closeDoorTile2, openDoorTile2);
        tilemap.SwapTile(closeDoorTile3, openDoorTile3);
        tilemap.SwapTile(closeDoorTile4, openDoorTile4);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //키를 가지고 있을 때만 문 열기
            if (openByKey  && Key.activeSelf) 
            {
                Open();
                Key.SetActive(false);
                openByKey = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (moveDirection == 1) //수평으로만 이동 가능한 문
            {
                //카메라가 문보다 왼쪽에 있으면 1, 아니면 -1
                if (mainCamera.transform.position.x < transform.GetComponent<BoxCollider2D>().bounds.center.x) playerDirX = 1; 
                else playerDirX = -1;

                //카메라의 목적지 설정
                destination = new Vector3(mainCamera.transform.position.x + 17.7f * playerDirX, mainCamera.transform.position.y, mainCamera.transform.position.z);
            }
            else if (moveDirection == -1) //수직으로만 이동 가능한 문
            {
                //카메라가 문보다 아래에 있으면 1, 아니면 -1
                if (mainCamera.transform.position.y < transform.GetComponent<BoxCollider2D>().bounds.center.y) playerDirY = 1;
                else playerDirY = -1;

                //카메라의 목적지 설정
                destination = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + 9.0f * playerDirY, mainCamera.transform.position.z);
            }
            enterDoor = true;
        }
    }

}
