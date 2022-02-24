using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] float foodEffectScale = 0.1f;
    CharacterController characterController;
    int foodScore = 50;
    void Start()
    {
   
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.layer == LayerMask.NameToLayer(ConstantContainer.LayerNames.PLAYER_LAYER) || other.gameObject.layer == LayerMask.NameToLayer(ConstantContainer.LayerNames.NPCC_LAYER))
        {
            if (other.GetComponent<CharacterController>() != null)
            {
                characterController = other.GetComponent<CharacterController>();
                StartCoroutine(characterController.GainFood(foodEffectScale,foodScore));

            }

            GameManager.instance.RemoveFoodFromList(this);
            this.gameObject.SetActive(false);
        }
    }
}
