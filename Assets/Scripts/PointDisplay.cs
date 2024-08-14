using UnityEngine;
using TMPro;

public class PointDisplay : MonoBehaviour
{
    public TextMeshProUGUI pointText;

    private void Update()
    {
        if (PointManager.Instance != null)
        {
            pointText.text = PointManager.Instance.GetCurrentPoints().ToString();
        }
    }
}