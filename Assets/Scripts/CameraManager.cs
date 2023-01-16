using UnityEngine;
using Constants;

public class CameraManager: MonoBehaviour {

    [SerializeField] private Transform leftObject, rightObject, ball;
    private float smooth = 1;
    public bool startAnimationFinished = false;

    public static CameraManager instance;

    void Awake() {
        if(instance == null) {
            instance = this;
        }
    }

    // Garantir que a camera est� sempre com foco na bola
    void Update() {
        if (startAnimationFinished && GameManager.instance.gameStarted) {
            // Fazer com que a transi��o de volta da tela seja mais suave
            if (transform.position.x != leftObject.position.x) {
                smooth -= 0.8f * Time.deltaTime;
                transform.position = new Vector3(
                    Mathf.SmoothStep(leftObject.position.x, Camera.main.transform.position.x, smooth),
                    this.transform.position.y,
                    this.transform.position.z
                );
            }

            if (ball == null && GameManager.instance.sceneBalls > 0) {
                int ballId = PlayerPrefs.GetInt("BallInUse");
                ball = GameObject.Find($"{VARIABLES.BALL_IN_USE[ballId]}(Clone)").GetComponent<Transform>();
            } else if (GameManager.instance.sceneBalls > 0) {
                Vector3 cameraPosition = transform.position;
                // Limita a movimenta��o da camera entre os limites definidos e a bola
                cameraPosition.x = Mathf.Clamp(ball.position.x, leftObject.position.x, rightObject.position.x);
                transform.position = cameraPosition;
            }
        }
    }

    // Ao terminar a anima��o libere o foco da tela na bola
    // Foi inserido como evento dentro da anima��o
    void FinishAnimation() {
        this.startAnimationFinished = true;
        this.GetComponent<Animator>().enabled = false;
    }
}

