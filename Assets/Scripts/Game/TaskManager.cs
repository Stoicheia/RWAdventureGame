using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager instance;

    public List<TaskStatus> taskStatuses;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void CompleteTask(Task toComplete)
    {
        CompleteTask(toComplete.taskName);
    }

    public void CompleteTask(string taskName)
    {
        foreach (var t in taskStatuses)
        {
            if (t.task.taskName == taskName)
            {
                t.complete = true;
                return;
            }
        }
        Debug.LogWarning(taskName + " doesn't seem to be a valid task");
    }

    public void UncompleteTask(Task toUncomplete)
    {
        UncompleteTask(toUncomplete.taskName);
    }
    
    public void UncompleteTask(string taskName)
    {
        foreach (var t in taskStatuses)
        {
            if (t.task.taskName == taskName)
            {
                t.complete = false;
                return;
            }
        }
        Debug.LogWarning(taskName + " doesn't seem to be a valid task");
    }

    public int LastCompleteTaskIndex()
    {
        for (int i = 0; i < taskStatuses.Count; i++)
        {
            if (!taskStatuses[i].complete)
            {
                return i-1;
            }
        }

        return taskStatuses.Count - 1;
    }

    public int TotalTasks => taskStatuses.Count;
}

[System.Serializable]
public class TaskStatus
{
    public Task task;
    public bool complete;
}
