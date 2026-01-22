using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements; 

public class 消失显示 : MonoBehaviour
{
    public bool 尺寸;
    public bool 颜色;
   I_消失进度 s;
    [SerializeField]
    GameObject
    ObjI_消失进度;
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
        if (ObjI_消失进度 == null)
        {
            ObjI_消失进度 = gameObject;
            if (ObjI_消失进度 == null)
            {
                ObjI_消失进度 = transform.parent.gameObject;
            }

        }
        s = ObjI_消失进度.GetComponent<I_消失进度>();
        if (sp != null)
        {
 
            _initialLocalPos = sp.transform.localPosition;
            if (颜色) _initialLColor = sp.color;
            if (尺寸) _initialLLosize =sp.transform.localScale;
        }
    }
    Color _initialLColor;
    Vector2 _initialLLosize;
    private void OnEnable()
    {
        if (sp != null)
        if (颜色) sp.color  = _initialLColor;
        if (尺寸) sp.transform.localScale = _initialLLosize;
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

            if (颜色) sp.color = _initialLColor;
            if (尺寸) sp.transform.localScale = _initialLLosize;
            // restore position
            sp.transform.localPosition = _initialLocalPos;
        }
    }

    IEnumerator ShakeRoutine()
    {
        Vector2 s尺寸 = sp.transform.localScale;
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

            if (尺寸)     sp.transform.localScale = s尺寸 * (振幅缩放 + s进度);
   
            if (颜色) sp.color= Color.Lerp(Color.white, Color.red,进度)   ;
    
            if (amp <= 0f)
            {
                if (颜色) sp.color = _initialLColor;
                if (尺寸) sp.transform.localScale = _initialLLosize;
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
