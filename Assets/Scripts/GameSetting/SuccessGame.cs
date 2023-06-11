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
        if (!isSuccess && Enemy.transform.childCount < 1)
        {
            successSound.Play();
            dungeonSound.Stop();
            Invoke("changeScene", 3f);
            isSuccess = true;
        }
    }

    void changeScene()
    {
        SceneManager.LoadScene("HappyVillage");
    }
}
