using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public Sprite bought;

    private bool unlocked;

    public string prefName;

    public GameObject priceTag;
    private Image priceBox;
    public GameObject priceText;
    public GameObject lockImageClosed;
    public GameObject lockImageOpen;

    public int price;
    private ShopManager shopManager;


    private void Start() {
        shopManager = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopManager>();

        priceText.GetComponent<TextMeshProUGUI>().SetText("" + price);

        priceBox = priceTag.GetComponent<Image>();
        unlocked = PlayerPrefs.GetInt(prefName, 0) != 0;


        if(unlocked) {
            unlock();
        }
    }

    public void clicked() {

        if(!unlocked) {
            buy();
            return;
        }

        PlayerPrefs.SetInt(prefName + "IsActive", PlayerPrefs.GetInt(prefName + "IsActive", 0) == 0 ? 1 : 0);
        lockImageClosed.SetActive(PlayerPrefs.GetInt(prefName + "IsActive", 0) == 1);
        lockImageOpen.SetActive(PlayerPrefs.GetInt(prefName + "IsActive", 0) == 0);
    }




    private void unlock() {
        priceBox.sprite = bought;
        priceText.SetActive(false);
        lockImageClosed.SetActive(PlayerPrefs.GetInt(prefName + "IsActive", 0) == 1);
        lockImageOpen.SetActive(PlayerPrefs.GetInt(prefName + "IsActive", 0) == 0);
    }

    private void buy() {

        if(PlayerPrefs.GetInt("Money", 0) >= price) {

            unlocked = true;
            PlayerPrefs.SetInt(prefName, 1);
            PlayerPrefs.SetInt(prefName + "IsActive", 1);
            shopManager.buySth(price);
            unlock();

        }
    }   
}
