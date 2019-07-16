using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimentação : MonoBehaviour
{
    public CharacterController2D controle;
    public SpriteRenderer sprite;
    public Animator animação;
    public int health;
    public float velocidade = 40f;
    float MovimentoHorizontal = 0f;
    bool jump = false;
    bool crouch = false;
    public bool invunerable = false;

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
        
    }

    public void OnLanding()
    {
        animação.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animação.SetBool("IsCrouching", isCrouching);
    }

    IEnumerator Damage()
    {
        yield return new WaitForSeconds(0.2f);
        animação.SetBool("TouchTheEnemy", false);

        for (float i = 0f; i<1f; i+= 0.1f)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        invunerable = false;
        


    }

    public void DamagePlayer()
    {
        invunerable = true;
        health--;
        StartCoroutine(Damage());
    }
}
