using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentação : MonoBehaviour
{
    public CharacterController2D controle;
    public Animator animação;
    public float velocidade = 40f;
    float MovimentoHorizontal = 0f;
    bool jump = false;
    bool crouch = false;

    void Update()
    {
        MovimentoHorizontal = Input.GetAxisRaw("Horizontal") * velocidade;
        animação.SetFloat("Speed", Mathf.Abs(MovimentoHorizontal));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animação.SetBool("IsJumping", true);
        }
        if(Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    private void FixedUpdate()
    {
        controle.Move(MovimentoHorizontal * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        crouch = false;
    }
}
