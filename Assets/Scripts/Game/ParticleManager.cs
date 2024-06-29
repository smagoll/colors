using CartoonFX;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;
    
    [SerializeField]
    private Effect doneEffect;
    [SerializeField]
    private Effect firework;
    [SerializeField]
    private Transform transformParcticles;

    private ObjectPool<Effect> fireworks;
    private ObjectPool<Effect> donePool;
    
    public bool IsFirework { get; set; }
    
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        fireworks = CreatePool(firework);
        donePool = CreatePool(doneEffect);
    }

    public void CellDone(Transform transform)
    {
        var doneEffectObj = donePool.Get();
        doneEffectObj.pool = donePool;
        doneEffectObj.transform.position = transform.position;
        AudioController.instance.PlaySFX(AudioController.instance.done);
    }
    
    public async UniTaskVoid FireworksComplete()
    {
        IsFirework = true;
        while (IsFirework)
        {
            Vector2 randomPositionOnScreen = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, Random.value));
            var fireworkObj = fireworks.Get();
            fireworkObj.pool = fireworks;
            fireworkObj.transform.position = randomPositionOnScreen;
            AudioController.instance.PlaySFX(AudioController.instance.firework);
            await UniTask.Delay(500);
        }
    }
    
    private ObjectPool<Effect> CreatePool(Effect effect)
    {
        ObjectPool<Effect> pool = new(() =>
        {
            return Instantiate(effect, transformParcticles);
        }, obj => {
            obj.gameObject.SetActive(true);
        }, obj => {
            obj.gameObject.SetActive(false);
        }, obj => {
            Destroy(obj);
        }, false);

        return pool;
    }
}
