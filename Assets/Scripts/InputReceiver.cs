using UnityEngine;
using System.Collections;

public class InputReceiver : YuleMonoBehaviour {
    public ParticleSystem sparkSystem;

    private Vector3 lastMousePosition;
    private float timeInLastMousePosition;
    private const float kTimeInSamePositionToHideCursor = 1f;

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
            bool showCursor;
            if (lastMousePosition == Input.mousePosition) {
                timeInLastMousePosition += Time.deltaTime;
                showCursor = timeInLastMousePosition < kTimeInSamePositionToHideCursor;
            } else {
                timeInLastMousePosition = 0f;
                showCursor = true;
            }
            Screen.showCursor = showCursor;
            lastMousePosition = Input.mousePosition;

            if (Input.GetMouseButtonDown(0)) {
                InputDownAtPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            } else if (Input.GetMouseButton(0)) {
                InputStayAtPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            } else if (Input.GetMouseButtonUp(0)) {
                InputUpAtPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        if (Input.GetButtonDown("Quit")) {
            Application.Quit();
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
            Log log = GetLog().GetComponent<Log>();
            LogPart logPart = tappedLogPart.GetComponent<LogPart>();
            if (log.CanBePoked() && logPart.CanBePoked()) {
                GetComponent<AudioPlayer>().PlayPoke();
                log.SparkAtPoint(point2d, tappedLogPart);
                tappedLogPart.GetComponent<LogPart>().Poke();
            }
        }
    }

    private void InputStayAtPoint(Vector2 point2d) {}

    private void InputUpAtPoint(Vector2 point2d) {}
}
