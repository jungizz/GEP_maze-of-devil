using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessGame : MonoBehaviour
{
    public GameObject Enemy;
    private AudioSource successSound;
    private AudioSource dungeonSound;

    private bool isSuccess;

    private void Start()
    {
        successSound = GameObject.Find("GameSuccess").GetComponent<AudioSource>();
        dungeonSound = GameObject.Find("Dungeon").GetComponent<AudioSource>();
    }

    private void Update()
    {
        //보스 던전의 적을 다 죽인 경우
        if (!isSuccess && Enemy.transform.childCount < 1)
        {
            successSound.Play();
            dungeonSound.Stop();

            //3초 뒤 씬 이동
            Invoke("changeScene", 3f);
            isSuccess = true;
        }
    }

    void changeScene()
    {
        SceneManager.LoadScene("HappyVillage");
    }
}
