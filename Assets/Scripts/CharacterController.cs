using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    protected Rigidbody playerRigidBody;
    Animator animator;
    GameObject playerModel;
    GameObject interactionObject;

    public int score=0;
    int killScore = 200;

    [Header("Movement  control")]
    
    [SerializeField] float moveSpeed = 2f;

    [Header("Food Effect Scale")]
    [SerializeField] float foodEffectLerpTime;

    [Header("Interaction Settings")]
    [SerializeField] float interactionEffecTime;
    [SerializeField] float equalİnteractionTime;
    float otherInteractionScale;
    float initialPushForce;
    [SerializeField] float pushForce = 2.5f;
    [SerializeField] float doubleBackForce = 5f;
    [SerializeField] float equalPushForce = 2.5f;
    public  bool isChracterInteraction = false;
    Vector3 interactionVector;
  

    [SerializeField] public CharacterController lastTouchCharacter;

    public void Awake()
    {
        ReferanceSetter();
        
    }

    public void Start()
    {
        
    }


    public void Update()
    {
       
    
    }
    private void ReferanceSetter()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerModel = this.transform.GetChild(0).transform.gameObject;
        animator = GetComponentInChildren<Animator>();
        interactionObject = this.transform.GetChild(1).transform.gameObject;
        initialPushForce = pushForce;
        equalİnteractionTime = interactionEffecTime / 2;

    }
    protected virtual void Movement(Vector3 joyistickPosition)
    {
       
        SetInteractionSphrePosition();
        AnimationController();

        if (isChracterInteraction == false)
        {
     
            NonInteractıonPlay(joyistickPosition);
        }
        else
        {
            
            InteractionMovement(interactionVector * otherInteractionScale* pushForce);
        }
        
    }

    public void NonInteractıonPlay(Vector3 joyistickPosition)
    {
        playerRigidBody.velocity = new Vector3(joyistickPosition.x * moveSpeed,
                       playerRigidBody.velocity.y,
                       joyistickPosition.z * moveSpeed);

        if (joyistickPosition.x != 0 || joyistickPosition.z != 0)
        {
            transform.rotation = Quaternion.LookRotation(playerRigidBody.velocity);
        }
    }
    public void InteractionMovement(Vector3 interaction)
    {
        playerRigidBody.velocity = new Vector3(interaction.x * moveSpeed,
                       playerRigidBody.velocity.y,
                       interaction.z * moveSpeed);

      
    }

    public void AnimationController()
    {
        if (playerRigidBody.velocity.magnitude > 0)
        {

            AnimationPlay(ConstantContainer.AnimationsName.RUN_TRIGGER);
        }
        else
        {
            AnimationPlay(ConstantContainer.AnimationsName.IDLE_TRIGGER);
        }
    }
    public void AnimationPlay(string animationTriggerName)
    {

        animator.SetTrigger(animationTriggerName);
    }
    public float GetCharacterScale()
    {
        return transform.localScale.x;
    }
    public void SetInteractionSphrePosition()
    {
        
      interactionObject.transform.position = this.transform.position +new Vector3(0,1,0);
    }

    public IEnumerator InterajtionCharacterTime(Vector3 fI,float otherScale,CharacterController interactionCharacter,bool isScaleEqual)
    {
       
        lastTouchCharacter = interactionCharacter;


        if (fI.z < -0.5f && fI.x < 0.3f && fI.x > -0.3f)
        {

            pushForce = doubleBackForce;
        }
        else
        {
            if (isScaleEqual == true)
            {

                pushForce = equalPushForce;
            }
            else if (isScaleEqual == false)
            {
                pushForce = initialPushForce;


            }
        }


        otherInteractionScale = otherScale;
        interactionVector = -fI;
        isChracterInteraction = true;

        // playerRigidBody.velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(interactionEffecTime);
        isChracterInteraction = false;
      
    }
    public IEnumerator GainFood(float foodEffectScale,int foodScore)
    {

        score += foodScore;
        Vector3 target = playerModel.transform.localScale + Vector3.one * foodEffectScale;
        float timeElapsed = 0;
        while (timeElapsed < foodEffectLerpTime)
        {
            playerModel.transform.localScale = Vector3.Lerp(this.transform.localScale, target, timeElapsed /foodEffectLerpTime);
            timeElapsed += Time.deltaTime;
            

        }
        
        yield return null;

    }
    public void KillScore()
    {
        score += killScore;

    }
}
