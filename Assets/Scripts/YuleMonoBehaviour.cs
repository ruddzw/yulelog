using UnityEngine;
using System.Collections;

public class YuleMonoBehaviour : MonoBehaviour {
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
