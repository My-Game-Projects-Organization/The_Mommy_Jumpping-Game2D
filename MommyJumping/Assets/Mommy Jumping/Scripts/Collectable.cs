using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int scoreBonus;
    public bool isSpecial;
    public GameObject explosionEffPb;

    public void Trigger()
    {
        if(explosionEffPb != null)
        {
            Instantiate(explosionEffPb, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);

        if (GameManager.Ins && !isSpecial)
        {
            GameManager.Ins.AddScore(scoreBonus);
        }else if(GameManager.Ins && isSpecial)
        {
            GameManager.Ins.AddCoin();
        } 

        if (AudioController.Ins)
            AudioController.Ins.PlaySound(AudioController.Ins.gotCollectable);
    }
}
