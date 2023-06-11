using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public static float speed;
    public float HP;
    private float dirX;
    private float dirY;

    private Animator anim;

    public GameObject[] items;
    private AudioSource deathSound;

    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        deathSound = GameObject.Find("EnemyDeath").GetComponent<AudioSource>();
    }

    public void Update()
    {

        dirX = target.position.x - transform.position.x;
        dirY = target.position.y - transform.position.y;

        //플레이어를 바라보게 만들기 위한 애니메이션 제어
        if(dirX <= 0)
        {
            anim.SetBool("isIdleLeft", true);
        }else
        {
            anim.SetBool("isIdleLeft", false);
        }


        if(HP <= 0)
        {
            deathSound.Play();
            Destroy(this.gameObject);
            DropItem();
        }
    }

    //적이 죽었을때 실행되는 함수
    public void DropItem()
    {   
        //랜덤으로 떨어지게 설정
        int ran = Random.Range(0,3);
        if(ran >= 2)
            return;
        if(ran == 1 && items[1].gameObject == null)
            return;
        Instantiate(items[ran], transform.position, transform.rotation);
    }
}