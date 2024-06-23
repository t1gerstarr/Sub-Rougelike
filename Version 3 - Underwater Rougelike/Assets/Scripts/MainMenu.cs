using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Timer timer;
    [SerializeField] private TMP_Text bestTimeText;
    [SerializeField] private TMP_Text currentTimeText;

    void Start()
    {
        // Find and reference the Timer component
        timer = FindObjectOfType<Timer>();

        if (timer == null)
        {
            Debug.LogError("Timer reference not found in the scene.");
        }
    }
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has Quit");
    }

    public void DeathScreen()
    {
        Debug.Log("DeathScreen called");

        if(timer != null)
        {
            Debug.Log("Updating UI");
            bestTimeText.text = "Longest Time Alive: " + timer.FormatTime(timer.bestTime);
            currentTimeText.text = "Time Alive: " + timer.FormatTime(timer.currentTime);
        }
        else
        {
            Debug.Log("Timer is null");
        }
    }
}
