using UnityEngine;
using System.Collections;

public class LogPart : MonoBehaviour {
	public LogPart left;
    public LogPart right;

    public float normalLifetimeMin;
    public float normalLifetimeMax;
    public float normalSpeedMin;
    public float normalSpeedMax;
    public float normalEmissionRate;

    private ParticleSystem fireSystem;

    public Vector3 FirePosition() {
        return new Vector3(transform.position.x, -3.5f, 1f);
    }

    void Start() {
        Spawner spawner = GameObject.FindWithTag("GameController").GetComponent<Spawner>();
        fireSystem = spawner.SpawnFireSystem(this);
        InitializeFireSystem();
    }

    void Update() {
        fireSystem.startLifetime = Random.Range(normalLifetimeMin, normalLifetimeMax);
        fireSystem.startSpeed = Random.Range(normalSpeedMin, normalSpeedMax);
    }

    private void InitializeFireSystem() {
        fireSystem.startLifetime = normalLifetimeMin;
        fireSystem.startSpeed = Random.Range(normalSpeedMin, normalSpeedMax);
        fireSystem.emissionRate = normalEmissionRate;

        fireSystem.Play();
    }
}
