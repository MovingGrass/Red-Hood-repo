using UnityEngine;
using UnityEngine.UI;

public class LoadMainMenuButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("Button component not found on this GameObject.");
        }
    }

    private void Start()
    {
        if (button != null)
        {
            button.onClick.AddListener(LoadMainMenu);
        }
    }

    private void LoadMainMenu()
    {
        if (GameSceneManager.Instance != null)
        {
            GameSceneManager.Instance.LoadMainMenu();
        }
        else
        {
            Debug.LogError("GameSceneManager instance is not available.");
        }
    }
}
