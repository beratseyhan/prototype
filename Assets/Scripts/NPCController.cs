using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConstantContainer;

public class NPCController : CharacterController
{
    public NPCTarget targetType;
    public NPCTarget randomTarget;
    float lowestDistance = Mathf.Infinity;
    float nextLowestDistance = Mathf.Infinity;
    [SerializeField] GameObject nearestObject = null;
    [SerializeField] GameObject secondObject = null;
    
    bool playerİsTouched = false;
    bool endGame = false;
    bool setFirstTarget = false;
    Vector3 fI=Vector3.zero;
    Vector3 npccJoyistickPosition = Vector3.zero;


    private void Awake()
    {
        EventSetter(true);
        base.Awake();
    }

    void Start()
    {
        base.Start();
   
    }

    
    void Update()
    {
        base.Movement(- fI);
       
        if (playerİsTouched == true && endGame==false)
        {

            ControlTargetType();
            if (nearestObject.activeInHierarchy == true)
            {
                ControlTargetPosition();
            }
           else
            {
                lowestDistance = Mathf.Infinity;
                nextLowestDistance = Mathf.Infinity;
                nearestObject = null;
                secondObject = null;
                
                StopCoroutine(SetTarget());
                StartCoroutine(SetTarget());
            }


            
        }
        if(endGame==true)
        {
            fI = Vector3.zero;
            StopCoroutine(SetTarget());
        }
       
    }
    private void OnDisable()
    {
        EventSetter(false);
    }
    IEnumerator SetTarget()
    {
        
        while ( endGame==false)
        {

            if (targetType == NPCTarget.Nearest)
            {

                foreach (CharacterController go in GameManager.instance.allCharacterController)
                {
                    if (go != this.transform.GetComponent<CharacterController>())
                    {

                        if (go.transform.gameObject.activeInHierarchy == true)
                        {
                            float dist = Vector3.Distance(go.transform.position, transform.position);
                            if (dist < lowestDistance)
                            {
                                if (lowestDistance < nextLowestDistance)
                                {
                                    nextLowestDistance = lowestDistance;
                                    secondObject = nearestObject;
                                }
                                lowestDistance = dist;
                                nearestObject = go.transform.gameObject;
                            }
                            else if (dist < nextLowestDistance)
                            {
                                nextLowestDistance = dist;
                                secondObject = go.transform.gameObject;
                            }
                        }

                    }


                }

            }
            else if (targetType == NPCTarget.Food)
            {

                foreach (Food go in GameManager.instance.foodsList)
                {

                    if (go.transform.gameObject.activeInHierarchy == true)
                    {
                        float dist = Vector3.Distance(go.transform.position, transform.position);
                        if (dist < lowestDistance)
                        {
                            if (lowestDistance < nextLowestDistance)
                            {
                                nextLowestDistance = lowestDistance;
                                secondObject = nearestObject;
                            }
                            lowestDistance = dist;
                            nearestObject = go.transform.gameObject;
                        }
                        else if (dist < nextLowestDistance)
                        {
                            nextLowestDistance = dist;
                            secondObject = go.transform.gameObject;
                        }
                    }


                }


               
            }
            yield return new WaitForSeconds(5f);
        }

    }
    public void ControlTargetType()
    {
        if (GameManager.instance.foodsList.Count == 0)
        {
            targetType = NPCTarget.Nearest;
        }
        

    }
    public void ControlTargetPosition()
    {
        fI = (transform.position - nearestObject.transform.position).normalized;
    }


    public void BeginProsses()
    {

       
        if (setFirstTarget==false)
        {
            playerİsTouched = true;
            setFirstTarget = true;
            StartCoroutine(SetTarget());
        }
        

    }

    public void EndGameProgress()
    {
      
        endGame = true;
        StopCoroutine(SetTarget());
       
    }
    private void EventSetter(bool enable)
    {
        if (enable)
        {

            EventManager.OnGameStart += BeginProsses;
            EventManager.OnLose += EndGameProgress;
            EventManager.OnWin += EndGameProgress;
        }
        else
        {

            EventManager.OnGameStart -= BeginProsses;
            EventManager.OnLose -= EndGameProgress;
            EventManager.OnWin -= EndGameProgress;
        }
    }
}
