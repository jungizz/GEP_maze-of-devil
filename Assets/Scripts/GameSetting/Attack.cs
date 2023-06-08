using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject bulletObjE;
    public GameObject bulletObjP;
    public GameObject player;

    public float maxShotDelay;
    public float curShotDelay;

    //적과 플레이어를 구분하기 위한 변수
    public string GameObjectName;

    //플레이어 스크립트 변수
    private Player playerScript;

    void Start()
    {
        playerScript = player.GetComponent<Player>();
    }

    void Update()
    {
        if(GameObjectName == "ShootE" )
        {
            basicShoot();
            Reload();
        }
    }

    //총알 하나 날라가는 공격패턴
    public void basicShoot()
    {
        if(GameObjectName == "ShootE")
        {
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
        }else if(GameObjectName == "P")
        {
            Rigidbody2D playerRb = this.GetComponent<Rigidbody2D>();
            GameObject bullet = Instantiate(bulletObjP, transform.position, transform.rotation);
            Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
            rb2d.AddForce(new Vector2(playerScript.checkMoveX,playerScript.checkMoveY) * 10, ForceMode2D.Impulse);
        }
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
}
