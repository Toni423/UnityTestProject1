using System.Collections;
using System.Collections.Generic;
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
    public GameObject lockImage;

    public int price;
    private ShopManager shopManager;


    private void Start() {
        shopManager = GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopManager>();

        priceBox = priceTag.GetComponent<Image>();
        unlocked = PlayerPrefs.GetInt(prefName, 0) == 1;


        if(unlocked) {
            unlock();
        }
    }

    public void clicked() {

        if(!unlocked) {
            buy();
            return;
        }



    }




    private void unlock() {
        priceBox.sprite = bought;
        priceText.SetActive(false);
        lockImage.SetActive(true);
        
    }

    private void buy() {

        if(PlayerPrefs.GetInt("Money", 0) >= price) {

            unlocked = true;
            PlayerPrefs.SetInt(prefName, 1);
            shopManager.buySth(price);
            unlock();

        }
    }   
}
