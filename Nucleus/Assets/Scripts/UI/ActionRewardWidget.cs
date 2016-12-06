using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionRewardWidget : MonoBehaviour
{
    public Action action;
    public Slider slider;
    public GameObject sliderGO;
    public Text text;
    
	
	void Start ()
    {
	
	}
	
	void Update ()
    {
	
	}
    public void SetTextValue()
    {
        text.text = slider.value.ToString();
    }
    public void ToggleSlider()
    {
        sliderGO.SetActive(!sliderGO.activeInHierarchy);
    }
}
