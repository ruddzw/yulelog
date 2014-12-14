using UnityEngine;
using System.Collections;

public class Initialization : YuleMonoBehaviour {
    void Start() {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        StartCoroutine(LightLog());
    }

    private IEnumerator LightLog() {
        GetComponent<AudioPlayer>().PlayMatch();

        yield return new WaitForSeconds(3f);

        GetComponent<AudioPlayer>().PlayFireplaces();

        GameObject centerLogPart = GetLogPart(9);
        centerLogPart.GetComponent<LogPart>().Light();
    }
}
