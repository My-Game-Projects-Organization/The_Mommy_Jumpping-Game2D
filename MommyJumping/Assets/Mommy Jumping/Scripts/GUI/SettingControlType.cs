using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SettingControlType : Singleton<SettingControlType>
{
    public Button BtnType1;
    public Button BtnType2;
    public Image CheckType1;
    public Image CheckType2;

    public int checkType = 1;

    public override void Awake()
    {
        MakeSingleton(false);
    }

    public override void Start()
    {
        LoadTypeControl();
    }

    public void LoadTypeControl()
    {
        //if (Pref.checkType == null) checkType = 1;
        checkType = Pref.checkType;
        if(checkType == 1)
        {
            SetStateBtnType(true);
        }else if(checkType == 2)
        {
            SetStateBtnType(false);
        }
    }

    public void SetStateBtnType(bool isCheck)
    {
        BtnType1.enabled = !isCheck;
        BtnType2.enabled = isCheck;
        CheckType1.gameObject.SetActive(isCheck);
        CheckType2.gameObject.SetActive(!isCheck);
    }

    public void CheckGameType1()
    {
        if(BtnType1 && CheckType1.gameObject.activeInHierarchy == false)
        {
            checkType = 1;
            Pref.checkType = 1;
            SetStateBtnType(true);
        }
    }
    public void CheckGameType2()
    {
        if (BtnType2 && CheckType2.gameObject.activeInHierarchy == false)
        {
            Pref.checkType = 2;
            checkType = 2;
            SetStateBtnType(false);
        }
    }
}
