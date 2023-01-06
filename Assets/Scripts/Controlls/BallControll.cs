using UnityEngine;
using UnityEngine.UI;

public class BallControll: MonoBehaviour {
    [SerializeField] private Transform positionStart;

    // Seta
    public GameObject arrow;
    public GameObject arrowWithForce;

    // Angulo
    public bool releaseRotation = false;
    public bool releasekick = false;
    public float zRotation;

    // Força
    private Rigidbody2D ball;
    public float strength = 0;

    void Awake() {
        arrow = GameObject.Find("Arrow");
        arrowWithForce = arrow.transform.GetChild(0).gameObject;
        arrow.SetActive(false);
    }

    void Start() {
        positionStart = GameObject.Find("Ball Start Position").GetComponent<Transform>();
        ball = GetComponent<Rigidbody2D>();
        PositionBall();
    }

    void Update() {
        ApplyForce();
        
        if(releaseRotation) {
            ForceControl();
            RotationArrow();
            TouchRotation();
            limitsRotation();
        }
    }

    private void FixedUpdate() {
        PositionArrow();
    }

    void PositionArrow() {
        arrow.gameObject.transform.position = this.transform.position;
    }

    void PositionBall() {
        this.gameObject.transform.position = positionStart.position;
    }

    void RotationArrow() {
        // Quero chutar a bola do angulo 0 até o 90º
        arrow.gameObject.transform.eulerAngles = new Vector3(0, 0, zRotation);
    }

    void TouchRotation() {
        float moveY = Input.GetAxis("Mouse Y");

        if(zRotation < 90 && moveY < 0) {
            zRotation += 2.5f;
        }

        if(zRotation > 0 && moveY > 0) {
            zRotation -= 2.5f;
        }
    }

    void limitsRotation() {
        if(zRotation >= 90) {
            zRotation = 90;
        }

        if(zRotation <= 0) {
            zRotation = 0;
        }
    }

    // Clica na bola
    void OnMouseDown() {
        if(!GameManager.instance.kicked) {
            releaseRotation = true;
            arrow.SetActive(true);
        }
    }

    // Solta o clique da bola
    void OnMouseUp() {
        releaseRotation = false;
        if(!GameManager.instance.kicked && strength > 0) {
            releasekick = true;
            GameManager.instance.kicked = true;
        }
        arrow.SetActive(false);
    }

    // Direciona a força de acordo com o angulo inserido.
    void ApplyForce() {
        // Usamos seno e conseno para direcionar o disparo da bola na direção da rotação da flexa.
        float x = strength * Mathf.Cos(zRotation * Mathf.Deg2Rad);
        float y = strength * Mathf.Sin(zRotation * Mathf.Deg2Rad);

        if(releasekick) {
            ball.AddForce(new Vector2(x, y));
            releasekick = false;
        }
    }

    void ForceControl() {
        float moveX = Input.GetAxis("Mouse X");
        Image arrowWithForceImg = arrowWithForce.GetComponent<Image>();

        if(moveX < 0) {
            arrowWithForceImg.fillAmount += 1f * Time.deltaTime;
            strength = arrowWithForceImg.fillAmount * 1000;
        }

        if(moveX > 0) {
            arrowWithForceImg.fillAmount -= 1f * Time.deltaTime;
            strength = arrowWithForceImg.fillAmount * 1000;
        }
    }

    // Ao terminar a animação deixe a bola dinamica para a gravidade e força agir
    // Foi inserido como evento dentro da animação
    void DynamicBall() {
        ball.isKinematic = false;
    }
}

