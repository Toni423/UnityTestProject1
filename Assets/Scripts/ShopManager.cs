using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour {

    public GameObject moneyField;
    private TextMeshProUGUI moneyText;


    // Start is called before the first frame update
    void Start() {

        moneyText = moneyField.GetComponent<TextMeshProUGUI>();
        moneyText.SetText("" + PlayerPrefs.GetInt("Money", 0));
    }

    public void buySth(int price) {
        int newMoney = (PlayerPrefs.GetInt("Money", 0)) - price;
        PlayerPrefs.SetInt("Money", newMoney);
        moneyText.SetText("" + newMoney);
    }

}
