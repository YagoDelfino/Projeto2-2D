using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Cinemachine;

public class Movimentação : MonoBehaviour
{
    public CharacterController2D controle;
    public SpriteRenderer sprite;
    public Animator animação;
    public AIPath aiPath;
    public Transform inimigo;
    public Rigidbody2D rb;
    public Colisão colisão;

    public float velocidade = 40f;
    float MovimentoHorizontal = 0f;
    bool jump = false;
    bool crouch = false;

    public bool invunerable = false;
    public bool estaVivo = true;

    public Rigidbody2D moedas;
    public Transform moedaspawner;

    void Update()
    {
        MovimentoHorizontal = Input.GetAxisRaw("Horizontal") * velocidade;
        animação.SetFloat("Speed", Mathf.Abs(MovimentoHorizontal));

        if (Input.GetButtonDown("Jump") && !invunerable)
        {
            jump = true;
            animação.SetBool("IsJumping", true);
        }
        if(Input.GetButtonDown("Crouch") && !invunerable)
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch") && !invunerable)
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
            if(LevelManeger.levelManeger.GetMoedas() > 0)
            {

            int totalDeMoedas = LevelManeger.levelManeger.GetMoedas();
            if (totalDeMoedas >= 10)
            {
                totalDeMoedas = 10;
            }

            LevelManeger.levelManeger.ResetMoedas();

            for (int i = 0; i < totalDeMoedas; i++)
            {
                Rigidbody2D tempMoeda = Instantiate(moedas, moedaspawner.position, Quaternion.identity) as Rigidbody2D;
                int randomForceX = Random.Range(-10, 5);
                int randomForceY = Random.Range(1, 5);
                tempMoeda.AddForce(new Vector2(randomForceX, randomForceY), ForceMode2D.Impulse);
            }

            if (aiPath.desiredVelocity.x >= 0.01f)
                {
                    Vector2 direção = inimigo.TransformDirection(Vector2.right);
                    Vector2 força = direção * 50f;
                    rb.AddForce(força, ForceMode2D.Impulse);
                }
                else if (aiPath.desiredVelocity.x <= -0.01f)
                {
                    Vector2 direção = inimigo.TransformDirection(Vector2.left);
                    Vector2 força = direção * 40f;
                    rb.AddForce(força, ForceMode2D.Impulse);
                }

                yield return new WaitForSeconds(0.2f);
                animação.SetBool("TouchTheEnemy", false);

                for (float i = 0f; i < 1f; i += 0.1f)
                {
                    sprite.enabled = false;
                    yield return new WaitForSeconds(0.1f);
                    sprite.enabled = true;
                    yield return new WaitForSeconds(0.1f);
                }
                invunerable = false;
            }
            else
            {
                Morreu();
            }
   }

    public void DamagePlayer()
    {
        invunerable = true;
        StartCoroutine(Damage());
    }

    public void Morreu()
    {
            estaVivo = false;
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            animação.SetBool("TouchTheEnemy", true);
            velocidade = 0f;
            MovimentoHorizontal = 0f;
            jump = false;
            crouch = false;
            Invoke("GameOver", 1f);
            GetComponent<Movimentação>().enabled = false;
            
    }

    public void GameOver()
    {
       LevelManeger.levelManeger.GameOver();
    }
}
