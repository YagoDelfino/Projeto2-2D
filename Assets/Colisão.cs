using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisão : MonoBehaviour
{
    public Animator animação;
    public Movimentação player;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(player.invunerable == false)
            {
                animação.SetBool("TouchTheEnemy", true);
                player.DamagePlayer();
                
            }
                

        }
    }
}



