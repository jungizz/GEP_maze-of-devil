using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public GameObject Player;

    public Image HPbar;
    public Text HPText;

    private Player playerScript;

    private void Start()
    {
        playerScript = Player.GetComponent<Player>();
    }

    private void Update()
    {
        //�÷��̾� �Ӹ� ���� ü�¹ٰ� ����ٴϱ�
        transform.position = Camera.main.WorldToScreenPoint(Player.transform.position + Vector3.up);
        PlayerHP();

        if(playerScript.HP <= 0)
            Destroy(this.gameObject);

        if(playerScript.HP > 300)
            playerScript.HP = 300;
    }

    private void PlayerHP()
    {
        //HP �����̴��� ü�� ǥ��
        float HP = playerScript.HP;
        HPbar.fillAmount = HP / 300;
        HPText.text = string.Format("{0}/300", HP);
    }
}
