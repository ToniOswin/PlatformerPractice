using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    Button[] levels;
    [SerializeField]
    Canvas levelsCan;
    [SerializeField]
    Canvas menuCan;
    [SerializeField]
    TextMeshProUGUI pointsTexts;
   
    private void Start()
    {
        int playerLevel = PlayerPrefs.GetInt("LevelsPassed", 0);
        int points = PlayerPrefs.GetInt("Points", 0);

        for(int i = 0; i < levels.Length; i++)
        {
            Button btn = levels[i];
            btn.interactable = playerLevel >= i+1;
        }

        pointsTexts.text = "Total Space Flowers = " + points;
    }

    public void StartLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void  OpenLevelSel()
    {
        levelsCan.gameObject.SetActive(true);
        menuCan.gameObject.SetActive(false);
    }
    public void CloseLevelSel()
    {
        levelsCan.gameObject.SetActive(false);
        menuCan.gameObject.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
