using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
   
    public float speed;
    public float limitDis;
    private float dirX;
    private float dirY;

    private Animator anim;

    private Enemy enemyScript;
    private Player playerScript;
    private GameManager gameManager;

    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        enemyScript = this.gameObject.GetComponent<Enemy>();

        GameObject playerObject = GameObject.FindWithTag("Player");
        playerScript = playerObject.GetComponent<Player>();

        GameObject gameManagerObject = GameObject.FindWithTag("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    void Update()
    {
        float dis = Vector3.Distance(transform.position, target.position);
        dirX = target.position.x - transform.position.x;
        dirY = target.position.y - transform.position.y;

        if (dis <= limitDis)
        {
            Move();
        }
        else
        {
            if(dirX <= 0)
            {
                anim.SetBool("isIdleLeft", true);
                anim.SetBool("isTraceRight",false);
                anim.SetBool("isTraceLeft",false);
            }else
            {
                anim.SetBool("isIdleLeft", false);
                anim.SetBool("isTraceRight",false);
                anim.SetBool("isTraceLeft",false);
            }
            return;
        }
    }

    void Move()
    {
        if(dirX <= 0)
        {
            anim.SetBool("isIdleLeft", false);
            anim.SetBool("isTraceRight",false);
            anim.SetBool("isTraceLeft",true);
        }else
        {
            anim.SetBool("isIdleLeft", false);
            anim.SetBool("isTraceLeft",false);
            anim.SetBool("isTraceRight",true);
        }

        transform.Translate(new Vector2(dirX, dirY) * speed * Time.deltaTime);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            playerScript.HP -= 1;
            if(playerScript.HP <= 0) {
                gameManager.GameOver(); //플레이어 라이프가 0이 되었을 때 게임오버 만들어줌
            }
        }
    }
}