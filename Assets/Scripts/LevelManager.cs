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
    public TMPro.TextMeshProUGUI timerText;
    public GameObject timesUpPanel; 

    public float timer = 90f; 
    private bool timerRunning = true;
    

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        UpdateTimerText();

    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning && timer > 0)
        {
            timer -= Time.deltaTime; // Decrease the timer by the time difference between frames
            UpdateTimerText();

            if (timer <= 0)
            {
                timerRunning = false;
                timer = 0; // Ensure timer doesn't go below 0
                OnTimerEnd(); // Handle the timer reaching 0
            }
        }


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

    public void StopTimer()
    {
        timerRunning = false;
    }

    public void StartTimer()
    {
        timerRunning = true;
    }

    public void ResetTimer()
    {
        timer = 0f;
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            timerText.text = $"Time: {timer:F2}"; // Display the timer value with 2 decimal places
        }
    }

    private void OnTimerEnd()
    {
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (timesUpPanel != null)
        {
            timesUpPanel.SetActive(true);
        }

        StartCoroutine(WaitAndReloadScene());
    }

    public void ReloadCurrentScene()
    {
        // Get the currently active scene and retrieve its name
        string sceneName = SceneManager.GetActiveScene().name;

        // Reload the scene by name
        SceneManager.LoadScene(sceneName);

        // If you've stopped the game using Time.timeScale, make sure to reset it
        Time.timeScale = 1.0f;

        // Optionally, re-enable the cursor state if you've modified it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    IEnumerator WaitAndReloadScene()
    {
        yield return new WaitForSecondsRealtime(3);
        ReloadCurrentScene();
    }

}
