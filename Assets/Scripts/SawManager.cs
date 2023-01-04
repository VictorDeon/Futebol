using UnityEngine;

public class SawManager : MonoBehaviour {

    private SliderJoint2D saw;
    private JointMotor2D motor;

    void Start() {
        saw = GetComponent<SliderJoint2D>();
        motor = saw.motor;
    }

    void Update() {
        if (saw.limitState == JointLimitState2D.UpperLimit) {
            // Movimenta da direita para a esquerda em velocidades aleatorias de 1 a 5
            motor.motorSpeed = Random.Range(-1, -5);
            saw.motor = motor;
        } else if (saw.limitState == JointLimitState2D.LowerLimit) {
            // Movimenta da esquerda para a direita em velocidades aleatorias de 1 a 5
            motor.motorSpeed = Random.Range(1, 5);
            saw.motor = motor;
        }
    }
}
