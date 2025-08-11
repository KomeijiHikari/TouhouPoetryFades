using UnityEngine;
using Tree_;
 
    namespace BehaviorDesigner.Runtime.Tasks
{

    [TaskDescription("Wait a specified amount of time. The task will return running until the task is done waiting. It will return success after the wait time has elapsed.")]
    [TaskIcon("{SkinColor}WaitIcon.png")]
    public class Wait : Aweak
    {
         [Tooltip("     是否用 我的时间")]
        public bool Change_=true ;
 
        [Tooltip("The amount of time to wait")]
        public SharedFloat waitTime = 1;
        [Tooltip("Should the wait be randomized?")]
        public SharedBool randomWait = false;
        [Tooltip("The minimum wait time if random wait is enabled")]
        public SharedFloat randomWaitMin = 1;
        [Tooltip("The maximum wait time if random wait is enabled")]
        public SharedFloat randomWaitMax = 1;

 
        // The time to wait
        private float waitDuration;
        // The time that the task started to wait.
        private float startTime;
        // Remember the time that the task is paused so the time paused doesn't contribute to the wait time.
        private float pauseTime;

 
        public override void OnStart()
        { 


            // Remember the start time.
            startTime = Time.time;
            if (randomWait.Value) {
                waitDuration = Random.Range(randomWaitMin.Value, randomWaitMax.Value);
            } else {
                waitDuration = waitTime.Value;
            }
        }

        public override TaskStatus OnUpdate()
        {
            //if (b.Debug_ ) Debug.LogError(waitDuration * 1 / b.I_S.Speed);
            // The task is done waiting if the time waitDuration has elapsed since the task was started.
            if (startTime +(waitDuration*1/b.I_S .固定等级差)  < Time.time) {
                return TaskStatus.Success;
            }
            // Otherwise we are still waiting.
            return TaskStatus.Running;
        }

        public override void OnPause(bool paused)
        {
            if (paused) {
                // Remember the time that the behavior was paused.
                pauseTime = Time.time;
            } else {
                // Add the difference between Time.time and pauseTime to figure out a new start time.
                startTime += (Time.time - pauseTime);
            }
        }

        public override void OnReset()
        {
            // Reset the public properties back to their original values
            waitTime = 1;
            randomWait = false;
            randomWaitMin = 1;
            randomWaitMax = 1;
        }
    }
}