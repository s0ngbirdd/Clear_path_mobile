using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthFillBar : MonoBehaviour
{
    // Serialize
    [SerializeField] private Image _fillBar;
    [SerializeField] private TextMeshProUGUI _unitsText;

    // Private
    private StatsController _statsController;

    private void Start()
    {
        _statsController = FindObjectOfType<StatsController>();
    }

    private void Update()
    {
        DisplayCurrentFill();
    }

    private void DisplayCurrentFill()
    {
        _fillBar.fillAmount = _statsController.PlayerHealth / 100.0f;
        _unitsText.text = _statsController.PlayerHealth.ToString();
    }
}
