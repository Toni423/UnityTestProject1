using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGetMoney : MonoBehaviour
{
    
    public void clickedOn() {
        GameObject.FindGameObjectWithTag("ShopManager").GetComponent<ShopManager>().buySth(-50);
    }

}
