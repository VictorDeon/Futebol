using UnityEngine;
using UnityEngine.UI;

public class Strength : MonoBehaviour {

    private Rigidbody2D ball;
    private Rotation rotation;
    public float strength = 0;
    public Image arrowWithForce;

    void Start() {
        ball = GetComponent<Rigidbody2D>();
        rotation = GetComponent<Rotation>();
    }

    void Update() {
        ForceControl();
        ApplyForce();
    }

    // Direciona a força de acordo com o angulo inserido.
    void ApplyForce() {
        // Usamos seno e conseno para direcionar o disparo da bola na direção da rotação da flexa.
        float x = strength * Mathf.Cos(rotation.zRotation * Mathf.Deg2Rad);
        float y = strength * Mathf.Sin(rotation.zRotation * Mathf.Deg2Rad);

        if (rotation.releasekick) {
            ball.AddForce(new Vector2(x, y));
            rotation.releasekick = false;
        }
    }

    void ForceControl() {
        if (rotation.releaseRotation) {
            float moveX = Input.GetAxis("Mouse X");

            if (moveX < 0) {
                arrowWithForce.fillAmount += 0.8f * Time.deltaTime;
                strength = arrowWithForce.fillAmount * 1000;
            }

            if (moveX > 0) {
                arrowWithForce.fillAmount -= 0.8f * Time.deltaTime;
                strength = arrowWithForce.fillAmount * 1000;
            }
        }
    }
}
