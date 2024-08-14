using UnityEngine;

public class PointManager : MonoBehaviour
{
    public static PointManager Instance { get; private set; }

    private int currentPoints = 0;

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
        }
    }

    public void AddPoint()
    {
        currentPoints++;
    }

    public int GetCurrentPoints()
    {
        return currentPoints;
    }

    public void ResetPoints()
    {
        currentPoints = 0;
    }
}