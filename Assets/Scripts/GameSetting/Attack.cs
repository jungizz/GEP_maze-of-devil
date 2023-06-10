using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject bulletObjE;
    public GameObject bulletObjP;
    public GameObject bombObjP;
    public GameObject staff;
    public GameObject machete;

    //MacheteE에서 필요한 변수
    public GameObject effectObj;
    private GameObject effect;
    private Animator effectAnim;
    private bool isHit; //공격 딜레이를 위한 변수
    
    public GameObject player;
    private Player playerScript;

    //총알 딜레이를 위한 변수
    public float maxShotDelay;
    public float curShotDelay;

    //거리측정때 사용하는 변수
    private float dis;
    private float dirX;
    private float dirY;

    //적과 플레이어를 구분하기 위한 변수
    public string gameObjectName;

    void Start()
    {
        playerScript = player.GetComponent<Player>();
        isHit = true;
    }

    void Update()
    {
        dis = Vector3.Distance(transform.position, player.transform.position);
        dirX = player.transform.position.x - transform.position.x;
        dirY = player.transform.position.y - transform.position.y;

        //오브젝트의 이름에 따라 실행
        if(gameObjectName == "ShootE")
        {
            basicShoot();
            reload();
        }
        if(gameObjectName == "MacheteE")
        {
            macheteAttack();
        }
        if (gameObjectName == "BombE")
        {
            BombAttack();
            reload();
        }
    }

    //창을 휘두르는 공격패턴
    void macheteAttack()
    {
        if(gameObjectName == "MacheteE")
        {   
            //플레이어의 방향에 따라 무기 위치&회전 값 바꾸기
            if(dirX <= 0)
            {
                machete.transform.position = transform.position + new Vector3(-0.4f,0,0);
                machete.transform.rotation = Quaternion.Euler(0,0,12);
            }else
            {
                machete.transform.position = transform.position + new Vector3(0.4f,0,0);
                machete.transform.rotation = Quaternion.Euler(0,0,-12);
            }

            //해당 범위내에 플레이어가 들어왔을 시 공격 실행
            if (dis <= 1)
            {
                if(isHit)
                {
                    isHit = false;
                    effect = Instantiate(effectObj, new Vector2(transform.position.x, (transform.position.y - 0.045f)),transform.rotation);
                    effectAnim = effect.GetComponent<Animator>();
                    effectAnim.SetBool("isEffect", true);   //공격이펙트 실행
                    playerScript.HP -= 10;
                    StartCoroutine(CoolTime());     //공격 딜레이를 위한 코루틴 함수 실행
                }
            }
        }
    }

    //총알 하나 날라가는 공격패턴
    public void basicShoot()
    {
        if(gameObjectName == "ShootE")
        {
            //플레이어의 방향에 따라 무기 위치&회전 값 바꾸기
            if(dirX <= 0)
            {
                staff.transform.position = transform.position + new Vector3(-0.4f,0,0);
                staff.transform.rotation = Quaternion.Euler(0,0,13);
            }else
            {
                staff.transform.position = transform.position + new Vector3(0.4f,0,0);
                staff.transform.rotation = Quaternion.Euler(0,0,-13);
            }

            //총알 딜레이
            if(curShotDelay < maxShotDelay)
            {
                return;
            }

            if(gameObject != null && player != null)
            {
                GameObject bullet = Instantiate(bulletObjE, transform.position, transform.rotation); //적 위치에 총알 생성
                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                Vector2 dirVec = player.transform.position - transform.position; //플레이어 위치 - Enemy 위치를 빼면 목표물로의 방향 값이 나옴
                rigid.AddForce(dirVec.normalized * 4, ForceMode2D.Impulse); //단위벡터로 만들어줌(normalized : 벡터가 단위 값(1)로 변환된 변수)
                                                                            //플레이어가 있던 위치로 총알 발사
            }

            curShotDelay = 0; //총알은 쏜 다음에는 딜레이 변수 0으로 초기화
        }
        else if(gameObjectName == "P")
        {
            //플레이어 위치에서 총알을 생성하여 플레이어가 바라보는 방향으로 날려줌
            Rigidbody2D playerRb = this.GetComponent<Rigidbody2D>();
            GameObject bullet = Instantiate(bulletObjP, transform.position, transform.rotation);
            Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
            rb2d.AddForce(new Vector2(playerScript.checkMoveX,playerScript.checkMoveY) * 10, ForceMode2D.Impulse);
        }
    }

    //폭탄을 던지는 공격패턴
    public void BombAttack()
    {
        if (gameObjectName == "BombE")
        {
            if (curShotDelay < maxShotDelay) return;
            GameObject bomb = Instantiate(bulletObjE, transform.position, transform.rotation);

            curShotDelay = 0;
        }
        else if (gameObjectName == "P")
        {
            GameObject bomb = Instantiate(bombObjP, transform.position, transform.rotation);
        }
    }

    //날리는 공격 딜레이를 위한 함수
    void reload()
    {
        curShotDelay += Time.deltaTime;
    }

    //일반 공격 딜레이를 위한 함수
    private IEnumerator CoolTime()
    {
        //대기 0.3초후 이펙트 오브젝트 제거
        yield return new WaitForSeconds(0.3f);
        Destroy(effect);
        //대기 3초후 공격 실행
        yield return new WaitForSeconds(3.0f);
        isHit = true;
    }

    //따라오는 적이 플레이어와 닿았을 때 플레이어 피 깎이게 만들기
    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(gameObjectName == "FollowE")
                playerScript.HP -= 1;
        }
    }
}
