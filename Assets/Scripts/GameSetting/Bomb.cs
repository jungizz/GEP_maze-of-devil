using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public string BombName;

    private GameObject player;
    private Vector3 targetPos;
    private Player playerScript;

    private Animator bombAnim;

    private AudioSource bombSound;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        bombAnim = transform.GetComponent<Animator>();

        bombSound = GameObject.Find("Explosion").GetComponent<AudioSource>();

        if (BombName == "P") //플레이어가 쏘는 폭탄
        {
            //플레이어가 향하는 방향을 목표지점으로 설정
            targetPos = player.transform.position + new Vector3(playerScript.checkMoveX, playerScript.checkMoveY, 0) * 10;
        }
        else if (BombName == "E") //적이 쏘는 폭탄
        {
            //플레이어 위치를 목표 지점으로 설정
            targetPos = player.transform.position;
        }
        //3초 안에 충돌 감지가 없는 경우 폭발 애니메이션 활성화
        Invoke("Explosion", 3f);
    }

    private void Update()
    {
        //폭발 애니메이션 활성화 되기 전까지 폭탄 발사
        if (!bombAnim.GetBool("isExplosion"))
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.005f);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //적이 쏜 총알이고 플레이어와 부딪히면
        if (BombName == "E" && other.gameObject.CompareTag("Player"))
        {
            playerScript.DeceasePlayerHP(5); //플레이어 체력 감소
            Explosion(); //폭탄 애니메이션 활성화
        }
        //플레이어가 쏜 총알이고 적과 부딪히면
        else if (BombName == "P" && other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().HP -= 10; //적 체력 감소
            Explosion();
        }
    }

    private void Explosion()
    {
        if (BombName == "P") bombSound.Play();
        bombAnim.SetBool("isExplosion", true);
        Invoke("Destroy", 0.35f);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
