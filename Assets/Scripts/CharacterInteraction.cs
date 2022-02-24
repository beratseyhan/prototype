using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    Rigidbody interactionSphereRigidbody;
    Rigidbody collisionObjectSphereRigidBody;
     

    float thisCharacterScale;
    float otherCharacterScale;

    Vector3 fI;

    public float interactionForce;

    float thisMomoentum, otherMomentum;




    private void OnCollisionEnter(Collision collision)
    {

        
        if (collision.gameObject.layer == LayerMask.NameToLayer(ConstantContainer.LayerNames.INTERACTION_LAYER))
        {
            
            if (collision.transform.parent.GetComponent<CharacterController>() )
            {





                thisCharacterScale = this.transform.parent.transform.localScale.x;
                CharacterController otherCharacterController = collision.transform.parent.GetComponent<CharacterController>();
                otherCharacterScale = otherCharacterController.GetCharacterScale();


                   if (thisCharacterScale> otherCharacterScale)
                    {


                       
                        fI = (transform.position - collision.transform.position).normalized;
                        fI = new Vector3(fI.x, 0, fI.z)
                        + new Vector3(UnityEngine.Random.Range(-0.15f, +0.15f), UnityEngine.Random.Range(0, 0), UnityEngine.Random.Range(-0.15f, +0.15f)); 
                        StartCoroutine(otherCharacterController.InterajtionCharacterTime(fI, collision.transform.parent.gameObject.transform.localScale.x, this.transform.parent.GetComponent<CharacterController>(),false));
                    }
                      else if (thisCharacterScale == otherCharacterScale)
                    {
                  
                   
                    fI = (transform.position - collision.transform.position).normalized;
                    fI = new Vector3(fI.x, 0, fI.z) + new Vector3(UnityEngine.Random.Range(-0.15f, +0.15f), UnityEngine.Random.Range(0, 0), UnityEngine.Random.Range(-0.15f, +0.15f));
                    StartCoroutine(otherCharacterController.InterajtionCharacterTime(fI, collision.transform.parent.gameObject.transform.localScale.x, this.transform.parent.GetComponent<CharacterController>(), true));
                    StartCoroutine(this.transform.parent.GetComponent<CharacterController>().InterajtionCharacterTime(-fI, 2, otherCharacterController, true));
                }
                

            }
          

        }


    }


    void Start()
    {

    }


    void Update()
    {

    }
    private void FixedUpdate()
    {

    }


    private void Awake()
    {
        ReferanceSetter();


    }
    private void ReferanceSetter()
    {


       
    }
}
