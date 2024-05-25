using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class FlyEnemy : MonoBehaviour, IDamageable, ITargetEnemy
{
    [Header("Parameters")]
    [SerializeField] private float _moveSpeed;

    [Header("Elements")]
    [SerializeField] private Slider _healthBar;

    private float maxHealth;
    private float curHealth;
    private float damage;

    private Transform target;
    private SpriteRenderer spr;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        maxHealth = GlobalConfigurationVariables.FLY_ENEMY_MAX_HEALTH;
        curHealth = maxHealth;
        damage = GlobalConfigurationVariables.FLY_ENEMY_DAMAGE;

        _healthBar.value = curHealth / maxHealth;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void RemoveTarget()
    {
        target = null;
    }

    private void Update()
    {
        if (target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
            spr.flipX = transform.position.x < target.position.x;
        }
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
