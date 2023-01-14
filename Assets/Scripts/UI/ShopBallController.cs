using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBallController: MonoBehaviour {
    public int ballId;
    public Image ballSprite;
    public GameObject priceContainer;
    public GameObject released;
    public GameObject closed;
    private ShopBallController lastBallControllerUsed;
    private ShopBallModel lastBallUsed;

    public void BuyBall() {
        List<ShopBallModel> balls = UIShopManager.instance.balls;
        int coins = PlayerPrefs.GetInt("Coins");
        bool selectedBall = false;
        for(int i = 0; i < balls.Count; i++) {
            // Atualizando a bola selecionada
            if(balls[i].id == ballId) {
                if(balls[i].bought) {
                    this.ChangeBallStatus(balls[i]);
                    selectedBall = true;
                } else if(coins >= balls[i].price) {
                    this.ChangeBallStatus(balls[i]);
                    selectedBall = true;
                    ballSprite.sprite = Resources.Load<Sprite>($"Balls/{balls[i].spriteName}");
                    ScoreManager.instance.LoseCoins(balls[i].price);
                    balls[i].bought = true;
                }
            } else {
                this.GetLastBallUsed(balls[i]);
            }
        }

        if (selectedBall && this.lastBallUsed is not null) {
            this.ChangeLastUsedBall();
        }
    }

    void ChangeBallStatus(ShopBallModel ball) {
        released.SetActive(true);
        priceContainer.SetActive(false);
        closed.SetActive(false);
        ball.use = true;
    }

    void ChangeLastUsedBall() {
        this.lastBallControllerUsed.priceContainer.GetComponentInChildren<Text>().text = "Usar";
        this.lastBallUsed.use = false;
        this.lastBallControllerUsed.released.SetActive(false);
        this.lastBallControllerUsed.priceContainer.SetActive(true);
        this.lastBallControllerUsed.closed.SetActive(false);
    }

    void GetLastBallUsed(ShopBallModel ball) {
        if(ball.bought && ball.use) {
            foreach(ShopBallController controller in UIShopManager.instance.controllerBalls) {
                if (controller.ballId == ball.id) {
                    this.lastBallControllerUsed = controller;
                    this.lastBallUsed = ball;
                }
            }
        }
    }
}

