using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int keyCount;
    public float MaxSpeed;

    [SerializeField] private int keyWinAmount;

    [SerializeField] private GameObject gameMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
