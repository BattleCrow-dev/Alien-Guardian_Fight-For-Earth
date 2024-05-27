using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _SFXSource;

    [Header("Tracks")]
    [SerializeField] private AudioClip _menuBackMusic;
    [SerializeField] private AudioClip _gameBackMusic;

    [Header("Clips")]
    [SerializeField] private AudioClip _buttonSound;
    [SerializeField] private AudioClip _shootSound;
    [SerializeField] private AudioClip _reloadSound;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _gameOverSound;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _coinSound;
    [SerializeField] private AudioClip _diamondSound;
    [SerializeField] private AudioClip _heartSound;
    [SerializeField] private AudioClip _heroDamageSound;
    [SerializeField] private AudioClip _enemyDamageSound;
    [SerializeField] private AudioClip _alertSound;
    [SerializeField] private AudioClip _boxSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void ChangeMusicToMenu()
    {
        _musicSource.Stop();
        _musicSource.clip = _menuBackMusic;
        _musicSource.Play();
    }
    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void StopSFX()
    {
        _SFXSource.Stop();
    }

    public void ChangeMusicToGame()
    {
        _musicSource.Stop();
        _musicSource.clip = _gameBackMusic;
        _musicSource.Play();
    }

    public void SetVolumes(float musicVolume, float SFXVolume)
    {
        _musicSource.volume = musicVolume;
        _SFXSource.volume = SFXVolume;
    }

    public void PlayButtonSound()
    {
        _SFXSource.PlayOneShot(_buttonSound);
    }
    public void PlayShotSound()
    {
        _SFXSource.PlayOneShot(_shootSound);
    }
    public void PlayReloadSound()
    {
        _SFXSource.PlayOneShot(_reloadSound);
    }
    public void PlayJumpSound()
    {
        _SFXSource.PlayOneShot(_jumpSound);
    }
    public void PlayGameOverSound()
    {
        _SFXSource.PlayOneShot(_gameOverSound);
    }
    public void PlayWinSound()
    {
        _SFXSource.PlayOneShot(_winSound);
    }
    public void PlayCoinSound()
    {
        _SFXSource.PlayOneShot(_coinSound);
    }
    public void PlayDiamondSound()
    {
        _SFXSource.PlayOneShot(_diamondSound);
    }
    public void PlayHeartSound()
    {
        _SFXSource.PlayOneShot(_heartSound);
    }
    public void PlayHeroDamageSound()
    {
        _SFXSource.PlayOneShot(_heroDamageSound);
    }
    public void PlayEnemyDamageSound()
    {
        _SFXSource.PlayOneShot(_enemyDamageSound);
    }
    public void PlayAlertSound()
    {
        _SFXSource.PlayOneShot(_alertSound);
    }
    public void PlayBoxSound()
    {
        _SFXSource.PlayOneShot(_boxSound);
    }
    public void PlayCustomSound(AudioClip clip)
    {
        _SFXSource.PlayOneShot(clip);
    }
}
