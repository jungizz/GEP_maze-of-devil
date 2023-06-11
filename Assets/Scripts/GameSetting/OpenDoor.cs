using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OpenDoor : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject Key; //Ű �̹��� UI

    //����, ���� �� Ÿ��
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


    //ī�޶� �̵� ���� ����
    public float moveDirection; //���� 1, ���� -1
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
        //�� ������ ���� �� �׾��� �� ���� ������ ���
        if (openByEnemy &&  Enemy.transform.childCount < 1) 
        {
            Open();
            openByEnemy = false;
        }

        if (openByKey && Key.activeSelf)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }

    }

    private void FixedUpdate()
    {
        //ī�޶� �̵�
        if (enterDoor)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, destination, 0.1f);
        }
        if (mainCamera.transform.position == destination) enterDoor = false;
    }

    //�� ����
    public void Open()
    {
        doorSound.Play();

        //�ݶ��̴��� Trigger�� Ȱ��ȭ�ؼ� ��� �����ϵ���
        GetComponent<BoxCollider2D>().isTrigger = true;

        //���� �� Ÿ���� ���� �� Ÿ�Ϸ� �ٲٱ�
        tilemap.SwapTile(closeDoorTile1, openDoorTile1);
        tilemap.SwapTile(closeDoorTile2, openDoorTile2);
        tilemap.SwapTile(closeDoorTile3, openDoorTile3);
        tilemap.SwapTile(closeDoorTile4, openDoorTile4);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //Ű�� ������ ���� ���� �� ����
            if (openByKey && Key.activeSelf)
            {
                Open();
                Key.SetActive(false);
                openByKey = false;
            }

            if (moveDirection == 1) //�������θ� �̵� ������ ��
            {
                //ī�޶� ������ ���ʿ� ������ 1, �ƴϸ� -1
                if (mainCamera.transform.position.x < transform.GetComponent<BoxCollider2D>().bounds.center.x) playerDirX = 1; 
                else playerDirX = -1;

                //ī�޶��� ������ ����
                destination = new Vector3(mainCamera.transform.position.x + 17.7f * playerDirX, mainCamera.transform.position.y, mainCamera.transform.position.z);
            }
            else if (moveDirection == -1) //�������θ� �̵� ������ ��
            {
                //ī�޶� ������ �Ʒ��� ������ 1, �ƴϸ� -1
                if (mainCamera.transform.position.y < transform.GetComponent<BoxCollider2D>().bounds.center.y) playerDirY = 1;
                else playerDirY = -1;

                //ī�޶��� ������ ����
                destination = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + 9.0f * playerDirY, mainCamera.transform.position.z);
            }
            enterDoor = true;
        }
    }

}
