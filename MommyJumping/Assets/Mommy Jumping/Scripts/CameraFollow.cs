using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UDEV.EndlessGame
{
    public class CameraFollow : MonoBehaviour
    {
        public static CameraFollow Ins;

        public Transform target_char_normal;
        public Transform target_char_special;
        public Vector3 offset;
        [Range(1, 10)]
        public float smoothFactor;

        private void Awake()
        {
            Ins = this;
        }

        private void FixedUpdate()
        {
            Follow();
        }

        private void Follow()
        {
            
            if(Pref.SelectedCharacterId == 0)
            {
                if (GameManager.Ins && GameManager.Ins.player != null)
                    target_char_normal = GameManager.Ins.player.GetComponent<Transform>();        
                if (target_char_normal == null || target_char_normal.transform.position.y < transform.position.y) return;
            }
            else
            {
                if (GameManager.Ins && GameManager.Ins.player_special != null)
                    target_char_special = GameManager.Ins.player_special.GetComponent<Transform>();
                if (target_char_special == null || target_char_special.transform.position.y < transform.position.y) return;
            }

            Vector3 targetPos = (Pref.SelectedCharacterId == 0) 
                ? (new Vector3(0, target_char_normal.transform.position.y, 0f) + offset) 
                : (new Vector3(0, target_char_special.transform.position.y, 0f) + offset);

            Vector3 smoothedPos = Vector3.Lerp(transform.position, targetPos, smoothFactor * Time.deltaTime);
            transform.position = new Vector3(
                Mathf.Clamp(smoothedPos.x, 0, smoothedPos.x),
                Mathf.Clamp(smoothedPos.y, 0, smoothedPos.y),
                -10f);
        }
    }
}
