using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    private Rigidbody2D Rd;
    private Animator Anim;


    public Collider2D Coll;
    public LayerMask Groud;

    public float MoveSpeech = 10f;
    public float JumpSpeech = 10f;

    private readonly string Str_Horizontal = "Horizontal";
    private readonly string Anim_Str_jumpping = "jumping";
    private readonly string Anim_Str_running = "running";
    private readonly string Anim_Str_falling = "falling";
    private readonly string Anim_Str_idel = "idel";



    // Start is called before the first frame update
    void Start()
    {
        this.Rd = GetComponent<Rigidbody2D>();
        this.Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        TurnAround();
        Move();
        SwichAnim();

    }


    private void TurnAround()
    {
        float horizontalFaceDirect = Input.GetAxisRaw(this.Str_Horizontal);

        if (horizontalFaceDirect == -1 || horizontalFaceDirect == 1)
        {
            this.transform.localScale = new Vector3(horizontalFaceDirect, 1, 1);
        }

        //更新动画
        this.Anim.SetFloat(this.Anim_Str_running, Math.Abs(horizontalFaceDirect));
    }

    void Move()
    {
        float horizontal = Input.GetAxis(this.Str_Horizontal);
        if (horizontal != 0)
        {
            this.Rd.velocity = new Vector2(horizontal * this.MoveSpeech * Time.deltaTime, this.Rd.velocity.y);
        }

        bool jumpClick = Input.GetButtonDown("Jump");
        if (jumpClick == true)
        {
            this.Rd.velocity = new Vector2(this.Rd.velocity.x, this.JumpSpeech * Time.deltaTime);
            this.Anim.SetBool(this.Anim_Str_jumpping, true);
            this.Anim.SetBool(this.Anim_Str_idel, false);
        }

    }

    private void SwichAnim()
    {
        bool jumping = this.Anim.GetBool(this.Anim_Str_jumpping);
        if (jumping == true)
        {
            if (this.Rd.velocity.y < 0)
            {
                this.Anim.SetBool(this.Anim_Str_jumpping, false);
                this.Anim.SetBool(this.Anim_Str_falling, true);
            }
        }
        else if (this.Coll.IsTouchingLayers(this.Groud))
        {
            this.Anim.SetBool(this.Anim_Str_falling, false);
            this.Anim.SetBool(this.Anim_Str_idel, true);
        }

    }



}
