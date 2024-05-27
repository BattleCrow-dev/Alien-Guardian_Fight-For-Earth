using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [Header("ScenesIDs")]
    [SerializeField] private int _loseSceneID;
    [SerializeField] private int _winSceneID;

    [Header("UI pause panel buttons")]
    [SerializeField] private Button _unpauseButton;
    [SerializeField] private Button _pauseMenuButton;

    [Header("UI texts")]
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private TMP_Text _diamondsText;
    [SerializeField] private TMP_Text _enemiesText;
    [SerializeField] private TMP_Text _hintText;
    [SerializeField] private TMP_Text _cutsceneText;

    [Header("UI panels")]
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private GameObject _hintPanel;
    [SerializeField] private GameObject _cutscenePanel;

    [Header("For cutscene")]
    [SerializeField] private bool _haveStartCutscene;
    [SerializeField] private CinemachineVirtualCamera _cineCamera;
    [SerializeField] private CutScene[] _frames;
    [SerializeField] private Button _cutSceneSkipButton;

    private bool isPaused = false;
    private bool isGameStarted = false;
    private Transform firstTarget;
    private Coroutine cutSceneCoroutine;

    private void Awake()
    {
        _hintPanel.SetActive(false);
        _cutscenePanel.SetActive(false);

        UpdateCoinsUI(0);
        UpdateDiamondsUI(0);
    }

    private void Start()
    {
        AudioController.Instance.StopMusic();
        if (_haveStartCutscene)
            StartCutscene();
        else {
            isGameStarted = true;
            AudioController.Instance.ChangeMusicToGame();
        }

        GlobalData.EraseData();
    }

    private void OnEnable()
    {
        _pauseMenuButton.onClick.AddListener(() =>
        {
            AudioController.Instance.PlayButtonSound();
            SceneManager.LoadScene(0);
        });
        _unpauseButton.onClick.AddListener(() =>
        {
            AudioController.Instance.PlayButtonSound();
            Pause();
        });
        _cutSceneSkipButton.onClick.AddListener(() =>
        {
            AudioController.Instance.PlayButtonSound();
            SkipCutscene();
        });
    }

    private void OnDisable()
    {
        _pauseMenuButton.onClick.RemoveAllListeners();
        _unpauseButton.onClick.RemoveAllListeners();
    }

    private void OnCutsceneEnded()
    {
        isGameStarted = true;
        _infoPanel.SetActive(true);
        _cineCamera.Follow = firstTarget;
        AudioController.Instance.StopSFX();
        AudioController.Instance.ChangeMusicToGame();
        _cutscenePanel.SetActive(false);
    }

    private void StartCutscene()
    {
        isGameStarted = false;
        _infoPanel.SetActive(false);
        firstTarget = _cineCamera.Follow;
        cutSceneCoroutine = StartCoroutine(nameof(Cutscene));
        _cutscenePanel.SetActive(true);
    }

    private void SkipCutscene()
    {
        StopCoroutine(cutSceneCoroutine);
        OnCutsceneEnded();
    }

    private IEnumerator Cutscene()
    {
        for (int i = 0; i < _frames.Length; i++)
        {
            _cineCamera.Follow = _frames[i].Target;
            _cutsceneText.text = _frames[i].Text;
            AudioController.Instance.PlayCustomSound(_frames[i].Voice);
            yield return new WaitForSecondsRealtime(_frames[i].Delay);
        }

        OnCutsceneEnded();
    }

    public void ShowLoseScene()
    {
        isGameStarted = false;
        GlobalData.FINAL_POINTS = GlobalData.COINS_GAINED_COUNT * GlobalConfigurationVariables.COIN_POINTS_VALUE
            + GlobalData.DIAMOND_GAINED_COUNT * GlobalConfigurationVariables.DIAMOND_POINTS_VALUE
            + GlobalData.ENEMY_KILLED_COUNT * GlobalConfigurationVariables.ENEMY_POINTS_VALUE;

        if (PlayerPrefs.GetInt(GlobalStringVariables.LEVEL_POINTS_SAVE_NAME) < GlobalData.FINAL_POINTS)
            PlayerPrefs.SetInt(GlobalStringVariables.LEVEL_POINTS_SAVE_NAME, GlobalData.FINAL_POINTS);

        AudioController.Instance.PlayGameOverSound();
        SceneManager.LoadScene(_loseSceneID);
    }

    public void ShowWinScene()
    {
        isGameStarted = false;
        GlobalData.FINAL_POINTS = GlobalData.COINS_GAINED_COUNT * GlobalConfigurationVariables.COIN_POINTS_VALUE 
            + GlobalData.DIAMOND_GAINED_COUNT * GlobalConfigurationVariables.DIAMOND_POINTS_VALUE
            + GlobalData.ENEMY_KILLED_COUNT * GlobalConfigurationVariables.ENEMY_POINTS_VALUE;

        if(PlayerPrefs.GetInt(GlobalStringVariables.LEVEL_POINTS_SAVE_NAME) < GlobalData.FINAL_POINTS)
            PlayerPrefs.SetInt(GlobalStringVariables.LEVEL_POINTS_SAVE_NAME, GlobalData.FINAL_POINTS);

        AudioController.Instance.PlayWinSound();
        SceneManager.LoadScene(_winSceneID);
    }
    public void ShowHint(bool isOpen, string text)
    {
        _hintText.text = text;
        _hintPanel.SetActive(isOpen);
    }
    public void Pause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        _pausePanel.SetActive(isPaused);
    }
    public void UpdateCoinsUI(int count)
    {
        _coinsText.text = $"{count}";
    }
    public void UpdateDiamondsUI(int count)
    {
        _diamondsText.text = $"{count}";
    }
    public void UpdateEnemiesUI(int count)
    {
        _enemiesText.text = $"{count}";
    }
    public bool IsGameStarted()
    {
        return isGameStarted;
    }
    public bool IsPaused()
    {
        return isPaused;
    }
}
