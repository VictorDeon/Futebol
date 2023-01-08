using UnityEngine;
using UnityEngine.UI;

public class BallControll: MonoBehaviour {
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

    // Paredes
    private Transform leftWall, rightWall;

    void Awake() {
        arrow = GameObject.Find("Arrow");
        arrowWithForce = arrow.transform.GetChild(0).gameObject;
        arrow.GetComponent<Image>().enabled = false;
        arrowWithForce.GetComponent<Image>().enabled = false;
        leftWall = GameObject.Find("LeftAll").GetComponent<Transform>();
        rightWall = GameObject.Find("RightWall").GetComponent<Transform>();
    }

    void Start() {
        ball = GetComponent<Rigidbody2D>();
    }

    void Update() {
        ApplyForce();
        Walls();
        
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
            arrow.GetComponent<Image>().enabled = true;
            arrowWithForce.GetComponent<Image>().enabled = true;
        }
    }

    // Solta o clique da bola
    void OnMouseUp() {
        releaseRotation = false;
        arrow.GetComponent<Image>().enabled = false;
        Image arrowWithForceImg = arrowWithForce.GetComponent<Image>();
        arrowWithForceImg.enabled = false;
        if(!GameManager.instance.kicked && strength > 0) {
            releasekick = true;
            GameManager.instance.kicked = true;
            arrowWithForceImg.fillAmount = 0;
        }
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

    void die() {
        Destroy(this.gameObject);
        GameManager.instance.sceneBalls -= 1;
        GameManager.instance.qtdKicks -= 1;
    }

    void Walls() {
        if (this.gameObject.transform.position.x > rightWall.position.x ||
            this.gameObject.transform.position.x < leftWall.position.x) {
            this.die();
        }
    }

    private void OnTriggerEnter2D(Collider2D otherObject) {
        if (otherObject.gameObject.CompareTag("die")) {
            this.die();
        }
    }

    // Ao terminar a animação deixe a bola dinamica para a gravidade e força agir
    // Foi inserido como evento dentro da animação
    void DynamicBall() {
        ball.isKinematic = false;
    }
}

