using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] GameObject characterPosition;

    Vector3 defaultDistanceCameraBetweenCamera;

    private void Awake()
    {
        mainCamera = FindObjectOfType<Camera>();
    }

    void Start()
    {
        ReferanceSetter();
    }

    public void ReferanceSetter()
    {
        defaultDistanceCameraBetweenCamera = characterPosition.transform.position + mainCamera.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.position = new Vector3(characterPosition.transform.position.x,
            mainCamera.transform.position.y, characterPosition.transform.position.z + defaultDistanceCameraBetweenCamera.z);
       
    }
}
