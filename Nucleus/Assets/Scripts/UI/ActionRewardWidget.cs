using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionRewardWidget : MonoBehaviour
{
    public Colony colony;
    public Text text;
    public Image fill_top;
    public Image fill_bottom;
    public Image icon;
    public Text icon_text;
    public Slider slider;

    private bool isVisible = false;
    private float visibility = 1;

    public float timer_goal = 0.2f;
    private float timer = 0;
	
	void Start()
    {
	    
	}
	
	void Update()
    {
        float timer_evaluated = timer / timer_goal;

        if (isVisible)
        {
            if (visibility < 1)
            {
                visibility = Mathf.Lerp(0, 1, timer_evaluated);
                timer += Time.deltaTime;

                float lerp = Mathf.Lerp(1, 0, timer_evaluated);

                fill_top.color = new Color(fill_top.color.r, fill_top.color.g, fill_top.color.b, lerp);
                fill_bottom.color = new Color(fill_bottom.color.r, fill_bottom.color.g, fill_bottom.color.b, lerp);
                text.color = new Color(text.color.r, text.color.g, text.color.b, lerp);
            }
        }
        else
        {
            if (visibility > 0)
            {
                visibility = Mathf.Lerp(1, 0, timer_evaluated);
                timer += Time.deltaTime;

                float lerp = Mathf.Lerp(1, 0, timer_evaluated);

                fill_top.color = new Color(fill_top.color.r, fill_top.color.g, fill_top.color.b, lerp);
                fill_bottom.color = new Color(fill_bottom.color.r, fill_bottom.color.g, fill_bottom.color.b, lerp);
                text.color = new Color(text.color.r, text.color.g, text.color.b, lerp);
            }
        }
	}

    public void Initialise(string action, Colony colony)
    {
        text.text = action;
        this.colony = colony;

        icon = Resources.Load<Image>("Sprites/" + action);

        ColorBlock colorBlock = slider.colors;
        // GlobalsSetter has a dictionary of action name to colour
        colorBlock.pressedColor = fill_bottom.color = GlobalsSetter.action_COLORS[action];
    }

    public void ValueChange()
    {
        icon_text.text = slider.value.ToString();
        colony.actionRewards[text.text] = slider.value;
    }

    public void ToggleVisibility()
    {
        isVisible = !isVisible;
        timer = 0;
    }
}
