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
        SceneManager.LoadScene("Dungeon");
        Time.timeScale = 1;
    }

    public void TitleScene()
    {
        SceneManager.LoadScene("TItle");
        Time.timeScale = 1;
    }
}
