using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConstantContainer;

public class GameManager : MonoBehaviour
{
    Rigidbody rigidbodyController;
    public static GameManager instance;

    [SerializeField] public List<NPCController> allNPCControllerList = new List<NPCController>();
    [SerializeField] public List<NPCController> eliminatedNPCControllerList = new List<NPCController>();
    [SerializeField] public List<NPCController> npcControllerRankedList = new List<NPCController>();
    [SerializeField] public List<CharacterController> allCharacterController = new List<CharacterController>();

    [SerializeField] public List<Food> foodsList = new List<Food>();
    [SerializeField] public int rank = 1;
    PlayerController playerController;
    public GameStates currentState;
    //CharacterController den;
    bool isGameStart = false;
    float playTime = 0;
    float planeRadius = 0;

  
    CharacterController winScoreCharacter;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        EventSetter(true);
        ReferanceSetter();
    }
    void Start()
    {

    }


    void Update()
    {
        if (currentState == GameStates.InGame)
        {
            playTime += Time.deltaTime;
            UIManager.instance.DisplayTime(playTime);
            ControlWin();

        }
       


    }
    private void OnDisable()
    {
        EventSetter(false);
    }
    public void ReferanceSetter()
    {
        playerController = FindObjectOfType<PlayerController>();
     
        PushListControl();

    }

    public void getPlaneRadius(float radius)
    {
        planeRadius = radius ;
     
    }

    public void PushListControl()
    {
        allCharacterController.AddRange(FindObjectsOfType<CharacterController>());
        allNPCControllerList.AddRange(FindObjectsOfType<NPCController>());
        foodsList.AddRange(FindObjectsOfType<Food>());

       
    }
    public void RemoveFoodFromList(Food eatingFood)
    {
        foodsList.Remove(eatingFood);
    }

    IEnumerator RankControl()
    {

      //  allCharacterController.Sort(den.score);
        while (currentState == GameStates.InGame)
        {

            foreach (NPCController npcController in allNPCControllerList)
            {
                if (npcController.transform.gameObject.activeInHierarchy == true)
                {
                    if (npcController.score >= playerController.score)
                    {


                        if (!npcControllerRankedList.Contains(npcController))
                            npcControllerRankedList.Add(npcController);

                    }
                    else
                    {
                        if (npcControllerRankedList.Contains(npcController))
                            npcControllerRankedList.Remove(npcController);

                    }

                }
               

               
            }

            rank = npcControllerRankedList.Count + 1;

            if (rank == 1)
            {
                UIManager.instance.FirstCharacter("Player");
            }
            else
            {
                UIManager.instance.FirstCharacter(npcControllerRankedList[0].transform.gameObject.transform.name);
            }


            yield return new WaitForSeconds(0.001f);
        }
    }


    IEnumerator OutOfPlaneControl()
    {

        while (currentState == GameStates.InGame)
        {

            foreach (CharacterController characterController in allCharacterController)
            {

            
                float distance = Vector3.Distance(characterController.transform.position, Vector3.zero);




                if (distance > planeRadius+0.25f)
                {


                    if (characterController.GetComponentInChildren<PlayerController>())
                    {
                        EventManager.LoseEvent();
                        rigidbodyController = characterController.GetComponentInChildren<PlayerController>().GetComponentInChildren<Rigidbody>();
                        rigidbodyController.useGravity = true;
                        StartCoroutine(CharacterSetActiveFalse(characterController.GetComponentInChildren<PlayerController>().transform.gameObject));
                        ChangeGameState(GameStates.LevelEnd);
                    
                    }
                    else
                    {
                        if (!eliminatedNPCControllerList.Contains(characterController.GetComponentInChildren<NPCController>()))
                        {
                            eliminatedNPCControllerList.Add(characterController.GetComponentInChildren<NPCController>());
                        }
                            

                        
                        winScoreCharacter = characterController.lastTouchCharacter;
                        winScoreCharacter.KillScore();
                

                        rigidbodyController = characterController.GetComponentInChildren<NPCController>().GetComponentInChildren<Rigidbody>();
                        rigidbodyController.useGravity = true;

                        StartCoroutine(CharacterSetActiveFalse(characterController.GetComponentInChildren<NPCController>().transform.gameObject));
                        npcControllerRankedList.Remove(characterController.GetComponentInChildren<NPCController>());
                       
                     
                    }

                 


                }

                

            }





            yield return new WaitForSeconds(0.05f);

        }




    }
    
    public void ControlWin()
    {
        if (eliminatedNPCControllerList.Count == allNPCControllerList.Count)
        {
            EventManager.WinEvent();
            ChangeGameState(GameStates.LevelEnd);
        }
    }
    IEnumerator CharacterSetActiveFalse(GameObject character)
    {
       
        yield return new WaitForSeconds(1.5f);
        character.SetActive(false);
    }
   

    public void ChangeGameState(GameStates targetState)
    {
        currentState = targetState;
    }
    public void BeginProsses()
    {
        isGameStart = true;

        ChangeGameState(GameStates.InGame);
        StartCoroutine(RankControl());
        StartCoroutine(OutOfPlaneControl());
    }
    private void EventSetter(bool enable)
    {
        if (enable)
        {

            EventManager.OnGameStart += BeginProsses;
          
        }
        else
        {

            EventManager.OnGameStart -= BeginProsses;
        }
    }
}
