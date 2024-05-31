using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_Action : MonoBehaviour
{
    public float Speed;
    float h;
    float v;
    int lastH;
    int lastV;
    Rigidbody2D rigid;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Animator 컴포넌트를 찾을 수 없습니다!");
        }

        // 초기값 설정
        lastH = 0;
        lastV = -1; // 아래 방향을 초기값으로 설정합니다.
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (anim.GetInteger("hAxisRaw") != (int)h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
        }
        else if (anim.GetInteger("vAxisRaw") != (int)v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            anim.SetBool("isChange", false);
        }

        // 이전 방향 기록
        if (h != 0 || v != 0)
        {
            lastH = (int)h;
            lastV = (int)v;
        }
    }

    void FixedUpdate()
    {
        Vector2 moveVec = new Vector2(h, v);
        rigid.velocity = moveVec * Speed;

        // 캐릭터가 이동하지 않을 때 마지막 방향 유지
        if (rigid.velocity.magnitude == 0)
        {
            anim.SetInteger("lastH", lastH);
            anim.SetInteger("lastV", lastV);
        }
    }
}
