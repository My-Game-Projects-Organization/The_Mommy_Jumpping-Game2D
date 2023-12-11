using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopItemUI : MonoBehaviour
{
    public Text priceText;
    public Image hud;
    public Button btn;

    public void UpdateUI(ShopItems item, int shopItemId)
    {
        if (item == null) return;

        if (hud)
            hud.sprite = item.hud;

        bool isUnlocked = Pref.GetBool(PrefKey.Player_.ToString() + shopItemId);
        if (isUnlocked)
        {
            if (shopItemId == Pref.CurPlayerId)
            {
                if (priceText)
                    priceText.text = "Active";
            }
            else
            {
                if (priceText)
                    priceText.text = "OWNED";
            }
        }
        else
        {
            if (priceText)
                priceText.text = item.price.ToString();
        }
        
    }
}
