using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public Transform fireParticleSystemPrefab;

    public ParticleSystem SpawnFireSystem(LogPart logPart) {
        Transform fireParticleSystemInstance = Instantiate(fireParticleSystemPrefab, logPart.FirePosition(), fireParticleSystemPrefab.rotation) as Transform;
        fireParticleSystemInstance.transform.parent = logPart.transform;

        ParticleSystem fireSystem = fireParticleSystemInstance.GetComponent<ParticleSystem>();

        return fireSystem;
    }
}
