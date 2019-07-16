using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManeger : MonoBehaviour
{
    public static LevelManeger levelManeger;
    private int moedasAtual = 0;
    private bool gameOver = false;

    private float segundos;
    private int segundosToInt;
    private int minutos;

    public Text minutosText;
    public Text segundosText;
    public Text Moedas;

    public GameObject GameOverText;

    void Awake()
    {
        if(levelManeger == null)
        {
            levelManeger = this;
        }
        else if(levelManeger != this)
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
        {
            segundosToInt = (int)segundos;
            segundosText.text = segundosToInt.ToString();

            segundos += Time.deltaTime;

            if (segundos < 10)
            {
                segundosText.text = "0" + segundosToInt.ToString();
            } else if (segundos >= 60)
            {
                segundos = 0;
                minutos++;
                minutosText.text = minutos.ToString();

            }
            
        }
        if(gameOver && Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void SetMoedas()
    {
        moedasAtual++;
        Moedas.text = moedasAtual.ToString();
    }

    public int GetMoedas()
    {
        return moedasAtual;
    }

    public void ResetMoedas()
    {
        moedasAtual = 0;
        Moedas.text = moedasAtual.ToString();
    }

    public void GameOver()
    {
        gameOver = true;
        GameOverText.SetActive(true);
    }
}
