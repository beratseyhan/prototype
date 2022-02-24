using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject gameCanvas;
    GameObject inGameCanvas,menu;
    Text playerRankText,timeText,scoreText,firstCharacter;

    PlayerController playerController;
    public static UIManager instance;
    private void Awake()
    {

        if (instance == null )
        {
            instance = this;
        }

        playerController = FindObjectOfType<PlayerController>();

        EventSetter(true);
        ReferenceSetter();
    }
    public void ReferenceSetter()
    {
        menu= gameCanvas.transform.GetChild(2).gameObject;
        menu.transform.gameObject.SetActive(false);

        inGameCanvas = gameCanvas.transform.GetChild(1).gameObject;
        inGameCanvas.SetActive(false);
        playerRankText = inGameCanvas.transform.GetChild(0).GetComponent<Text>();
        scoreText = inGameCanvas.transform.GetChild(1).GetComponent<Text>();
        timeText = inGameCanvas.transform.GetChild(2).GetComponent<Text>();
        firstCharacter= inGameCanvas.transform.GetChild(3).GetComponent<Text>();
    }
    private void OnDisable()
    {
        EventSetter(false);
    }
    void Start()
    {
        
    }

  
    void Update()
    {
        playerRankText.text = "rank : " + GameManager.instance.rank;
        DisplayScore();
      //   DisplayScore();
    }
    public void BeginProsses()
    {
        inGameCanvas.SetActive(true);
    }
   

    public void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void DisplayScore()
    {
        scoreText.text="Score :" + playerController.GetPlayerScore().ToString();

       
    }
    public void FirstCharacter(string firstCharacterName)
    {
        firstCharacter.text = "1."+ firstCharacterName;
    }
    public void playAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void PlayerWin()
    {
        menu.transform.gameObject.SetActive(true);
        menu.transform.GetChild(0).transform.GetChild(0).transform.gameObject.SetActive(true);
       
    }
    public void PlayerLose()
    {
        menu.transform.gameObject.SetActive(true);
        menu.transform.GetChild(0).transform.GetChild(1).transform.gameObject.SetActive(true);
    }
    private void EventSetter(bool enable)
    {
        if (enable)
        {

            EventManager.OnGameStart += BeginProsses;
            EventManager.OnLose += PlayerLose;
            EventManager.OnWin += PlayerWin;
        }
        else
        {

            EventManager.OnGameStart -= BeginProsses;
            EventManager.OnLose -= PlayerLose;
            EventManager.OnWin -= PlayerWin;
        }
    }
}
