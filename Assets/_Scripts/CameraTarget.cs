using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Transform playerTransform;
    [SerializeField] float threshold;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform == null) return;

        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (playerTransform.position + mousePos) / 2f;

        targetPos.x = Mathf.Clamp(targetPos.x, playerTransform.position.x - threshold, playerTransform.position.x + threshold);
		targetPos.y = Mathf.Clamp(targetPos.y, playerTransform.position.y - threshold, playerTransform.position.y + threshold);

        this.transform.position = targetPos;
	}
}
