using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour {
    public AudioSource fireplace1;
    public AudioSource fireplace2;

    private const float kFireplacePlayTime = 23f;

    void Start() {
        StartCoroutine(PlayFireplaces());
    }

    private IEnumerator PlayFireplaces() {
        fireplace2.volume = 0f;
        fireplace2.Stop();
        fireplace1.volume = 1f;
        fireplace1.Play();

        while (true) {
            yield return new WaitForSeconds(kFireplacePlayTime);

            fireplace2.volume = 0f;
            fireplace2.Play();
            LeanTween.value(gameObject, (float vol) => {fireplace1.volume = vol;}, 1f, 0f, 1f);
            LeanTween.value(gameObject, (float vol) => {fireplace2.volume = vol;}, 0f, 1f, 1f);

            yield return new WaitForSeconds(1f);

            fireplace1.Stop();

            yield return new WaitForSeconds(kFireplacePlayTime);

            fireplace1.volume = 0f;
            fireplace1.Play();
            LeanTween.value(gameObject, (float vol) => {fireplace1.volume = vol;}, 0f, 1f, 1f);
            LeanTween.value(gameObject, (float vol) => {fireplace2.volume = vol;}, 1f, 0f, 1f);

            yield return new WaitForSeconds(1f);

            fireplace2.Stop();
        }
    }
}
