using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private float _bulletSpeed;

    private float damage;
    private string targetTag;
    private string mineTag;
    private Vector3 direction;

    private Rigidbody2D rb;

    public void InitBullet(float damage, string mineTag, string targetTag, Vector3 direction)
    {
        this.damage = damage;
        this.targetTag = targetTag;
        this.mineTag = mineTag;
        this.direction = direction;

        rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + _bulletSpeed * Time.fixedDeltaTime * direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(mineTag) && collision.CompareTag(targetTag) && collision.gameObject.TryGetComponent(out IDamageable _))
        {
            collision.gameObject.GetComponent<IDamageable>().SetDamage(damage);
            AudioController.Instance.PlayEnemyDamageSound();
            Destroy(gameObject);
        }
        else if (collision.CompareTag(GlobalStringVariables.GROUND_TAG))
            Destroy(gameObject);
        else if (collision.CompareTag(GlobalStringVariables.BOX_TAG))
        {
            collision.GetComponent<Box>().OnHit();
            Destroy(gameObject);
        }
    }
}
