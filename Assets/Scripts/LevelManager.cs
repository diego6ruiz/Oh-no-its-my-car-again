using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject death;
    public GameObject win;
    public GameObject crosshair;

    public Text tags;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf==false)
        {
            activatePause();
        } else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == true)
        {
            desactivateMenu();
        }

    }
    public void ContinueButton()
    {
        desactivateMenu();

    }
    public void activatePause()
    {
        pauseMenu.SetActive(true);
        crosshair.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0.0f;
    }
    public void activateDeath()
    {
        death.SetActive(true);
        crosshair.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0.0f;
    }

    public void activateWin()
    {
        win.SetActive(true);
        crosshair.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0.0f;
    }
    public void desactivateMenu()
    {
        pauseMenu.SetActive(false);
        crosshair.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1.0f;
    }

    public void MainMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    }
}
