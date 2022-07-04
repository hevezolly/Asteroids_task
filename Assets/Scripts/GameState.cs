using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField]
    private ScriptableValue<bool> showMenu;
    [SerializeField]
    private ScriptableValue<bool> canContinueGame;
    [SerializeField]
    private ScriptableValue<int> playerHealth;
    [SerializeField]
    private Menu menu;

    private void Awake()
    {
        Time.timeScale = 1;
        if (showMenu.Value)
        {
            menu.DisplayMenu();
            showMenu.Value = false;
        }
    }

    private void OnEnable()
    {
        playerHealth.ValueChangeEvent.AddListener(OnHealthChange);
    }

    private void OnDisable()
    {
        playerHealth.ValueChangeEvent.RemoveListener(OnHealthChange);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.Pause();
        }
    }

    private void OnHealthChange(int health)
    {
        if (health != 0)
            return;

        canContinueGame.Value = false;
        menu.GameOver();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

#if UNITY_EDITOR
    private void OnApplicationQuit()
    {
        showMenu.Value = true;
        canContinueGame.Value = false;
    }
#endif
}
