using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class UIShopManager: MonoBehaviour {

    [SerializeField] private Text coinsText;
    public static UIShopManager instance;
    public List<ShopBallModel> balls = new List<ShopBallModel>();
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

            if (ball.bought) {
                controll.ballSprite.sprite = Resources.Load<Sprite>($"Balls/{ball.spriteName}");
                controll.released.SetActive(true);
                controll.priceContainer.SetActive(false);
                controll.closed.SetActive(false);
            } else if (!ball.enabled) {
                controll.ballSprite.sprite = Resources.Load<Sprite>($"Balls/{ball.spriteName}_cinza");
                button.GetComponent<Button>().interactable = false;
                controll.closed.SetActive(true);
                controll.priceContainer.SetActive(false);
                controll.released.SetActive(false);
            } else {
                controll.closed.SetActive(false);
                controll.released.SetActive(false);
                controll.priceContainer.SetActive(true);
            }
        }
    }
}

