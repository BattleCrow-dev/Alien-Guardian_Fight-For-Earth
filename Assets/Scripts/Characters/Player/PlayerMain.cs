using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput), typeof(PlayerMovement))]
public class PlayerMain : MonoBehaviour, IDamageable
{
    [Header("Elements")]
    [SerializeField] private Slider _healthBar;
    [SerializeField] private TMP_Text _bulletText;
    [SerializeField] private Transform _shootPoint;

    [Header("Prefabs")]
    [SerializeField] private GameObject _bulletPrefab;

    private LevelUI levelUI;
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;

    private int maxBullets;
    private int curBullets;
    private float maxHealth;
    private float curHealth;
    private float damage;

    private void Awake()
    {
        levelUI = FindFirstObjectByType<LevelUI>();
        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();

        maxHealth = GlobalConfigurationVariables.PLAYER_MAX_HEALTH;
        maxBullets = GlobalConfigurationVariables.PLAYER_MAX_BULLETS;
        curHealth = maxHealth;
        curBullets = maxBullets;
        damage = GlobalConfigurationVariables.PLAYER_DAMAGE;

        _healthBar.value = curHealth / maxHealth;
    }

    private void Update()
    {
        if(levelUI.IsGameStarted() && !levelUI.IsPaused())
            CheckInputs();

        levelUI.UpdateEnemiesUI(GlobalData.ENEMY_KILLED_COUNT);
    }

    private void CheckInputs()
    {
        playerMovement.SetHorizontalInput(playerInput.HorizontalInput);
        playerMovement.SetIsJump(playerInput.IsJump);

        if (playerInput.IsShoot)
            Shoot();

        if (playerInput.IsPause)
            Pause();

        if (playerInput.IsReloading && curBullets != maxBullets)
            StartCoroutine(Reloading());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(GlobalStringVariables.DEATH_TAG))
            OnDeath();

        if (other.CompareTag(GlobalStringVariables.FINISH_TAG))
            OnFinish();

        if (other.CompareTag(GlobalStringVariables.HINT_TAG))
            OnHint(true, other.GetComponent<HintTable>().GetHintText());

        if (other.CompareTag(GlobalStringVariables.DESTROY_TRIGGER_TAG))
            other.GetComponent<DestroyTrigger>().DestroyObjects();

        if (other.CompareTag(GlobalStringVariables.WAIT_AREA_TRIGGER_TAG))
            other.GetComponent<WaitArea>().SetTarget(transform);

        if (other.CompareTag(GlobalStringVariables.DIAMOND_TAG))
        {
            OnDiamondGained();
            Destroy(other.gameObject);
        }

        if (other.CompareTag(GlobalStringVariables.COIN_TAG))
        {
            OnCoinGained();
            Destroy(other.gameObject);
        }

        if (other.CompareTag(GlobalStringVariables.HEART_TAG))
        {
            OnHPGained();
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(GlobalStringVariables.HINT_TAG))
            OnHint(false, other.GetComponent<HintTable>().GetHintText());

        if (other.CompareTag(GlobalStringVariables.WAIT_AREA_TRIGGER_TAG))
            other.GetComponent<WaitArea>().RemoveTarget();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(GlobalStringVariables.WATER_TAG))
            SetDamage(GlobalConfigurationVariables.WATER_DAMAGE);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(GlobalStringVariables.ENEMY_TAG))
            SetDamage(collision.gameObject.GetComponent<IDamageable>().GetDamage());

        if (collision.collider.CompareTag(GlobalStringVariables.SPIKES_TAG))
            SetDamage(GlobalConfigurationVariables.SPIKES_DAMAGE);
    }

    private void OnDeath()
    {
        levelUI.ShowLoseScene();
    }

    private void OnFinish()
    {
        levelUI.ShowWinScene();
    }

    private void OnHint(bool isOpen, string text)
    {
        levelUI.ShowHint(isOpen, text);
    }

    private void OnCoinGained()
    {
        GlobalData.COINS_GAINED_COUNT++;
        levelUI.UpdateCoinsUI(GlobalData.COINS_GAINED_COUNT);
    }

    private void OnHPGained()
    {
        SetDamage(-GlobalConfigurationVariables.PLAYER_HEART_HEAL);
    }

    private void OnDiamondGained()
    {
        GlobalData.DIAMOND_GAINED_COUNT++;
        levelUI.UpdateDiamondsUI(GlobalData.DIAMOND_GAINED_COUNT);
    }

    private void Shoot()
    {
        if (curBullets > 0)
        {
            Bullet bullet = Instantiate(_bulletPrefab, _shootPoint.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.InitBullet(damage, GlobalStringVariables.PLAYER_TAG, GlobalStringVariables.ENEMY_TAG, playerMovement.GetFlipX() ? transform.right : -transform.right);
            curBullets--;
            AudioController.Instance.PlayShotSound();
            if (curBullets <= 0)
                StartCoroutine(nameof(Reloading));
            else
                _bulletText.text = $"{curBullets} / {maxBullets}";
        }
    }
    private IEnumerator Reloading()
    {
        curBullets = 0;
        _bulletText.text = "Перезарядка...";
        AudioController.Instance.PlayReloadSound();
        yield return new WaitForSeconds(GlobalConfigurationVariables.PLAYER_RELOADING_TIME);
        curBullets = maxBullets;
        _bulletText.text = $"{curBullets} / {maxBullets}";
    }
    private void Pause()
    {
        levelUI.Pause();
    }
    public void SetDamage(float damage)
    {
        curHealth = Mathf.Clamp(curHealth - damage, 0f, maxHealth);
        _healthBar.value = curHealth > 0 ? curHealth / maxHealth : 0f;

        if (curHealth <= 0f)
            OnDeath();
    }

    public float GetDamage()
    {
        return damage;
    }
}
