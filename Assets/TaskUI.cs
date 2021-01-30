using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    private TaskManager tManager;
    [SerializeField] private List<TaskLineUI> tasksToDisplay;

    private void Start()
    {
        tManager = TaskManager.instance;
    }

    public void RefreshTasks()
    {
        int lastTaskCompleted = Mathf.Max(0,tManager.LastCompleteTaskIndex());
        int startFromIndex = Mathf.Min(lastTaskCompleted, tManager.TotalTasks - tasksToDisplay.Count);

        for (int i = 0; i < tasksToDisplay.Count; i++)
        {
            TaskStatus associatedTask = tManager.taskStatuses[i+startFromIndex];
            tasksToDisplay[i].taskText.text = associatedTask.task.taskDescription;
            if(associatedTask.complete) tasksToDisplay[i].Strike();
            else tasksToDisplay[i].Unstrike();
        }
    }

    private void Update() 
    {
        RefreshTasks(); //whatever
    }
}
