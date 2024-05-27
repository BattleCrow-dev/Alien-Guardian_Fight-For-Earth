using UnityEngine;

public class Box : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _prefabInBox;

    private int health = 3;

    public void OnHit()
    {
        health--;
        if (health <= 0)
        {
            if (_prefabInBox)
                Instantiate(_prefabInBox, transform.position, transform.rotation);

            AudioController.Instance.PlayBoxSound();
            Destroy(gameObject);
        }
    }
}
