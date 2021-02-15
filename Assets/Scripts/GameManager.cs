using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;
    [SerializeField]
    Canvas GameOverCan;
    [SerializeField]
    Canvas endGameCan;
    [SerializeField]
    Canvas startLevelCan;

    private void Start()
    {
        StartCoroutine(startGame());
        GameOverCan.gameObject.SetActive(false) ;
        ResumeGame();
    }
    public void setMaxHealth( int health)
    {
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    public void SetHealth(int health)
    {
        healthBar.value = health;
    }

    public void Die()
    {
        PauseGame();
        GameOverCan.gameObject.SetActive(true);
    }


     public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    public void Restart()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().name));
    }

    public void NextLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
    public void EndGame()
    {
        PauseGame();
        endGameCan.gameObject.SetActive(true);
    }

    IEnumerator startGame()
    {
        PauseGame();
        startLevelCan.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        startLevelCan.gameObject.SetActive(false);
        ResumeGame();
    }
}
