using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager Instance { get; private set; }

    [Header("Scene Names")]
    public string mainMenuScene = "MainMenu";
    public string gameOverScene = "GameOver";
    public string winScene = "Win";
    public string level1Scene = "Level1";
    public string level2Scene = "Level2";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == mainMenuScene)
        {
            // Ensure PauseManager is disabled when returning to main menu
            if (PauseManager.Instance != null)
            {
                PauseManager.Instance.gameObject.SetActive(false);
            }
        }
        else if (scene.name == level1Scene || scene.name == level2Scene)
        {
            // Ensure PauseManager is enabled for game levels
            if (PauseManager.Instance != null)
            {
                PauseManager.Instance.gameObject.SetActive(true);
                PauseManager.Instance.ResetPauseState();
            }
        }

        // Always ensure correct time scale
        Time.timeScale = 1f;
    }

    public void LoadMainMenu()
    {
         SceneManager.LoadScene(mainMenuScene);
    }

    

    public void LoadGameOver()
    {
        SceneManager.LoadScene(gameOverScene);
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene(winScene);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(level1Scene);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(level2Scene);
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("No next level available. Loading win scene.");
            LoadWinScene();
        }
    }

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}