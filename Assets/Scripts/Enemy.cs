using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static float speed;
    public float HP;

    public GameObject[] items;

    public void DropItem()
    {
        int ran = Random.Range(0,5);
        if(ran >= 2)
            return;
        if(ran == 1 && items[1].gameObject == null)
            return;
        Instantiate(items[ran], transform.position, transform.rotation);
    }
}