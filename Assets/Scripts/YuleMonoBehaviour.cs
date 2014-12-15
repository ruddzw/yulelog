using UnityEngine;
using System.Collections;

public class YuleMonoBehaviour : MonoBehaviour {
    protected GameObject GetMatch() {
        return GameObject.FindWithTag("Match");
    }

    protected GameObject GetLog() {
        return GameObject.FindWithTag("Log");
    }

    protected GameObject[] GetLogParts() {
        return GameObject.FindGameObjectsWithTag("LogPart");
    }

    protected GameObject GetLogPart(int num) {
        foreach (GameObject logPart in GetLogParts()) {
            if (logPart.GetComponent<LogPart>().number == num) {
                return logPart;
            }
        }
        return null;
    }
}
