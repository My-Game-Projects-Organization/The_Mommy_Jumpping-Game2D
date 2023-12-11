using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecking : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        // ko va chạm vs platform 
        if (!col.gameObject.CompareTag(GameTag.Platform.ToString())) return;

        var platformLanded = col.gameObject.GetComponent<Platform>();

        // check tồn tại củac cac đối tượng
        if (!GameManager.Ins || (!GameManager.Ins.player && !GameManager.Ins.player_special) || !platformLanded) return;

        if(Pref.SelectedCharacterId == 0)
        {
            GameManager.Ins.player.PlatformLanded = platformLanded;
            GameManager.Ins.player.Jump();
        }
        else
        {
            GameManager.Ins.player_special.PlatformLanded = platformLanded;
            GameManager.Ins.player_special.Jump();
        }


        if (!GameManager.Ins.IsPlatformLanded(platformLanded.Id))
        {
            int randScore = Random.Range(3, 8);
            GameManager.Ins.AddScore(randScore);
            GameManager.Ins.PlatformLandedIds.Add(platformLanded.Id);
        }
    }
}
