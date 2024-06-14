using System.Collections;
using UnityEngine;

public class EffectManager : SceneSingleton<EffectManager>
{
    public GameObject aircraftExplosionEffect;
    public GameObject aircraftFireEffect;

    public void EffectGenerate(GameObject item, Vector3 position)
    {
        GameObject effect = ObjectPoolManager.Instance.DequeueObject(item);
        effect.transform.position = position;
        effect.transform.rotation = Quaternion.identity;

        StartCoroutine(EffectEnqueue(effect, 15));
    }

    public void AircraftExplosionEffectGenerate(Vector3 position)
    {
        EffectGenerate(aircraftExplosionEffect, position);
    }
    public void AircraftFireEffectGenerate(Transform transform)
    {
        GameObject effect = ObjectPoolManager.Instance.DequeueObject(aircraftFireEffect);
        effect.GetComponent<ParticleSystem>().Play();

        effect.transform.SetParent(transform);
        effect.transform.position = transform.position;
        effect.transform.rotation = transform.rotation;

        StartCoroutine(OffParticle(effect, 2.4f));
    }

    IEnumerator EffectEnqueue(GameObject item, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        ObjectPoolManager.Instance.EnqueueObject(item);
    }

    IEnumerator OffParticle(GameObject item, float delayTime)
    {        
        yield return new WaitForSeconds(delayTime);
        if (item != null)
        {
            item.transform.SetParent(null);
            StartCoroutine(EffectEnqueue(item, 5));
        }
    }
}
