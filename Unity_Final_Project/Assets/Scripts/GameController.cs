using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

 public class GameController : MonoBehaviour {

    public Text ScoreText;
    private int score;
    Animator anim;

    private void Start()
    {
        score = 0;
        anim = GetComponent<Animator>();
    }





    public GameObject DisplayBox;
    public GameObject PassBox;
    public int QTEGen;
    public int WaitingForKey;
    public int CorrectKey;
    public int CountingDown;

    private void Update()
    {
        ScoreText.text = "Score: " + score;
        if (WaitingForKey == 0)
        {
            QTEGen = Random.Range(1, 4);
            CountingDown = 1;
            StartCoroutine(CountDown());
            if (QTEGen == 1)
            {
                WaitingForKey = 1;
                DisplayBox.GetComponent<Text>().text = "[U]";
            }
            if (QTEGen == 2)
            {
                WaitingForKey = 1;
                DisplayBox.GetComponent<Text>().text = "[I]";
            }
            if (QTEGen == 3)
            {
                WaitingForKey = 1;
                DisplayBox.GetComponent<Text>().text = "[O]";
            }
        }

        if (QTEGen == 1)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("UKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());

                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }

        if (QTEGen == 2)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("IKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());

                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }

        if (QTEGen == 3)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("OKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());

                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
    }

    IEnumerator KeyPressing()
    {
        QTEGen = 4;
        if (CorrectKey == 1)
        {
            CountingDown = 2;
            PassBox.GetComponent<Text>().text = "GREAT";
            score++;

            yield return new WaitForSeconds(1.0f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            DisplayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.0f);
            WaitingForKey = 0;
            CountingDown = 1;
            
        }
        if (CorrectKey == 2)
        {
            CountingDown = 2;
            PassBox.GetComponent<Text>().text = "WRONG";
            yield return new WaitForSeconds(1.0f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            DisplayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.0f);
            WaitingForKey = 0;
            CountingDown = 1;
        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1.0f);
        if (CountingDown == 1)
        {
            QTEGen = 4;
            CountingDown = 2;
            PassBox.GetComponent<Text>().text = "Too Slow";
            yield return new WaitForSeconds(1.0f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            DisplayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.0f);
            WaitingForKey = 0;
            CountingDown = 1;
        }
    }

}
