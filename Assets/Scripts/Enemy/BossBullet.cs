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

        //총알 움직임
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            playerScript.DeceasePlayerHP(10); //플레이어 체력 감소
            if (playerScript.HP <= 0)
            {
                gameManager.GameOver(); //플레이어 라이프가 0이 되었을 때 게임오버 만들어줌
            }
            Destroy(gameObject);
        }
    }
}
