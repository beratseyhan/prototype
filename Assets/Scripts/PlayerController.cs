using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{

 
    
    Vector3 joyistickMove;

    [SerializeField] private DynamicJoystick dynamicJoystick;

    bool isFirstTouch = false;
    public static PlayerController instance;

    bool isGameFinisih = false;
    private void OnDisable()
    {
        EventSetter(false);
    }
    
   
    void Update()
    {

        //if (Input.GetKeyDown("space"))
        //{
        //    Debug.Log("space");
        //    score += 50;
        //}
        
            base.Movement(joyistickMove);
        
       
       
       
       
        if (isFirstTouch == false)
        {
            FirstTouchControl();
        }
    }
    public void LateUpdate()
    {
        if (isGameFinisih == false)
        {
            PlayerInput();

        }
        
    }

    public void FirstTouchControl()
    {
        if (Input.GetMouseButton(0))
        {
            EventSetter(true);
            isFirstTouch = true;
            EventManager.GameStartEvent();
        

        }


    }

    public void PlayerInput()
    {
        joyistickMove = new Vector3(dynamicJoystick.Horizontal, 0, dynamicJoystick.Vertical);
       
    }
    public void EndGame()
    {
        joyistickMove = Vector3.zero;
        isGameFinisih =true;
        EventSetter(false);
    }


    private void EventSetter(bool enable)
    {
        if (enable)
        {
            EventManager.OnLose += EndGame;
            EventManager.OnWin += EndGame;
        }
        else
        {
            EventManager.OnLose -= EndGame;
            EventManager.OnWin -= EndGame;
        }
    }

    public float  GetPlayerScale()
    {
        return this.transform.localScale.x;
    }

    public int GetPlayerScore()
    {
        return score;
    }

}
