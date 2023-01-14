using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIShopManager: MonoBehaviour {

    [SerializeField] private Text coinsText;
    public static UIShopManager instance;
    public List<ShopBallModel> balls = new List<ShopBallModel>();
    public List<ShopBallController> controllerBalls = new List<ShopBallController>();
    public GameObject buttonBall;
    public Transform panelGrid;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {
        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
        FillBallList();
    }

    void FixedUpdate() {
        coinsText.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void GoToLevelScene() {
        int shopIndex = SceneUtility.GetBuildIndexByScenePath("Scenes/Stages Menu");
        SceneManager.LoadScene(shopIndex);
    }

    void FillBallList() {
        foreach (ShopBallModel ball in balls) {
            GameObject button = Instantiate(buttonBall);
            button.transform.SetParent(panelGrid, false);
            ShopBallController controll = button.GetComponent<ShopBallController>();
            controll.ballId = ball.id;
            controll.priceContainer.GetComponentInChildren<Text>().text = ball.price.ToString();
            controllerBalls.Add(controll);

            if(ball.bought) {
                controll.ballSprite.sprite = Resources.Load<Sprite>($"Balls/{ball.spriteName}");
                controll.released.SetActive(true);
                controll.priceContainer.SetActive(false);
                controll.closed.SetActive(false);
            } else {
                controll.ballSprite.sprite = Resources.Load<Sprite>($"Balls/{ball.spriteName}_cinza");
                controll.released.SetActive(false);
                if(ball.enabled) {
                    controll.priceContainer.SetActive(true);
                    controll.closed.SetActive(false);
                } else {
                    button.GetComponent<Button>().interactable = false;
                    controll.closed.SetActive(true);
                    controll.priceContainer.SetActive(false);
                }
            }
        }
    }
}

