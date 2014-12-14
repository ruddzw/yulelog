using UnityEngine;
using System.Collections;

public class Initialization : MonoBehaviour {
    void Start() {
        Application.targetFrameRate = 60;

        StartCoroutine(LightLog());
    }

    private IEnumerator LightLog() {
        GetComponent<AudioPlayer>().PlayMatch();

        yield return new WaitForSeconds(3f);

        GetComponent<AudioPlayer>().PlayFireplaces();

        foreach (GameObject logPart in GameObject.FindGameObjectsWithTag("LogPart")) {
            logPart.GetComponent<LogPart>().Light();
        }
    }
}
