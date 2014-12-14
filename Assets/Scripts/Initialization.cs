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

        GameObject matchObj = GetMatch();
        Match match = matchObj.GetComponent<Match>();
        match.StartLightingAnimation();

        yield return new WaitForSeconds(0.5f);

        GetComponent<AudioPlayer>().PlayMatch();

        yield return new WaitForSeconds(0.2f);

        match.Spark();

        yield return new WaitForSeconds(1.45f);

        match.Spark();

        yield return new WaitForSeconds(0.05f);

        match.LightMatch();

        yield return new WaitForSeconds(2.15f);

        GetComponent<AudioPlayer>().PlayFireplaces();

        GameObject centerLogPart = GetLogPart(9);
        centerLogPart.GetComponent<LogPart>().Light();
    }
}
