// chat I highkey used AI for this
using UnityEngine;
using TMPro;   // text mesh pro

public class DayTracker : MonoBehaviour
{
    public int day = 1;

    // Drag your TextMeshProUGUI object here in the Inspector
    public TMP_Text dayText; // idk what this does tbh

    void Start() // when the game starts, run da function
    {
        UpdateDayText();
    }

    public void NextDay() // when u click the NextDayButton, this runs
    {
        day++; // increase the day counter by 1
        UpdateDayText(); // run da functoin
    }

    void UpdateDayText() // function that changes the textbox
    {
        dayText.text = "Day " + day;
    }
}