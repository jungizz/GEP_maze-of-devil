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
        transform.position = Camera.main.WorldToScreenPoint(Player.transform.position + Vector3.up);
        PlayerHP();

        if(playerScript.HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void PlayerHP()
    {
        float HP = playerScript.HP;
        HPbar.fillAmount = HP / 200;
        HPText.text = string.Format("{0}/200", HP);
    }
}
