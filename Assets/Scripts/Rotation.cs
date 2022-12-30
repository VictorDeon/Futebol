using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour {

    [SerializeField] private Transform startPosition;
    [SerializeField] private Image arrowImage;
    public float zRotation;
    public bool releaseRotation = true;

    void Start() {
        PositionArrow();
        PositionBall();
    }

    void Update() {
        RotationArrow();
        if(releaseRotation) {
            TouchRotation();
            limitsRotation();
        }
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
    
    void TouchRotation() {
        float moveY = Input.GetAxis("Mouse Y");

        if (zRotation < 90 && moveY > 0) {
            zRotation += 2.5f;
        }

        if (zRotation > 0 && moveY < 0) {
            zRotation -= 2.5f;
        }
    }

    void limitsRotation() {
        if (zRotation >= 90) {
            zRotation = 90;
        }

        if (zRotation <= 0) {
            zRotation = 0;
        }
    }
}
