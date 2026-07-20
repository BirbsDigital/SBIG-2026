using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class DaySceneManager : MonoBehaviour
{
    [Header("Scenes")]
    public string nextDayScene;
    public string previousDayScene;

    private void Update()
    {
        if (Keyboard.current.leftShiftKey.isPressed &&
            Keyboard.current.periodKey.wasPressedThisFrame)
        {
            GoToNextDay();
        }

        if (Keyboard.current.leftShiftKey.isPressed &&
            Keyboard.current.commaKey.wasPressedThisFrame)
        {
            GoToPreviousDay();
        }
    }

    public void GoToNextDay()
    {
        SceneManager.LoadScene(nextDayScene);
    }

    public void GoToPreviousDay()
    {
        SceneManager.LoadScene(previousDayScene);
    }
}