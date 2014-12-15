using UnityEngine;
using System.Collections;

public class TouchReceiver : YuleMonoBehaviour {
    public ParticleSystem sparkSystem;

    void Update() {
        if (Input.touchCount > 0) {
            foreach (Touch touch in Input.touches) {
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(touch.position);
                if (touch.phase == TouchPhase.Began) {
                    InputDownAtPoint(worldPosition);
                } else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
                    InputUpAtPoint(worldPosition);
                } else {
                    InputStayAtPoint(worldPosition);
                }
            }
        } else {
            if (Input.GetMouseButtonDown(0)) {
                InputDownAtPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            } else if (Input.GetMouseButton(0)) {
                InputStayAtPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            } else if (Input.GetMouseButtonUp(0)) {
                InputUpAtPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }
    }

    private void InputDownAtPoint(Vector2 point2d) {
        // point2d is in world space
        Vector3 point = new Vector3(point2d.x, point2d.y, 0f);

        // find the log part we tapped, if any
        GameObject tappedLogPart = null;
        GameObject[] logParts = GetLogParts();
        foreach (GameObject logPart in logParts) {
            if (logPart.collider2D.bounds.Contains(point)) {
                tappedLogPart = logPart;
                break;
            }
        }

        if (tappedLogPart != null) {
            LogPart logPart = tappedLogPart.GetComponent<LogPart>();
            if (logPart.CanBePoked()) {
                GetComponent<AudioPlayer>().PlayPoke();
                SparkAtPoint(point2d, tappedLogPart);
                tappedLogPart.GetComponent<LogPart>().Poke();
            }
        }
    }

    private void InputStayAtPoint(Vector2 point2d) {}

    private void InputUpAtPoint(Vector2 point2d) {}

    public void SparkAtPoint(Vector2 point2d, GameObject logPart) {
        GetLog().GetComponent<Log>().SparkAtPoint(point2d, logPart);
    }
}
