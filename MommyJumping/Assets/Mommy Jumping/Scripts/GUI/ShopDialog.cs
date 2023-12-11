using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopDialog : Dialog
{
    public Transform skinCharacter;
    public Image itemUIPb;

    public int selectedCharacterId;
    public int viewCharacterId;
    public Text stateText;
    public Button btnUnlock;

    public override void Close()
    {
        base.Close();
        //if (GameManager.Ins.player != null && GameManager.Ins.player_special != null)
        //{
        //    if (Pref.SelectedCharacterId == 0)
        //    {
        //        GameManager.Ins.player_special.gameObject.SetActive(false);
        //        GameManager.Ins.player.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        GameManager.Ins.player_special.gameObject.SetActive(true);
        //        GameManager.Ins.player.gameObject.SetActive(false);
        //        GameManager.Ins.player_special.GetComponent<SpriteRenderer>().sprite = ShopManager.Ins.items[Pref.SelectedCharacterId].hud;
        //    }

        //}
    }

    public override void Show(bool isShow)
    {
        base.Show(isShow);
        SetActiveItemShop();
        UpdateUI();
    }

    private void SetActiveItemShop()
    {
        selectedCharacterId = Pref.SelectedCharacterId;
        viewCharacterId = Pref.SelectedCharacterId;

        if (skinCharacter == null || skinCharacter.childCount <= 0) return;

        for(int i = 0; i < skinCharacter.childCount; i++)
        {
            skinCharacter.GetChild(i).gameObject.SetActive(false);
        }

        skinCharacter.GetChild(Pref.SelectedCharacterId).gameObject.SetActive(true);
    }

    private void Awake()
    {
        if (Pref.numberOfCoins < 10000)
        {
            Pref.numberOfCoins = 10000;
            GUIManager.Ins.UpdateCoinTotal();
        }
        var items = ShopManager.Ins.items;
        if (items == null || items.Length <= 0) return;

        selectedCharacterId = Pref.SelectedCharacterId;
        viewCharacterId = Pref.SelectedCharacterId;

        ClearChilds();

        for (int i = 0; i < items.Length; i++)
        {
            var item = items[i];

            if (item != null)
            {
                itemUIPb.sprite = item.hud;
                var itemUIClone = Instantiate(itemUIPb, Vector3.zero, Quaternion.identity);

                itemUIClone.transform.SetParent(skinCharacter);
                itemUIClone.transform.localPosition = Vector3.zero;

                itemUIClone.transform.localScale = Vector3.one;

                itemUIClone.gameObject.SetActive(false);

            }
        }

        skinCharacter.GetChild(selectedCharacterId).gameObject.SetActive(true);
        stateText.text = Pref.GetBool(PrefKey.Player_.ToString() + selectedCharacterId) == true ? "Unlock" : "Lock";
        UpdateUI();
    }

    private void UpdateUI()
    {
        selectedCharacterId = Pref.SelectedCharacterId;
        bool isUnlocked = Pref.GetBool(PrefKey.Player_.ToString() + viewCharacterId);

        if (isUnlocked)
        {
            if (viewCharacterId == selectedCharacterId)
            {
                btnUnlock.interactable = false;
                btnUnlock.GetComponentInChildren<Text>().text = "Equipped";
                stateText.text = "Unlock";
            }
            else
            {
                btnUnlock.interactable = true;
                btnUnlock.GetComponentInChildren<Text>().text = "Select";
                stateText.text = "Unlock";
            }
        }
        else
        {
            int price = ShopManager.Ins.items[viewCharacterId].price;
            btnUnlock.GetComponentInChildren<Text>().text = "Price: " + price;
            stateText.text = "Lock";
            if (Pref.numberOfCoins < price)
            {
                btnUnlock.gameObject.SetActive(true);
                btnUnlock.interactable = false;
            }
            else
            {
                btnUnlock.gameObject.SetActive(true);
                btnUnlock.interactable = true;
            }
        }
    }

    public void Unlock()
    {
        selectedCharacterId = Pref.SelectedCharacterId;
        if (selectedCharacterId != viewCharacterId && Pref.GetBool(PrefKey.Player_.ToString() + viewCharacterId) == true)
        {
            Pref.SelectedCharacterId = viewCharacterId;
            Pref.ViewCharacterId = viewCharacterId;
            btnUnlock.GetComponentInChildren<Text>().text = "Equipped";
        }
        else
        {
            int coins = Pref.numberOfCoins;
            int price = ShopManager.Ins.items[viewCharacterId].price;
            Pref.numberOfCoins = coins - price;

            Pref.SetBool(PrefKey.Player_.ToString() + viewCharacterId, true);
            Pref.SelectedCharacterId = viewCharacterId;
            Pref.ViewCharacterId = viewCharacterId;
            GUIManager.Ins.UpdateCoinTotal();
        }

        UpdateUI();
    }

    public void NextCharacter()
    {
        skinCharacter.GetChild(viewCharacterId).gameObject.SetActive(false);
        viewCharacterId++;

        if (viewCharacterId == skinCharacter.childCount)
        {
            viewCharacterId = 0;
        }
        skinCharacter.GetChild(viewCharacterId).gameObject.SetActive(true);
        //if (characters[viewCharacterId].isUnlocked)
        //    PlayerPrefs.SetInt("selectedCharacterId", selectedCharacterId);
        UpdateUI();
    }

    public void PreviousCharacter()
    {
        skinCharacter.GetChild(viewCharacterId).gameObject.SetActive(false);
        viewCharacterId--;

        if (viewCharacterId == -1)
        {
            viewCharacterId = skinCharacter.childCount - 1;
        }
        skinCharacter.GetChild(viewCharacterId).gameObject.SetActive(true);
        //if(characters[selectedCharacterId].isUnlocked)
        //    PlayerPrefs.SetInt("selectedCharacterId", selectedCharacterId);
        UpdateUI();
    }

    private void ClearChilds()
    {
        if (!skinCharacter || skinCharacter.childCount <= 0) return;

        for(int i=0; i < skinCharacter.childCount; i++)
        {
            var child = skinCharacter.GetChild(i);
            if (child != null)
                Destroy(child.gameObject);
        }
    }
}
