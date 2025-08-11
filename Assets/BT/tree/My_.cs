using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class My_ : Composite
{
 
    public SharedInt count = 1; 
    public SharedBool repeatForever; 
    public SharedBool endOnFailure;
    private int executionCount = 0;
    // The number of times the child task has been run.

    // The status of the child after it has finished running.
    private TaskStatus executionStatus = TaskStatus.Inactive;

    public override bool CanExecute()
    {
        // Continue executing until we've reached the count or the child task returned failure and we should stop on a failure.
        return (repeatForever.Value || executionCount < count.Value) && (!endOnFailure.Value || (endOnFailure.Value && executionStatus != TaskStatus.Failure));
    }

    public override void OnChildExecuted(TaskStatus childStatus)
    {
        // The child task has finished execution. Increase the execution count and update the execution status.
        executionCount++;
        executionStatus = childStatus;
    }

    public override void OnEnd()
    {
        // Reset the variables back to their starting values.
        executionCount = 0;
        executionStatus = TaskStatus.Inactive;
    }

    public override void OnReset()
    {
        // Reset the public properties back to their original values.
        count = 0;
        endOnFailure = true;
    }
}
 
 