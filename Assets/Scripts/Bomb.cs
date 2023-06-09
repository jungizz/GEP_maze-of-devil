using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<Player>();
        bombAnim = transform.GetComponent<Animator>();

        if (BombName == "P") //�÷��̾ ��� ��ź
        {
            //�÷��̾ ���ϴ� ������ ��ǥ�������� ����
            targetPos = player.transform.position + new Vector3(playerScript.checkMoveX, playerScript.checkMoveY, 0) * 10;
        }
        else if (BombName == "E") //���� ��� ��ź
        {
            //�÷��̾� ��ġ�� ��ǥ �������� ����
            targetPos = player.transform.position;
        }
        //3�� �ȿ� �浹 ������ ���� ��� ���� �ִϸ��̼� Ȱ��ȭ
        Invoke("Explosion", 3f);
    }

    private void Update()
    {
        //���� �ִϸ��̼� Ȱ��ȭ �Ǳ� ������ ��ź �߻�
        if (!bombAnim.GetBool("isExplosion"))
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, 0.005f);
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //���� �� �Ѿ��̰� �÷��̾�� �ε�����
        if (BombName == "E" && other.gameObject.CompareTag("Player"))
        {
            playerScript.HP -= 5; //�÷��̾� ü�� ����
            Explosion(); //��ź �ִϸ��̼� Ȱ��ȭ
        }
        //�÷��̾ �� �Ѿ��̰� ���� �ε�����
        else if (BombName == "P" && other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().HP -= 10; //�� ü�� ����
            Explosion();
        }
    }

    private void Explosion()
    {
        bombAnim.SetBool("isExplosion", true);
        Invoke("Destroy", 0.35f);
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
}
