using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

public class 消失显示 : MonoBehaviour
{
    [SerializeField] 简单重生平台 s;
    [SerializeField] SpriteRenderer sp;

    Bounds BB; 

    SpriteRenderer 白块;

    [SerializeField]
    [DisplayOnly]
    float s进度;
    float 进度 => s.进度;
    bool 是 => s.是;
    [SerializeField][DisplayOnly] bool 开;

    [SerializeField]
    [Tooltip("振幅缩放，最终位移 = s.进度 * 振幅缩放")]
    float 振幅缩放 = 0.5f;

    // shake coroutine and initial position
    Coroutine _shakeCoroutine;
    Vector3 _initialLocalPos;

    private void Awake()
    {
        if (sp != null)
        {
            _initialLocalPos = sp.transform.localPosition;
        }
    }
    private void OnEnable()
    {
        sp.transform.localPosition = _initialLocalPos;
        _shakeCoroutine = null;
    }
    private void Update()
    {
        if (开 != 是)
        {
 

            开 = 是;
        }
       
        s进度= 进度;

        if (sp == null || s == null) return;

        if (开)
        {
            if (_shakeCoroutine == null)
            {
                _shakeCoroutine = StartCoroutine(ShakeRoutine());
            }
        }
        else
        {
 
            if (_shakeCoroutine != null)
            {
                StopCoroutine(_shakeCoroutine);
                _shakeCoroutine = null;
            }
            // restore position
            sp.transform.localPosition = _initialLocalPos;
        }
    }

    IEnumerator ShakeRoutine()
    {
        // Desired period per shake: 0.1s => frequency = 10Hz
        const float period = 0.1f;
        const float freq = 1f / period; // 10
        float t = 0f;
        // random phase offsets to make X/Y differ
        float phaseOffsetX = Random.Range(0f, Mathf.PI * 2f);
        float phaseOffsetY = Random.Range(0f, Mathf.PI * 2f);
        while (true)
        {
            // advance time using unscaled delta to respect game pause? use Time.deltaTime
            t += Time.deltaTime;

            // amplitude is s进度 in range 0..1, clamp to be safe
            float amp = Mathf.Clamp01(s进度) * 振幅缩放;

            if (amp <= 0f)
            {
                sp.transform.localPosition = _initialLocalPos;
            }
            else
            {
                // base sine oscillation for crisp periodic shake
                float phase = t * Mathf.PI * 2f * freq; // 2πft
                float sinX = Mathf.Sin(phase + phaseOffsetX);
                float sinY = Mathf.Sin(phase + phaseOffsetY);

                // small Perlin modulation for organic variation (0.8..1.2)
                float perlin = Mathf.PerlinNoise(t * 1.3f, 0f) * 0.4f + 0.8f;

                float x = sinX * amp * perlin;
                float y = sinY * amp * perlin * 0.7f; // slightly less vertical movement

                sp.transform.localPosition = _initialLocalPos + new Vector3(x, y, 0f);
            }

            yield return null; // update every frame for smooth motion
        }
    }
 }
