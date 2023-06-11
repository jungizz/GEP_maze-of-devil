using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeKey : MonoBehaviour
{
    public GameObject keyItem;

    private void Update()
    {
        //각 던전에 적이 다 죽었을 때 키 아이템 생성
        if (this.transform.childCount < 1)
        {
            keyItem.SetActive(true);
            gameObject.GetComponent<MakeKey>().enabled = false;
        }

    }
}
