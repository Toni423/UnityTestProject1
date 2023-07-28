using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopLevelBuy : MonoBehaviour
{

    private int level;
    
    public Image greenBox;

    public GameObject priceBox;
    private TextMeshProUGUI priceTag;

    private ShopManager shopManager;

    public GameObject lockImage;

    public int price;

    public string prefName;
    

    private void Start() {
        level = PlayerPrefs.GetInt(prefName, 0);

        price *= (int) Mathf.Pow(2f, level);

        priceTag = priceBox.GetComponent<TextMeshProUGUI>();
        priceTag.SetText("" + price);

        shopManager = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopManager>();

        unlock();
    }

    private void unlock() {
        greenBox.GetComponent<Image>().fillAmount = level / 3f;
        if(level > 2) {
            priceBox.SetActive(false);
            lockImage.SetActive(true);
        }
    }

    public void clicked() {
        if(level < 3) {
            buy();
        }
    }

    private void buy() {
        if(PlayerPrefs.GetInt("Money", 0) >= price) {

            PlayerPrefs.SetInt(prefName, (PlayerPrefs.GetInt(prefName, 0) + 1));
            level++;
            shopManager.buySth(price);
            unlock();
            price *= 2;
            priceTag.SetText("" + price);
        }
    }
}
