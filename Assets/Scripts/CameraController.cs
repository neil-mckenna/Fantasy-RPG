using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class CameraController : MonoBehaviour
{
    public Transform target = null;
    
    public Tilemap theMap = null;
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    private float halfHeight;
    private float halfWidth;

    public float boundExtraOffset = 1f;


    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.playerInstance.transform;

        halfHeight = Camera.main.orthographicSize;
        halfWidth = halfHeight * Camera.main.aspect;

        bottomLeftLimit = theMap.localBounds.min;
        topRightLimit = theMap.localBounds.max;

        PlayerController.playerInstance.SetBounds(theMap.localBounds.min, theMap.localBounds.max);

        // this compresses the tilemap to stop excessive missing blank edges after drawing on tilemaps 
        if(theMap != null)
        {
            theMap.CompressBounds();
        }

    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position =  new Vector3(target.position.x, target.position.y, transform.position.z);

        // keep the camera inside in the bounds
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, (bottomLeftLimit.x + halfWidth) * boundExtraOffset, (topRightLimit.x - halfWidth) * boundExtraOffset),
            Mathf.Clamp(transform.position.y, (bottomLeftLimit.y + halfHeight) * boundExtraOffset, (topRightLimit.y - halfHeight) * boundExtraOffset),
            transform.position.z
        );




    }
}
