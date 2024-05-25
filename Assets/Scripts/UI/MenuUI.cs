using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [Header("UI panels")]
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _settingsPanel;

    [Header("UI buttons")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _backButton;

    [Header("UI texts")]
    [SerializeField] private TMP_Text _pointsText;

    [Header("UI sliders")]
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _SFXSlider;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(() => 
        {
            AudioController.Instance.PlayButtonSound();
            SceneManager.LoadScene(1);
        });

        _settingsButton.onClick.AddListener(() => 
        {
            _mainPanel.SetActive(false);
            _settingsPanel.SetActive(true);
            AudioController.Instance.PlayButtonSound(); 
        });

        _backButton.onClick.AddListener(() =>
        {
            _settingsPanel.SetActive(false);
            _mainPanel.SetActive(true);
            AudioController.Instance.PlayButtonSound();
        });

        _musicSlider.onValueChanged.AddListener(delegate {
            PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
            AudioController.Instance.SetVolumes(_musicSlider.value, _SFXSlider.value);
        });

        _SFXSlider.onValueChanged.AddListener(delegate {
            PlayerPrefs.SetFloat("MusicVolume", _SFXSlider.value);
            AudioController.Instance.SetVolumes(_musicSlider.value, _SFXSlider.value);
        });
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveAllListeners();
        _musicSlider.onValueChanged.RemoveAllListeners();
        _SFXSlider.onValueChanged.RemoveAllListeners();
    }

    private void Awake()
    {
        Time.timeScale = 1f;

        if (!PlayerPrefs.HasKey(GlobalStringVariables.LEVEL_POINTS_SAVE_NAME))
            _pointsText.text = "null";
        else
            _pointsText.text = PlayerPrefs.GetInt(GlobalStringVariables.LEVEL_POINTS_SAVE_NAME).ToString();

        _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.25f);
        _SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
    }

    private void Start()
    {
        AudioController.Instance.SetVolumes(_musicSlider.value, _SFXSlider.value);
        AudioController.Instance.ChangeMusicToMenu();
    }
}
