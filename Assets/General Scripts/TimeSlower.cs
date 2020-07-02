using UnityEngine;

public class TimeSlower : MonoBehaviour
{
    public Material VignetteTintMaterial;

    public Color CameraTintColor;

    [Range(0, 1)]
    public float CameraTintFactor;

    public float SlowFactor;
    public float SlowSmoothTime;

    private float slowSmoothTimer;
    private float smoothDirection;
    private bool isSmoothing;


    public bool IsSlowingTime { get; private set; } = false;



    void Start()
    {

    }

    void Update()
    {
        if (isSmoothing)
        {
            slowSmoothTimer += Time.unscaledDeltaTime * smoothDirection;
            float realSlowScale = Mathf.Lerp(1, SlowFactor, slowSmoothTimer / SlowSmoothTime);

            Time.timeScale = realSlowScale;
            Time.fixedDeltaTime = 0.02f * realSlowScale;

            float cameraTintFactor = Mathf.Lerp(0, CameraTintFactor, slowSmoothTimer / SlowSmoothTime);
            VignetteTintMaterial.SetFloat("_TintFactor", cameraTintFactor);

            if (slowSmoothTimer > SlowSmoothTime || slowSmoothTimer < 0)
                isSmoothing = false;
        }
    }


    public void StartSlow()
    {
        if (IsSlowingTime)
            return;

        IsSlowingTime = true;
        isSmoothing = true;
        slowSmoothTimer = Mathf.InverseLerp(1, SlowFactor, Time.timeScale) * SlowSmoothTime;
        smoothDirection = 1;
    }

    public void StopSlow()
    {
        if (!IsSlowingTime)
            return;

        isSmoothing = true;
        IsSlowingTime = false;
        slowSmoothTimer = Mathf.InverseLerp(1, SlowFactor, Time.timeScale) * SlowSmoothTime;
        smoothDirection = -1;
    }

}
