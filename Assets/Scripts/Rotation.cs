using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour {

    [SerializeField] private Transform startPosition;
    [SerializeField] private Image arrowImage;
    public float zRotation;

    void Start() {
        PositionArrow();
        PositionBall();
    }

    void Update() {
        RotationArrow();
        InputRotation();
    }

    void PositionArrow() {
        arrowImage.rectTransform.position = startPosition.position;
    }

    void PositionBall() {
        this.gameObject.transform.position = startPosition.position;
    }

    void RotationArrow() {
        // Quero chutar a bola do angulo 0 até o 90º
        arrowImage.rectTransform.eulerAngles = new Vector3(0, 0, zRotation);
    }

    void InputRotation() {
        if (Input.GetKey(KeyCode.UpArrow)) {
            zRotation += 2.5f;
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            zRotation -= 2.5f;
        }
    }
}
