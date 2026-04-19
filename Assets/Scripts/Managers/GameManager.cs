using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;
// Game Manager
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("State")]
    public bool isLevelComplete = false;
    public bool isGameOver = false;

    [Header("UI Settings")]
    public TextMeshProUGUI orbText; 
    public GameObject levelCompletePanel; 
    public GameObject gameOverPanel;

    [Header("Door Settings")]
    public GameObject doorToOpen; 
    
    private int totalOrbs;
    private int collectedOrbs = 0;

    private void Awake()
    {
        if (Instance == null){
            Instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        isLevelComplete = false;
        isGameOver = false;
        Time.timeScale = 1f; 
        
        if (levelCompletePanel != null) {
            levelCompletePanel.SetActive(false);
        }
        if (gameOverPanel != null){
            gameOverPanel.SetActive(false);
        }

        totalOrbs = GameObject.FindGameObjectsWithTag("Orb").Length;
        UpdateUI();
    }

    public void AddOrb()
    {
        collectedOrbs++;
        UpdateUI();

        if (collectedOrbs >= totalOrbs) {
            OpenDoor();
        }
    }

    private void UpdateUI()
    {
        if (orbText != null) {
            orbText.text = "Orbs: " + collectedOrbs + " / " + totalOrbs;
        }
    }

    private void OpenDoor()
    {
        if (doorToOpen != null) {
            doorToOpen.SetActive(false); 
        }
    }

    public void LevelComplete()
    {
        if (isGameOver){
            return; 
        }

        isLevelComplete = true;
        if (levelCompletePanel != null) {
            levelCompletePanel.SetActive(true);
        }
        Time.timeScale = 0f; 
    }

    public void GameOver()
    {
        if (isLevelComplete){
            return; 
        }

        isGameOver = true;
        if (gameOverPanel != null) {
            gameOverPanel.SetActive(true);
        }
        Time.timeScale = 0f; 
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit(); 
    }
}