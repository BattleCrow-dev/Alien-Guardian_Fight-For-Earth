using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject[] _objectsToDestroy;

    public void DestroyObjects()
    {
        foreach (GameObject obj in _objectsToDestroy)
            Destroy(obj);
    }
}
