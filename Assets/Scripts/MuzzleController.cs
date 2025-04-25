using UnityEngine;

public class MuzzleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] muzzleFlashPrefab;
    public void PlayMuzzleFlash()
    {
        for(int i = 0; i < muzzleFlashPrefab.Length; i++)
        {
            muzzleFlashPrefab[i].Play();
        }
    }
}
