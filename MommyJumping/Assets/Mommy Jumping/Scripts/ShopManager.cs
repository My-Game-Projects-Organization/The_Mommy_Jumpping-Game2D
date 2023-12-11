using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    public ShopItems[] items;

    public override void Start()
    {
        if (items == null || items.Length <= 0) return;
        
        for(int i=0; i<items.Length; i++)
        {
            var item = items[i];

            if(item!= null)
            {
                if (i == 0)
                {
                    Pref.SetBool(PrefKey.Player_.ToString() + i, true);
                }
                else
                {
                    if(!PlayerPrefs.HasKey(PrefKey.Player_.ToString() + i))
                    {
                        Pref.SetBool(PrefKey.Player_.ToString() + i, false);
                    }
                }
            }
        } 
    }
}

[System.Serializable]
public class ShopItems
{
    public int price;
    public Sprite hud;
}