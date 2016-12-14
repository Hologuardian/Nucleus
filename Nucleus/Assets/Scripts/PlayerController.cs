using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class QueuedAction
{
    public string actionToDo;
}

public class PlayerController : NetworkBehaviour
{
    public ActionRewardWidget widget;

    public List<QueuedAction> queuedActions = new List<QueuedAction>();

    public Colony colony;
    public Canvas canvas;

    private Dictionary<string, ActionRewardWidget> widgets = new Dictionary<string, ActionRewardWidget>();

    // Use this for initialization
    void Start()
    {
        colony.playerController = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (canvas == null)
        {
            ColonyCamera cerma = GameObject.FindObjectOfType<ColonyCamera>();
            cerma.colony = colony;
            canvas = cerma.GetComponentInChildren<Canvas>();
        }
        else
        {
            foreach(QueuedAction action in queuedActions)
            {
                ActionAdded(action.actionToDo);
            }

            queuedActions.Clear();
        }
    }

    public void ActionAdded(string action)
    {
        if (!widgets.ContainsKey(action))
        {
            if (canvas == null)
            {
                queuedActions.Add(new QueuedAction() { actionToDo = action });
                return;
            }

            ActionRewardWidget widget_instance = Instantiate(widget, canvas.transform);
            widget_instance.Initialise(action, colony);

            widget_instance.transform.position = new Vector3(widget_instance.transform.position.x + widgets.Count * 50, widget_instance.transform.position.y);

            widgets.Add(action, widget_instance);
        }
    }

    public void ActionRemoved(string action)
    {
        widgets.Remove(action);
    }
}
