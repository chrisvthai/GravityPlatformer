using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float leftBoundary, rightBoundary, upperBoundary, lowerBoundary;
    
    public GameObject player;
    private Vector3 offset;

	void Start () {
        offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = player.transform.position + offset;

        if (transform.position.x <= leftBoundary)
            transform.position = new Vector3(leftBoundary, transform.position.y, transform.position.z);
        else if (transform.position.x >= rightBoundary)
            transform.position = new Vector3(rightBoundary, transform.position.y, transform.position.z);

        if (transform.position.y >= upperBoundary)
            transform.position = new Vector3(transform.position.x, upperBoundary, transform.position.z);
        else if (transform.position.y <= lowerBoundary)
            transform.position = new Vector3(transform.position.x, lowerBoundary, transform.position.z);
        
	}
}
