using UnityEngine;
using UnityEngine.UI;

public class Strength : MonoBehaviour {

    private Rigidbody2D ball;
    private float strength = 1000f;
    private Rotation rotation;
    
    void Start() {
        ball = GetComponent<Rigidbody2D>();
        rotation = GetComponent<Rotation>();
    }

    void Update() {
        ApplyForce();
    }

    // Aplica a for�a de acordo com o angulo inserido.
    void ApplyForce() {
        // Usamos seno e conseno para direcionar o disparo da bola na dire��o da rota��o da flexa.
        float x = strength * Mathf.Cos(rotation.zRotation * Mathf.Deg2Rad);
        float y = strength * Mathf.Sin(rotation.zRotation * Mathf.Deg2Rad);

        if (rotation.releasekick && Input.GetKeyUp(KeyCode.Space)) {
            ball.AddForce(new Vector2(x, y));
            rotation.releasekick = false;
        }
    }
}
