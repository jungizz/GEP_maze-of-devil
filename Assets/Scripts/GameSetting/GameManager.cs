using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Image gameOverPanel;

    //게임 오버시 실행할 함수
    public void GameOver()
    {
        gameOverPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("SadVillage");
    }

    public void RePlayGame()
    {
        //처음 위치에서 리스폰
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = new Vector3(0.08f, -1.95f, 0);
        player.GetComponent<Player>().HP = 100;
        Camera.main.gameObject.transform.position = new Vector3(0, 0.1f, -10f);
        gameOverPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void TitleScene()
    {
        SceneManager.LoadScene("TItle");
        Time.timeScale = 1;
    }
}
