using UnityEngine;

public class WaitArea : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private WaitEnemy _waitEnemy;
    [SerializeField] private FlyEnemy _flyEnemy;
    [SerializeField] private ShootEnemy _shootEnemy;

    public void SetTarget(Transform target)
    {
        if(_waitEnemy)
            _waitEnemy.SetTarget(target);
        if (_flyEnemy)
            _flyEnemy.SetTarget(target);
        if (_shootEnemy)
            _shootEnemy.SetTarget(target);
    }
    public void RemoveTarget()
    {
        if (_waitEnemy)
            _waitEnemy.RemoveTarget();
        if (_flyEnemy)
            _flyEnemy.RemoveTarget();
        if (_shootEnemy)
            _shootEnemy.RemoveTarget();
    }
}
