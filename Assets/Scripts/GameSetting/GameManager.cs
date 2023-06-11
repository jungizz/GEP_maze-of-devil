using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Image gameOverPanel;

    public AudioSource DungeonSound;
    public AudioSource gameoverSound;

    //게임 오버시 실행할 함수
    public void GameOver()
    {
        gameoverSound.Play();
        DungeonSound.Stop();
        gameOverPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    //게임 시작
    public void StartGame()
    {
        SceneManager.LoadScene("SadVillage");
    }

    //게임 재시작
    public void RePlayGame()
    {
        SceneManager.LoadScene("Dungeon");
        gameOverPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    //타이틀 화면
    public void TitleScene()
    {
        SceneManager.LoadScene("TItle");
        Time.timeScale = 1;
    }
}
