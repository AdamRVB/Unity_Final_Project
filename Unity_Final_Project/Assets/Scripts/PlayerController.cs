using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour{

    Animator anim;
    public Text ScoreText;
    private int score;
    
    [SerializeField] private Text uiText;
    [SerializeField] private float mainTimer;
    private bool GameOver;
    private float timer;
    private bool canCount = true;
    private bool doOnce = false;
        

    private void Start()
    {
        GameOver = false;
        score = 0;
        anim = GetComponent<Animator>();
        timer = mainTimer;
    }

    public GameObject DisplayBox;
    public GameObject PassBox;
    public int QTEGen;
    public int WaitingForKey;
    public int CorrectKey;
    public int CountingDown;

    void FixedUpdate()
    {
        
        timer = timer + Time.deltaTime;
        if (timer >= 10)
        {
            
            StartCoroutine(ByeAfterDelay(2));

        }

    }

    IEnumerator ByeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);

        GameLoader.gameOn = false;
    }
    void SetCountText()
    {
        
        if (score == 3)
        {
            
            StartCoroutine(ByeAfterDelay(2));
        }
    }

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

        if (score == 3)
        {
            GameOver = true;
            StopCoroutine(CountDown());
            StopCoroutine(KeyPressing());
            anim.SetInteger("State", 2);
            

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
            GameLoader.AddScore(1);
            anim.SetInteger("State", 1);
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
            anim.SetInteger("State", 0);
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
            anim.SetInteger("State", 0);
            yield return new WaitForSeconds(1.0f);
            CorrectKey = 0;
            PassBox.GetComponent<Text>().text = "";
            DisplayBox.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(1.0f);
            WaitingForKey = 0;
            CountingDown = 1;
        }
    }
    public void gameOver()
    {
        
        GameOver = true;
    }
}
public class GameLoader : MonoBehaviour
{

    private static int index;
    public static int score;
    public static int timeLeft;
    public static int waitLeft;
    public static bool gameOn;

    public Text scoreText;
    public Text timerText;


    // Use this for initialization
    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        index = 0;
        score = 0;
        timeLeft = 12;
        gameOn = true;

        scoreText.text = "";
        timerText.text = "";

        StartCoroutine(CountDown());
        LoadNewGame();
        updateText();
    }

    
    void Update()
    {

        updateText();


        if (gameOn == false)
        {
            gameOn = true;
            LoadNewGame();

        }
    }

    private static IEnumerator CountDown()
    {
        while (gameOn == true)
        {
            timeLeft = timeLeft - 1; //Time.deltaTime;
            Debug.Log(timeLeft);

            if (timeLeft <= 0)
            {
                gameOn = false;
                LoadNewGame();
            }


            yield return new WaitForSeconds(1);
        }



    }

    public static void LoadNewGame()
    {
        // runs almost everytime
        if (index != 0)
        {
            SceneManager.UnloadSceneAsync(index);
            Debug.Log("Scene " + index + " Unloaded!");
            index = Random.Range(2, 5);

            timeLeft = 10;
            waitLeft = 3;
            gameOn = true;

            SceneManager.LoadScene(index, LoadSceneMode.Additive);
            Debug.Log("Scene " + index + " Loaded!");
        }
        //runs the first time
        else
        {
            index = Random.Range(2, 4);
            SceneManager.LoadScene(index, LoadSceneMode.Additive);
            Debug.Log("Scene " + index + " Loaded!");
        }

    }

    public static void AddScore(int newScoreValue)
    {
        score = score + newScoreValue;
        Debug.Log(score);
    }

    public void updateText()
    {
        int falseTimeLeft = timeLeft - 2;

        scoreText.text = "Score: " + score;
        if (falseTimeLeft >= 0)
        {
            timerText.text = "Time Left: " + falseTimeLeft; //timeLeft;
        }
        else timerText.text = "Time Left: 0";
    }

}
