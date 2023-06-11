using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    
    //적이 속도 변수
    public float speed;

    //거리 관련 변수
    public float limitDis;
    private float dirX;
    private float dirY;
    private float dis;

    private Animator anim;

    //스크립트 변수
    private Enemy enemyScript;
    private GameManager gameManager;
    private Attack attackScript;

    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        enemyScript = this.gameObject.GetComponent<Enemy>();
        attackScript = this.gameObject.GetComponent<Attack>();

        GameObject playerObject = GameObject.FindWithTag("Player");

        GameObject gameManagerObject = GameObject.FindWithTag("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    void Update()
    {
        //거리 계산
        dis = Vector3.Distance(transform.position, player.position);
        dirX = player.position.x - transform.position.x;
        dirY = player.position.y - transform.position.y;

        //일정 범위내에 플레이어가 들어왔을 시 따라가는 함수 실행
        if (dis <= limitDis)
        {
            Move();
        }
        else
        {
            //가만히 있는 애니메이션 실행
            anim.SetBool("isTraceRight",false);
            anim.SetBool("isTraceLeft",false);
        }
    }

    void Move()
    {   
        //방향에 따라 다른 애니메이션 실행
        if(dirX <= 0)
        {
            //왼쪽으로 움직이는 애니메이션 실행
            anim.SetBool("isTraceRight",false);
            anim.SetBool("isTraceLeft",true);
        }else
        {
            //오른쪽으로 움직이는 애니메이션 실행
            anim.SetBool("isTraceLeft",false);
            anim.SetBool("isTraceRight",true);
        }

        //적이 플레이어쪽으로 움직이도록 설정
        transform.Translate(new Vector2(dirX, dirY) * speed * Time.deltaTime);
    }
}