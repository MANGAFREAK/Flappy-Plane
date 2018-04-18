using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody2D))]
public class GameControl : MonoBehaviour {
    public Rigidbody2D rb2d;
    public delegate void AfterStart();
    public static event AfterStart startmoving;
    public Text scoreText;
    public Text highScoreText;
    public static GameControl instance;
    public GameObject GameOverText1;
    public GameObject GameOverText2;
    public GameObject StartGameText1;
    public GameObject StartGameText2;
    public float scrollspeed = -1.5f;
    public bool GameOver = false;
    public bool Startgame = false;
    private int score = 0;
    private int highscore;
    // Use this for initialization
    void Awake ()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
    }
	
	 //Update is called once per frame
	void Update ()
    {
        if (GameOver == true && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if(Startgame == true && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void StartGame()
    {
        StartGameText1.SetActive(true);
        StartGameText2.SetActive(false);
        Startgame = true;
    }

    public void BirdScored()
    {
        //The bird can't score if the game is over.
        if (GameOver)
            return;
        //If the game is not over, increase the score...
        score++;
        //...and adjust the score text.
        scoreText.text = "Score: " + score.ToString();

        if (highscore < score)
        {
            highscore = score;
            highScoreText.text = "Best: " + highscore.ToString();
        }
    }

    public void BirdDied()
    {
        StartGameText1.SetActive(true);
        StartGameText2.SetActive(false);
        Startgame = true;
        GameOver = true;
    }
    public void Menu()
    {
        GameOverText1.SetActive(true);
        GameOverText2.SetActive(false);
        GameOver = true;
        rb2d.velocity = Vector2.zero;
        rb2d.simulated = false;
    }
}
