using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class PatrolEnemy : MonoBehaviour, IDamageable
{
    [Header("Move points")]
    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;

    [Header("Variables")]
    [SerializeField] private float _moveSpeed;

    [Header("Elements")]
    [SerializeField] private Slider _healthBar;

    private float maxHealth;
    private float curHealth;
    private float damage;

    private Vector3 targetPoint;
    private SpriteRenderer spr;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        transform.position = _point1.position;
        targetPoint = _point2.position;

        maxHealth = GlobalConfigurationVariables.PATROL_SPIKE_MAX_HEALTH;
        curHealth = maxHealth;
        damage = GlobalConfigurationVariables.PATROL_SPIKE_DAMAGE;

        _healthBar.value = curHealth / maxHealth;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPoint, _moveSpeed * Time.deltaTime);

        if (transform.position == targetPoint)
        {
            if (transform.position == _point1.position)
                targetPoint = _point2.position;
            else if (transform.position == _point2.position)
                targetPoint = _point1.position;

            spr.flipX = !spr.flipX;
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
