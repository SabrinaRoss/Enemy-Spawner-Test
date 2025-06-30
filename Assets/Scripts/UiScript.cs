using UnityEngine;
using TMPro;

public class UiScript : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TimeOfDayScript timeOfDay;
    // Update is called once per frame
    void Update()
    {
        if (timeOfDay != null && timeText != null) { timeText.text = $"It is {timeOfDay.time}";  }
    }
}
