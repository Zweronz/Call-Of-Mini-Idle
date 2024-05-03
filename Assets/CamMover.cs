using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMover : MonoBehaviour
{
	private Vector3 startingPosition;

    [SerializeField] private float smoothness;

    private float halfTimeStep, timeStep;

    private void Start()
    {
        startingPosition = transform.localPosition;
    }

    private void Update()
    {
        timeStep += Time.deltaTime * 25f;
        halfTimeStep += Time.deltaTime * 12.5f;

        Vector2 inputPos = Application.isMobilePlatform ? Input.acceleration : Input.mousePosition;
        Vector2 input = new Vector2(inputPos.x / 2550f, inputPos.y / 2985f);
        
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(startingPosition.x + input.x, startingPosition.y + input.y + (Mathf.Sin(timeStep) * 0.1f * Random.Range(0.8f, 1f)), transform.localPosition.z), Time.deltaTime * smoothness);
        transform.localRotation = Quaternion.Euler(5 + -input.y * 20 + Mathf.Cos(timeStep) * 0.5f * Random.Range(0.8f, 1f), -5 + input.x * 10, Mathf.Cos(halfTimeStep) * Random.Range(0.8f, 1f));
    }
}
