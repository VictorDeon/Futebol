using UnityEngine;
using UnityEngine.UI;

public class Rotation : MonoBehaviour {

    [SerializeField] private Image arrowImage;
    public bool releaseRotation = false;
    public bool releasekick = false;
    public float zRotation;

    void Update() {
        if(releaseRotation && arrowImage) {
            RotationArrow();
            TouchRotation();
            limitsRotation();
        }
    }

    private void FixedUpdate() {
        if(arrowImage) {
            PositionArrow();
        }
    }

    void PositionArrow() {
        arrowImage.rectTransform.position = this.transform.position;
    }

    void RotationArrow() {
        // Quero chutar a bola do angulo 0 até o 90º
        arrowImage.rectTransform.eulerAngles = new Vector3(0, 0, zRotation);
    }
    
    void TouchRotation() {
        float moveY = Input.GetAxis("Mouse Y");

        if (zRotation < 90 && moveY < 0) {
            zRotation += 2.5f;
        }

        if (zRotation > 0 && moveY > 0) {
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

    // Clica na bola
    void OnMouseDown() {
        releaseRotation = true;
    }

    // Solta o clique da bola
    void OnMouseUp() {
        releaseRotation = false;
        releasekick = true;
        Destroy(arrowImage.gameObject);
    }
}
