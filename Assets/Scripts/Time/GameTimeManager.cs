using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameTimeManager : MonoBehaviour, IDataPersistence
{
    public enum DayOfWeek
    {
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
    }

    #region IDataPersistence
    public void LoadData(GameData data)
    {
        this.dayHasStarted = data.dayHasStarted;
        this.currentDay = data.currentDay;
        this.currentTimeOfDay = data.currentTimeOfDay;
        this.currentDayOfWeek = (DayOfWeek)data.currentDayOfWeek;
    }

    public void SaveData(ref GameData data)
    {
        data.dayHasStarted = this.dayHasStarted;
        data.currentDay = this.currentDay;
        data.currentTimeOfDay = this.currentTimeOfDay;
        data.currentDayOfWeek = (int)this.currentDayOfWeek;
    }

    #endregion

    // Game Date and Time
    [SerializeField] private int currentDay = 1;
    [SerializeField] private double currentTimeOfDay = 0f;
    [SerializeField] private DayOfWeek currentDayOfWeek = DayOfWeek.Saturday;
    public int BedroomSceneIndex { get; set; } = 0;

    [Tooltip("The conversion of real time seconds to game time seconds")]
    [SerializeField] private float gameTimeConversionRate = 60.0f;

    public bool dayHasStarted = false; // If this is true, the day has begun and will not end until the player sleeps

    private int requiredHoursOfSleep = 9; //When the player goes to sleep, they will always sleep this long

    private int earliestBedTime = 20; // After the day reaches this time, the player can go to sleep

    private int endOfDayTime = 24; // At this time, the player will always go to sleep

    private int minuteRoundingPlace = 10; // The number the minute place will be rounded down to


    private void Update()
    {
        //  If the game is at day 1 or further, and the current day has started, continue
        if(currentDay >= 1 && dayHasStarted)
        {
            // If it is before midnight, continue ticking the time. Otherwise, force the player to end the day
            if (currentTimeOfDay < TimeSpan.FromHours(endOfDayTime).TotalSeconds)
                TickTime();
            else
                EndDay();
        }
    }

    private void TickTime()
    {
        currentTimeOfDay += Time.deltaTime * gameTimeConversionRate;
    }

    public void EndDay()
    {
        // You can only end the day if it is after the earliest possible bedtime
        if (currentTimeOfDay >= TimeSpan.FromHours(earliestBedTime).TotalSeconds)
        {
            // Calculate the time to start the next day based on what time you went to bed
            double secondsLeftInDay = TimeSpan.FromHours(endOfDayTime).TotalSeconds - currentTimeOfDay;
            double wakeUpTimeInSeconds = TimeSpan.FromHours(requiredHoursOfSleep).TotalSeconds - secondsLeftInDay;

            StartNewDay(wakeUpTimeInSeconds);
        }
        else
            Debug.LogWarning("You tried sleep at " + DisplayGameTime() + ". You can only sleep between 8pm and midnight");
    }

    void StartNewDay(double wakeUpTime)
    {
        // Set the new day and time
        currentDay++;

        if (currentDayOfWeek < DayOfWeek.Saturday)
            currentDayOfWeek++;
        else
            currentDayOfWeek = DayOfWeek.Sunday;

        currentTimeOfDay = wakeUpTime;
        dayHasStarted = false;

        FindObjectOfType<DataPersistenceManager>().SaveGame();

        FindObjectOfType<SceneChangeManager>().ChangeScenes(BedroomSceneIndex);
    }


    public string DisplayGameTime()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTimeOfDay);

        // Determine the hour and meridian
        string meridiem = "AM";
        int hours = timeSpan.Hours;
        if (hours > 11)
        {
            meridiem = "PM";

            if (hours > 12)
                hours -= 12;
        }

        // Round the minutes to whatever you want
        int minutes = (timeSpan.Minutes / minuteRoundingPlace) * minuteRoundingPlace;

        // Format the time
        string formattedTime = string.Format("{0:00}:{1:00}", hours, minutes) + $"{meridiem}";

        return formattedTime;
    }

    public string DisplayGameDate()
    {
        string formattedDate = $"{currentDayOfWeek}, August {currentDay}";

        return formattedDate;
    }
}
