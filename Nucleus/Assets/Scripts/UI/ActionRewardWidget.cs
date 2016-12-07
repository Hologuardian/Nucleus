using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionRewardWidget : MonoBehaviour
{
    public Colony colony;
    public Text text;
    public Image fill_bottom;
    public Sprite icon;
    public Slider slider;
	
	void Start()
    {
	    
	}
	
	void Update()
    {
	
	}

    public void Initialise(string action, Colony colony)
    {
        text.text = action;
        this.colony = colony;

        icon = Resources.Load<Sprite>("Sprites/" + action);

        ColorBlock colorBlock = slider.colors;
        // GlobalsSetter has a dictionary of action name to colour
        colorBlock.pressedColor = fill_bottom.color = GlobalsSetter.action_COLOR[action];
    }

    public void ValueChange()
    {
        colony.actionRewards[text.text] = slider.value;
    }
}
