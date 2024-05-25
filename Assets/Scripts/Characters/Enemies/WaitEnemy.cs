using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class WaitEnemy : MonoBehaviour, IDamageable, ITargetEnemy
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
    private Animator anim;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        maxHealth = GlobalConfigurationVariables.WAIT_ENEMY_MAX_HEALTH;
        curHealth = maxHealth;
        damage = GlobalConfigurationVariables.WAIT_ENEMY_DAMAGE;

        _healthBar.value = curHealth / maxHealth;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        anim.SetBool(GlobalStringVariables.ANIMATION_STATE_NAME, true);
    }

    public void RemoveTarget()
    {
        target = null;
        anim.SetBool(GlobalStringVariables.ANIMATION_STATE_NAME, false);
    }

    private void Update()
    {
        if (target)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(target.position.x, transform.position.y, transform.position.z), _moveSpeed * Time.deltaTime);
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
