using UnityEngine;
using UnityEngine.Pool;

public class Effect : MonoBehaviour
{
    public ObjectPool<Effect> pool;

    private void OnDisable()
    {
        pool.Release(this);
    }
}
