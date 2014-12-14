using UnityEngine;
using System.Collections;

public class Match : YuleMonoBehaviour {
    public ParticleSystem fireSystem;

    public float unlitSize;
    public float normalSize;
    public float growMaxSize;

    private bool litGrowing;
    private bool litStayingGrown;
    private bool litShrinking;
    private float litTimer;
    private const float kLightGrowTime = 0.5f;
    private const float kLightStayGrownTime = 0.25f;
    private const float kLightShrinkTime = 0.5f;

    public void StartLightingAnimation() {
        GetComponent<Animator>().SetTrigger("Start");
    }

    public void LightMatch() {
        fireSystem.Play();
        litGrowing = true;
        litTimer = 0f;
    }

    void Update() {
        if (litGrowing) {
            litTimer += Time.deltaTime;

            float amountGrown = litTimer / kLightGrowTime;
            float startSize = Mathf.Lerp(unlitSize, growMaxSize, amountGrown);
            fireSystem.startSize = startSize;

            if (litTimer > kLightGrowTime) {
                litTimer = 0f;
                litGrowing = false;
                litStayingGrown = true;
            }
        }
        if (litStayingGrown) {
            litTimer += Time.deltaTime;

            fireSystem.startSize = growMaxSize;

            if (litTimer > kLightStayGrownTime) {
                litTimer = 0f;
                litStayingGrown = false;
                litShrinking = true;
            }
        }
        if (litShrinking) {
            litTimer += Time.deltaTime;

            float amountShrunk = litTimer / kLightShrinkTime;
            float startSize = Mathf.Lerp(growMaxSize, normalSize, amountShrunk);
            fireSystem.startSize = startSize;

            if (litTimer > kLightShrinkTime) {
                litTimer = 0f;
                litShrinking = false;
            }
        }
    }
}
