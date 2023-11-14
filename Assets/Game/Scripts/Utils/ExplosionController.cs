using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] private Sound SfxEffect;
    private PoolManager _poolManager => PoolManager.I;

    private AudioManager _audioManager => AudioManager.I;

    public void PlayAudio()
    {
        _audioManager.PlaySfx(SfxEffect.soundName);
    }

    public void DestroyEffects()
    {
        _poolManager.ReturnPool(gameObject);
    }
}
