using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalUI : MonoBehaviour
{
    [Header("UI buttons")]
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _restartButton;

    [Header("UI texts")]
    [SerializeField] private TMP_Text _pointsText;

    private void OnEnable()
    {
        AudioController.Instance.StopMusic();
        _menuButton.onClick.AddListener(() =>
        {
            AudioController.Instance.PlayButtonSound();
            SceneManager.LoadScene(0);
        });

        if (_restartButton)
            _restartButton.onClick.AddListener(() =>
            {
                AudioController.Instance.PlayButtonSound();
                SceneManager.LoadScene(1);
            });
    }
    private void OnDisable()
    {
        _menuButton.onClick.RemoveAllListeners();

        if (_restartButton)
            _restartButton.onClick.RemoveAllListeners();

        AudioController.Instance.StopSFX();
    }

    private void Awake()
    {
        _pointsText.text = GlobalData.FINAL_POINTS.ToString();
    }
}
