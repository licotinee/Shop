using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopItemUI : MonoBehaviour
{
    public Text priceText;
    public Image hud;
    public Button btn;

    public void UpdateUI(ShopItem item, int shopItemId)
    {
        if (item == null) return;
        if (hud)
        {
            hud.sprite = item.hud;
        }
        bool isUnlock = Pref.GetBool(PrefConst.PLAYER_PEFIX + shopItemId);
        if (isUnlock)
        {
            if(shopItemId == Pref.CurPlayerId)
            {
                if (priceText)
                    priceText.text = "Active";
            }
            else
            {
                if (priceText)
                    priceText.text = "Owned";
            }
        }
        else
        {
            if (priceText)
                priceText.text = item.price.ToString();
        }
    }
}
