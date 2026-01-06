using UnityEngine;
using Sirenix.OdinInspector;
using 发射器空间;

public class 删除_速度等级velo : MonoBehaviour
{
    发射器 发射器;
    private Rigidbody rb;
    private bool isMoving = false;
    private float moveStartTime;
    private Vector3 targetPosition;
    private Vector3 initialPosition;
    private float moveTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Debug.Break();
        //Debug.DebugBreak();
    }
    [Button("Play_", ButtonSizes.Large)]
    public void Play_(float speed, float time)
    {
        if (rb == null) return;

        // 计算目标位置（根据当前方向向前移动）
        targetPosition = transform.position + transform.forward * speed * time;

        // 存储初始状态
        initialPosition = transform.position;
        moveTime = time;
        moveStartTime = Time.time;
        isMoving = true;

        // 物理反算：计算所需加速度
        MoveToPositionOverTime(targetPosition, time);
    }

    private void MoveToPositionOverTime(Vector3 targetPos, float duration)
    {
        // 获取当前位置
        Vector3 currentPos = transform.position;

        // 计算位移
        Vector3 displacement = targetPos - currentPos;

        // 根据匀加速运动公式 s = v0*t + 1/2*a*t²
        // 假设初速度为零，所以 a = 2s/t²
        float accelerationMagnitude = (2f * displacement.magnitude) / (duration * duration);

        // 计算加速度方向
        Vector3 accelerationDir = displacement.normalized;

        // 计算所需力 F = m * a
        Vector3 requiredForce = rb.mass * accelerationMagnitude * accelerationDir;

        // 应用力
        rb.AddForce(requiredForce, ForceMode.Force);
    }

    private void FixedUpdate()
    {
        if (!isMoving) return;

        float elapsedTime = Time.time - moveStartTime;

        if (elapsedTime >= moveTime)
        {
            // 时间到，停止运动
            isMoving = false;
            rb.velocity = Vector3.zero;

            // 精确校正位置
            transform.position = targetPosition;
        }
    }

    // 可选：在编辑器绘制目标位置
    private void OnDrawGizmosSelected()
    {
        if (isMoving)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(targetPosition, 0.5f);
            Gizmos.DrawLine(transform.position, targetPosition);
        }
    }
}