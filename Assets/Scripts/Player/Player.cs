using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject mapBoundary;
    public GameObject bulletObj;

    public float HP;
    public bool getStaff;

    public float maxShotDelay;
    public float curShotDelay;

    public float playerSpeed;
    public float bulletSpeed;

    private float checkMoveX; //방향에 따라 총알이 날라가도록 변수 설정
    private float checkMoveY;

    private Rigidbody2D playerRb;
    private Animator playerAnim;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        HP = 100;
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            Fire();
        }
    }
    
    private void FixedUpdate()
    {
        //플레이어 움직임
        playerRb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * playerSpeed * Time.deltaTime;

        //플레이어 이동 애니메이션
        playerAnim.SetFloat("moveX", playerRb.velocity.x);
        playerAnim.SetFloat("moveY", playerRb.velocity.y);

        //방향에 일치하는 Idle 애니메이션 적용을 위해 lastMoveX/Y 변수에 저장
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            playerAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            playerAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            checkMoveX = Input.GetAxisRaw("Horizontal");
            checkMoveY = Input.GetAxisRaw("Vertical");

        }
    }

    void Fire()
    {
        if(Input.GetKeyDown(KeyCode.Space) && getStaff)
        {
            Rigidbody2D playerRb = this.GetComponent<Rigidbody2D>();
            GameObject bullet = Instantiate(bulletObj, transform.position, transform.rotation);
            Rigidbody2D rb2d = bullet.GetComponent<Rigidbody2D>();
            rb2d.AddForce(new Vector2(checkMoveX,checkMoveY) * bulletSpeed, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("HPItem"))
        {
            Destroy(collision.gameObject);
            HP += 10;
        }
        if(collision.gameObject.CompareTag("Staff"))
        {
            Destroy(collision.gameObject);
            getStaff = true;
        }
    }
}
