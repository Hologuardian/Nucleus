using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ActionRewardWidget widget;

    public Colony colony;
    public Canvas canvas;

    private Dictionary<string, ActionRewardWidget> widgets = new Dictionary<string, ActionRewardWidget>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActionAdded(string action)
    {
        ActionRewardWidget widget_instance = Instantiate(widget, canvas.transform);

        widget_instance.transform.position = new Vector3(widget_instance.transform.position.x + widgets.Count * 50, widget_instance.transform.position.y);

        widgets.Add(action, widget_instance);
    }

    public void ActionRemoved(string action)
    {
        widgets.Remove(action);
    }
}
