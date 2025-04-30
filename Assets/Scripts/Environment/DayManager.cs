using UnityEngine;
using UnityEngine.Assertions;

public class DayManager : MonoBehaviour
{
    [SerializeField]
    public Light directionalLight;

    [SerializeField]
    public Color targetColor = Color.black;
    public DayCycle dayCycle;
    private readonly float LIGHT_DURATION = 1.0f;

    private Color initialColor;

    public bool shouldChangeColor = false;
    private bool firstTime = true;
    private float lerpTime = 0f;

    private void Start()
    {
        initialColor = directionalLight.color;

        Assert.IsNotNull(dayCycle, "dayCycle field is null in DayNight object");
    }

    private void OnEnable()
    {
        DayCycle.OnNightChange += OnNightChang;
    }

    private void OnDisable()
    {
        DayCycle.OnNightChange -= OnNightChang;
    }

    private void OnNightChang(bool isNight)
    {
        if (isNight)
        {
            shouldChangeColor = true;
            lerpTime = 0f;
        }
        else
        {
            firstTime = false;
            directionalLight.color = initialColor;
        }
    }

    private void ChangeLight()
    {
        lerpTime += Time.deltaTime / LIGHT_DURATION;
        directionalLight.color = Color.Lerp(initialColor, targetColor, lerpTime);
        if (lerpTime >= 1f)
        {
            directionalLight.color = targetColor;
            firstTime = false;
        }
    }

    private void FixedUpdate()
    {
        //Todo This needs to handle the day cycle
        if (shouldChangeColor && firstTime)
        {
            ChangeLight();
        }
    }
}
