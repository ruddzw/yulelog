using UnityEngine;
using System.Collections;

public class LogPart : YuleMonoBehaviour {
    public int number;
	public LogPart left;
    public LogPart right;

    public float normalLifetimeMin;
    public float normalLifetimeMax;
    public float normalSpeedMin;
    public float normalSpeedMax;
    public float normalEmissionRate;

    public float pokedLifetimeMin;
    public float pokedLifetimeMax;
    public float pokedSpeedMin;
    public float pokedSpeedMax;
    public float pokedEmissionRate;

    private ParticleSystem fireSystem;

    private bool lit;
    private float pokeTimer;
    private bool poked;
    private const float kPokeTimerMax = 0.25f;

    public Vector3 FirePosition() {
        return new Vector3(transform.position.x, -3.5f, 1f);
    }

    void Update() {
        if (!lit) {
            return;
        }

        if (poked) {
            pokeTimer += Time.deltaTime;
            if (pokeTimer >= kPokeTimerMax) {
                poked = false;
            }

            fireSystem.startLifetime = Random.Range(pokedLifetimeMin, pokedLifetimeMax);
            fireSystem.startSpeed = Random.Range(pokedSpeedMin, pokedSpeedMax);
            fireSystem.emissionRate = pokedEmissionRate;
        } else {
            fireSystem.startLifetime = Random.Range(normalLifetimeMin, normalLifetimeMax);
            fireSystem.startSpeed = Random.Range(normalSpeedMin, normalSpeedMax);
            fireSystem.emissionRate = normalEmissionRate;
        }
    }

    private void InitializeFireSystem() {
        fireSystem.startLifetime = normalLifetimeMin;
        fireSystem.startSpeed = Random.Range(normalSpeedMin, normalSpeedMax);
        fireSystem.emissionRate = normalEmissionRate;

        fireSystem.Play();
    }

    public void Light() {
        if (lit) {
            return;
        }

        Spawner spawner = GameObject.FindWithTag("GameController").GetComponent<Spawner>();
        fireSystem = spawner.SpawnFireSystem(this);
        InitializeFireSystem();
        lit = true;

        StartCoroutine(LightNeighbors());
    }
    private IEnumerator LightNeighbors() {
        yield return new WaitForSeconds(0.25f);

        if (left != null && !left.lit) {
            left.Light();
        }
        if (right != null && !right.lit) {
            right.Light();
        }
    }

    public void Poke() {
        if (!lit) {
            return;
        }

        pokeTimer = 0f;
        poked = true;

        if (left != null) {
            left.NearbyPoke();
        }
        if (right != null) {
            right.NearbyPoke();
        }
    }

    public void NearbyPoke() {
        if (!lit) {
            return;
        }

        pokeTimer = kPokeTimerMax / 2f;
        poked = true;
    }
}
