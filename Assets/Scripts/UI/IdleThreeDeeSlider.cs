using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using COMIdleStage1;

public class IdleThreeDeeSlider : MonoBehaviour
{
    public Material handlePressedMat;

	public AudioClip pressSound;

	public Vector2 bounds;

    private Material handleOriginalMaterial;

    private bool shouldChangeHandleMat;

    private bool isMouseDown;

    public float currentValue = 50f;

    public int methodGroup = 1;

    public string playerPrefsValue;

	private float mouseDelta;

	private bool firstDrag;

    public void OnClicked()
    {
		transform.GetComponent<MeshRenderer>().material = handlePressedMat;
		Game.ACInstance.PlayClip(pressSound);
        StartCoroutine(Drag());
    }

    private IEnumerator Drag()
    {
		firstDrag = true;
		shouldChangeHandleMat = false;
		mouseDelta = Input.mousePosition.x;
        while (Input.GetMouseButton(0))
        {
            yield return new WaitForEndOfFrame();
			transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x + ((Input.mousePosition.x - mouseDelta) / 500f), bounds.x, bounds.y), transform.localPosition.y, transform.localPosition.z);
			mouseDelta = Input.mousePosition.x;
            currentValue = (Utilities.Percentage(transform.localPosition.x - bounds.x, bounds.y - bounds.x));
			Game.RCInstance.SendMessage("SlidersGroup" + methodGroup, new SliderValue(this.name, this.currentValue));
        }
		if (Input.GetMouseButtonUp(0))
        {
            isMouseDown = false;
            shouldChangeHandleMat = true;
        }
		shouldChangeHandleMat = true;
    }

    private void Start()
    {
        if (playerPrefsValue != "")
        {
            currentValue = PlayerPrefs.GetFloat(playerPrefsValue);
        }
        handleOriginalMaterial = transform.GetComponent<MeshRenderer>().material;
        transform.localPosition = new Vector3(Mathf.Lerp(bounds.x, bounds.y, currentValue / 100f), transform.localPosition.y, transform.localPosition.z);
    }

    private void Update()
    {
        if (shouldChangeHandleMat)
        {
            transform.GetComponent<MeshRenderer>().material = handleOriginalMaterial;
        }
    }

    public struct SliderValue
    {
        public string name;

        public float value;

        public SliderValue(string nm, float val)
        {
            this.name = nm;
            this.value = val;
        }
    }
}