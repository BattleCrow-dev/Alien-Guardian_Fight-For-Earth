using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class ShootEnemy : MonoBehaviour, IDamageable, ITargetEnemy
{
    [Header("Points")]
    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;
    [SerializeField] private Transform _shootPoint;

    [Header("Parameters")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _shootDelay;

    [Header("Elements")]
    [SerializeField] private Slider _healthBar;
    [SerializeField] private GameObject _ui;

    [Header("Sprites")]
    [SerializeField] private Sprite _spriteBase;
    [SerializeField] private Sprite _spriteAngry;

    [Header("Prefabs")]
    [SerializeField] private GameObject _bulletPrefab;

    private float maxHealth;
    private float curHealth;
    private float damage;

    private float curDelay = 0;

    private Transform target;
    private SpriteRenderer spr;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        maxHealth = GlobalConfigurationVariables.SHOOT_ENEMY_MAX_HEALTH;
        curHealth = maxHealth;
        damage = GlobalConfigurationVariables.SHOOT_ENEMY_DAMAGE;

        _healthBar.value = curHealth / maxHealth;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        spr.sprite = _spriteAngry;
        curDelay = 0;
    }

    public void RemoveTarget()
    {
        target = null;
        spr.sprite = _spriteBase;
        curDelay = 0;
    }

    private void Update()
    {
        if (target)
        {
            if (Vector3.Distance(transform.position, target.position) < _maxDistance)
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, transform.position.z), -_moveSpeed * Time.deltaTime);

            if (transform.position.x >= _point2.position.x)
                transform.position = _point2.position;

            if (transform.position.x <= _point1.position.x)
                transform.position = _point1.position;

            transform.localScale = new Vector3(transform.position.x > target.position.x ? -1f : 1f, 1f, 1f);
            _ui.transform.localScale = transform.localScale;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (curDelay <= 0)
        {
            Bullet bullet = Instantiate(_bulletPrefab, _shootPoint.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.InitBullet(damage, GlobalStringVariables.ENEMY_TAG, GlobalStringVariables.PLAYER_TAG, transform.localScale.x < 0 ? -transform.right : transform.right);
            AudioController.Instance.PlayShotSound();
            curDelay = _shootDelay;
        }

        curDelay -= Time.deltaTime;
    }

    private void OnDeath()
    {
        GlobalData.ENEMY_KILLED_COUNT++;
        Destroy(transform.parent.gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetDamage(float damage)
    {
        curHealth -= damage;
        _healthBar.value = curHealth > 0 ? curHealth / maxHealth : 0f;

        if (curHealth <= 0)
            OnDeath();
    }
}
