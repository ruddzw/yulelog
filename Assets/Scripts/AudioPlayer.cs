using UnityEngine;
using System.Collections;

public class AudioPlayer : YuleMonoBehaviour {
    public AudioSource fireplace1;
    public AudioSource fireplace2;
    public AudioSource match;

    public void PlayMatch() {
        match.Play();
    }

    public void PlayFireplaces() {
        StartCoroutine(DoPlayFireplaces());
    }

    private const float kFireplacePlayTime = 23f;
    private IEnumerator DoPlayFireplaces() {
        fireplace2.volume = 0f;
        fireplace2.Stop();
        fireplace1.volume = 0f;
        fireplace1.Play();
        LeanTween.value(gameObject, (float vol) => {fireplace1.volume = vol;}, 0f, 1f, 1f);

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
