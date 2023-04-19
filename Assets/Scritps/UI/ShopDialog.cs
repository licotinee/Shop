using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDialog : Dialog
{
    public Transform gridRoot;
    public ShopItemUI itemUIPb;
    public override void Show(bool isShow)
    {
        base.Show(isShow);
        UpdateUI();
    }
    private void UpdateUI()
    {
        var items = ShopManager.Ins.items;
        if (items == null || items.Length <= 0 || !gridRoot || !itemUIPb) return;
        ClearChilds();
        for (int i = 0; i < items.Length; i++)
        {
            int idx = 1;
            var item = items[i];
            if(item != null)
            {
                var itemUIclone = Instantiate(itemUIPb, Vector3.zero, Quaternion.identity);
                itemUIclone.transform.SetParent(gridRoot);
                itemUIclone.transform.localPosition = Vector3.zero;
                itemUIclone.transform.localScale = Vector3.one;
                itemUIclone.UpdateUI(item, idx);
                if (itemUIclone.btn)
                {
                    itemUIclone.btn.onClick.RemoveAllListeners();
                    itemUIclone.btn.onClick.AddListener(() => ItemEvent(item, idx));
                    
                }
            }
        }
    }

    void ItemEvent(ShopItem item, int shopItemId)
    {
        if (item == null) return;
        bool isUnlocked = Pref.GetBool(PrefConst.PLAYER_PEFIX + shopItemId);
        if (isUnlocked)
        {
            if (shopItemId == Pref.CurPlayerId) return;
            Pref.CurPlayerId = shopItemId;
            UpdateUI();
        }
        else
        {
            if(Pref.Coins >= item.price)
            {
                Pref.Coins -= item.price;
                Pref.SetBool(PrefConst.PLAYER_PEFIX + shopItemId, true);
                Pref.CurPlayerId = shopItemId;
                UpdateUI();
            }
            else
            {
                Debug.Log("don't enough coins");
            }
        }
    }

    public void ClearChilds()
    {
        if (!gridRoot || gridRoot.childCount <= 0) return;
        for (int i = 0; i < gridRoot.childCount; i++)
        {
            var child = gridRoot.GetChild(i);
            if (child)
            {
                Destroy(child.gameObject);
            }
        }
    }

}

