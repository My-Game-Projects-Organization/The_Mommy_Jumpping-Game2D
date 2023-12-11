using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public float moveSpeed;
    float dirX;
    // check platform đã nhảy lên
    private Platform m_platformLanded;
    private float m_movingLimitX;

    private Rigidbody2D m_rb;

    public Platform PlatformLanded { get => m_platformLanded; set => m_platformLanded = value; }
    public float MovingLimitX { get => m_movingLimitX; }

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }

    // liên quan tới vật lý -> gọi trong fixupdate
    private void FixedUpdate()
    {
        MovingHandle();
    }

    public void Jump()
    {
        if (!GameManager.Ins || GameManager.Ins.state != GameState.Playing) return;

        if (!m_rb || m_rb.velocity.y > 0 || !m_platformLanded) return;

        if(m_platformLanded is BreakablePlatform)
        {
            m_platformLanded.PlatformAction();
        }
        if (AudioController.Ins)
            AudioController.Ins.PlaySound(AudioController.Ins.jump);

        m_rb.velocity = new Vector2(m_rb.velocity.x, jumpForce);
    }
    private void MovingHandle()
    {
        if (!GamePadController.Ins || !m_rb || !GameManager.Ins || GameManager.Ins.state != GameState.Playing) return;

        if (SettingControlType.Ins.checkType == 1)
        {
            if (GamePadController.Ins.CanMoveLeft)
            {
                m_rb.velocity = new Vector2(-moveSpeed, m_rb.velocity.y);
            }
            else if (GamePadController.Ins.CanMoveRight)
            {
                m_rb.velocity = new Vector2(moveSpeed, m_rb.velocity.y);
            }
            else
            {
                m_rb.velocity = new Vector2(0, m_rb.velocity.y);
            }
        }else if(SettingControlType.Ins.checkType == 2)
        {
            dirX = Input.acceleration.x * moveSpeed;
            m_rb.velocity = new Vector2(dirX,m_rb.velocity.y);
        }

        m_movingLimitX = Helper.Get2DCamSize().x / 2;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -m_movingLimitX, m_movingLimitX),
            transform.position.y, transform.position.z
            );
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(GameTag.Collectable.ToString()))
        {
            var collectable = col.GetComponent<Collectable>();
            if(collectable != null)
            {
                collectable.Trigger();
            }
        }
    }
}
