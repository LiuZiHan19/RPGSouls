using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    public void PauseTime()
    {
        Time.timeScale = 0;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1;
    }
}