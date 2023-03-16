using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMover : MonoBehaviour
{
	private Vector3 startingPosition;

    [SerializeField] private float smoothness;

    private void Start()
    {
        startingPosition = transform.localPosition;
    }

    private void Update()
    {
        Vector2 inputPos = Application.isMobilePlatform ? Input.acceleration : Input.mousePosition;
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(startingPosition.x + (inputPos.x / 2550f), startingPosition.y + (inputPos.y / 2985f), transform.localPosition.z), Time.deltaTime * smoothness);
    }
}
