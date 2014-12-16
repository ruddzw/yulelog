using UnityEngine;
using System.Collections;

public class Log : YuleMonoBehaviour {
    public ParticleSystem sparkSystem;

    public Vector3 pokeOffset;

    private bool inPoke;

    public bool CanBePoked() {
        return !inPoke;
    }

    public void SparkAtPoint(Vector2 point2d, GameObject logPart) {
        sparkSystem.transform.position = new Vector3(point2d.x, point2d.y, sparkSystem.transform.position.z);

        sparkSystem.Stop();
        sparkSystem.Clear();
        sparkSystem.Simulate(0.005f, true); // Workaround for weird behavior from http://forum.unity3d.com/threads/moving-a-particlesystem-why-does-unity-interpolate-its-position.134283/
        sparkSystem.Play();

        inPoke = true;
        LeanTween.move(gameObject, transform.position + pokeOffset, 0.1f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
            LeanTween.move(gameObject, transform.position - pokeOffset, 0.1f).setEase(LeanTweenType.easeInOutQuad).setOnComplete(() => {
                inPoke = false;
            });
        });
    }
}
