using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeKey : MonoBehaviour
{
    public GameObject keyItem;

    private void Update()
    {
        //�� ������ ���� �� �׾��� �� Ű ������ ����
        if (this.transform.childCount < 1)
        {
            keyItem.SetActive(true);
            gameObject.GetComponent<MakeKey>().enabled = false;
        }

    }
}
