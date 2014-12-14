using UnityEngine;
using System.Collections;

public class Initialization : YuleMonoBehaviour {
    void Start() {
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        StartCoroutine(LightLog());
    }

    private IEnumerator LightLog() {
        yield return new WaitForSeconds(1f);

        GetMatch().GetComponent<Animator>().SetTrigger("Start");

        yield return new WaitForSeconds(0.5f);

        GetComponent<AudioPlayer>().PlayMatch();

        yield return new WaitForSeconds(4f);

        GetComponent<AudioPlayer>().PlayFireplaces();

        GameObject centerLogPart = GetLogPart(9);
        centerLogPart.GetComponent<LogPart>().Light();
    }
}
