using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed;

    private Player playerScript;
    private GameManager gameManager;

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        playerScript = playerObject.GetComponent<Player>();

        GameObject gameManagerObject = GameObject.FindWithTag("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();

        //�Ѿ� ������
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            playerScript.DeceasePlayerHP(10); //�÷��̾� ü�� ����
            if (playerScript.HP <= 0)
            {
                gameManager.GameOver(); //�÷��̾� �������� 0�� �Ǿ��� �� ���ӿ��� �������
            }
            Destroy(gameObject);
        }
    }
}
