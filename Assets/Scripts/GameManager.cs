using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int keyCount;
    public float MaxSpeed;
    public int keyWinAmount;

    [SerializeField] private TextMeshProUGUI keyAmountUI;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject gameOverMenu;

    [SerializeField]
    private GameObject tutorialMenu;
    


    public void GameOver()
    {
        Debug.Log("Game over");
        Time.timeScale = 0;
        gameOverMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        gameOverMenu.SetActive(false);
        gameMenu.SetActive(false);
        tutorialMenu.SetActive(false);
    }

    private void Start()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateKeyUI();
        PlayerEscaped();
    }

    private void PlayerEscaped()
    {
        if (keyCount == keyWinAmount)
        {
            Time.timeScale = 0;
            gameMenu.SetActive(true);
        }
    }

    private void UpdateKeyUI()
    {
        keyAmountUI.text = "Keys: " + keyCount + " / " + keyWinAmount;
    }

}
