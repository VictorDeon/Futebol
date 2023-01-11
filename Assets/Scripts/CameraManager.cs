using UnityEngine;

public class CameraManager: MonoBehaviour {

    [SerializeField] private Transform leftObject, rightObject, ball;
    private float smooth = 1;

    // Garantir que a camera está sempre com foco na bola
    void Update() {
        if (GameManager.instance.gameStarted) {
            // Fazer com que a transição de volta da tela seja mais suave
            if (transform.position.x != leftObject.position.x) {
                smooth -= 0.8f * Time.deltaTime;
                transform.position = new Vector3(
                    Mathf.SmoothStep(leftObject.position.x, Camera.main.transform.position.x, smooth),
                    this.transform.position.y,
                    this.transform.position.z
                );
            }

            if (ball == null && GameManager.instance.sceneBalls > 0) {
                ball = GameObject.Find("Ball(Clone)").GetComponent<Transform>();
            } else if (GameManager.instance.sceneBalls > 0) {
                Vector3 cameraPosition = transform.position;
                // Limita a movimentação da camera entre os limites definidos e a bola
                cameraPosition.x = Mathf.Clamp(ball.position.x, leftObject.position.x, rightObject.position.x);
                transform.position = cameraPosition;
            }
        }
    }
}

