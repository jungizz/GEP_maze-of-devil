using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public GameObject bulletObj;

    public float HP;
    public float playerSpeed;
    private bool dieCheck;
    [HideInInspector] public bool getStaff;
    [HideInInspector] public bool getBomb;

    //키를 얻었을 때 화면에 띄울 이미지
    public GameObject keyImage;

    //일반공격 관련 변수
    private Transform pos;
    public Transform posRight;
    public Transform posLeft;
    public Transform posUp;
    public Transform posDown;

    public Vector2 boxSize;

    //방향에 따라 총알이 날라가도록 변수 설정
    [HideInInspector] public float checkMoveX;
    [HideInInspector] public float checkMoveY;

    private Rigidbody2D playerRb;
    private Animator playerAnim;
    private Attack attackscript;
    private GameManager gameManager;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        pos = posRight;
        attackscript = GetComponent<Attack>();
        GameObject gameManagerObject = GameObject.FindWithTag("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 2)
        {
            if(Input.GetKeyDown(KeyCode.Z) && !dieCheck)
            {
                if (getStaff) attackscript.basicShoot();
                else if(getBomb) attackscript.BombAttack();
            }
                
            if(Input.GetKeyDown(KeyCode.Space) && !dieCheck)
                basicAttack();
        }

        //플레이어 라이프가 0이 되었을 때 게임오버 만들어줌
        if (HP <= 0)
        {
            playerAnim.SetBool("isDie", true);
            dieCheck = true;
            StartCoroutine(Die());
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

    void basicAttack()
    {
        if(checkMoveX < 0)
        {
            pos = posLeft;
        }else if(checkMoveX > 0)
        {
            pos = posRight;
        }else if(checkMoveY < 0)
        {
            pos = posDown;
        }else if(checkMoveY > 0)
        {
            pos = posUp;
        }

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if(collider.tag == "Enemy")
            {
                collider.GetComponent<Enemy>().HP -= 5;
            }
        }
        playerAnim.SetBool("isAttack", true);
        Invoke("stopAttack", 0.3f);
    }

    void stopAttack()
    {
        playerAnim.SetBool("isAttack", false);
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.blue;
    //     Gizmos.DrawWireCube(pos.position,boxSize);
    // }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(0.6f);
        gameManager.GameOver();
        dieCheck = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("HPItem"))
        {
            Destroy(collision.gameObject);
            HP += 10;
        }
        if (collision.gameObject.CompareTag("KeyItem"))
        {
            Destroy(collision.gameObject);
            keyImage.SetActive(true);
        }

        if (collision.gameObject.CompareTag("Staff"))
        {
            Destroy(collision.gameObject);
            getStaff = true;
            getBomb = false;
        }
        if (collision.gameObject.CompareTag("BombItem"))
        {
            Destroy(collision.gameObject);
            getBomb = true;
            getStaff = false;
        }
    }
}
