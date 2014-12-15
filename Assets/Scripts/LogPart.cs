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
    private bool fullyLit;
    private float lightTimer;
    private const float kLightTimerMax = 2f;
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

        if (lit && lightTimer <= kLightTimerMax) {
            lightTimer += Time.deltaTime;

            float amountLit = lightTimer / kLightTimerMax;
            float lifetimeMin = Mathf.Lerp(0f, normalLifetimeMin, amountLit);
            float lifetimeMax = Mathf.Lerp(0f, normalLifetimeMax, amountLit);
            float speedMin = Mathf.Lerp(0f, normalSpeedMin, amountLit);
            float speedMax = Mathf.Lerp(0f, normalSpeedMax, amountLit);

            fireSystem.startLifetime = Random.Range(lifetimeMin, lifetimeMax);
            fireSystem.startSpeed = Random.Range(speedMin, speedMax);
            fireSystem.emissionRate = normalEmissionRate;
        } else if (poked) {
            pokeTimer += Time.deltaTime;
            if (pokeTimer >= kPokeTimerMax) {
                poked = false;
            }

            fireSystem.startLifetime = Random.Range(pokedLifetimeMin, pokedLifetimeMax);
            fireSystem.startSpeed = Random.Range(pokedSpeedMin, pokedSpeedMax);
            fireSystem.emissionRate = pokedEmissionRate;
        } else {
            fullyLit = true;
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
        lightTimer = 0f;

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

    public bool CanBePoked() {
        return fullyLit && !poked;
    }

    public void Poke() {
        if (!CanBePoked()) {
            return;
        }

        pokeTimer = 0f;
        poked = true;

        StartCoroutine(DoNearbyPokes());
    }
    private IEnumerator DoNearbyPokes() {
        yield return new WaitForSeconds(0.05f);

        if (left != null) {
            left.NearbyPoke();
        }
        if (right != null) {
            right.NearbyPoke();
        }
    }

    public void NearbyPoke() {
        if (!CanBePoked()) {
            return;
        }

        pokeTimer = kPokeTimerMax / 2f;
        poked = true;

        StartCoroutine(DoKindaNearbyPokes());
    }
    private IEnumerator DoKindaNearbyPokes() {
        yield return new WaitForSeconds(0.05f);

        if (left != null) {
            left.KindaNearbyPoke();
        }
        if (right != null) {
            right.KindaNearbyPoke();
        }
    }

    public void KindaNearbyPoke() {
        if (!CanBePoked()) {
            return;
        }

        pokeTimer = kPokeTimerMax / 4f;
        poked = true;

        StartCoroutine(DoFarawayPokes());
    }
    private IEnumerator DoFarawayPokes() {
        yield return new WaitForSeconds(0.05f);

        if (left != null) {
            left.FarawayPoke();
        }
        if (right != null) {
            right.FarawayPoke();
        }
    }

    public void FarawayPoke() {
        if (!CanBePoked()) {
            return;
        }

        pokeTimer = kPokeTimerMax / 8f;
        poked = true;
    }
}
