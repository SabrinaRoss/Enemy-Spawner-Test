using UnityEngine;

public enum TimeOfDay
{
    MORNING,
    AFTERNOON,
    NIGHT
} 

public class TimeOfDayScript : MonoBehaviour
{
    private float day = 0.0f;
    private float afternoon = 120.0f;
    private float night = 220.0f;
    public TimeOfDay time;
    public Light dirLight;
    public float tickTime = 1.0f;
    public float tickDelay = 0.0f; // this is changed to a normal value like .1 in the actual instance
    public float roationAmount = 0.0f;
    float xRotation = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetTime();
        dirLight = GetComponent<Light>(); // put this just in case I forget to direct reference the Directional Light
        xRotation = SetRotation();
        CheckTimeOfDay();
    }

    // I decided to add a changing daytime

    // Update is called once per frame
    void Update()
    {

        tickTime -= Time.deltaTime;
        if (tickTime <= 0.0f)
        {
            tickTime = tickDelay;
            Debug.Log("Tick Done");
            xRotation += roationAmount;
            dirLight.transform.rotation = Quaternion.Euler(xRotation, 0, 0);
        }
        CheckTimeOfDay();

    }

    void SetTime() { time = (TimeOfDay)UnityEngine.Random.Range(0, 3); }
    float SetRotation()
    {
        float currentRotation = 0.0f;
        switch (time)
        {
            case TimeOfDay.MORNING:
                currentRotation = day;
                break;
            case TimeOfDay.AFTERNOON:
                currentRotation = afternoon;
                break;
            case TimeOfDay.NIGHT:
                currentRotation = night;
                break;
        }
        dirLight.transform.rotation = Quaternion.Euler(currentRotation, 0, 0);
        return currentRotation;
    }
    void CheckTimeOfDay()
    {
        // I am adding the module just in case, becuase I am pretty sure rotation does higher than 360 and does not reset, this seems like a way to prevent encountering this issue
        float xRot = xRotation % 360f;
        if (xRot >= day && xRot < afternoon)  { time = TimeOfDay.MORNING; }
        else if (xRot >= afternoon && xRot < night) { time = TimeOfDay.AFTERNOON; }
        else { time = TimeOfDay.NIGHT; }
        Debug.Log($"xRot: {xRot} - TimeOfDay: {time}");
    }
}
