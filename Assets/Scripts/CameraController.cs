using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target = null;


    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.playerInstance.transform;
    }
    
    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position =  new Vector3(target.position.x, target.position.y, transform.position.z);   
    }
}
