using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.GameTime;
using RPG.Core;
using System;

namespace RPG.WeatherControl
{
    public class WeatherContoller : MonoBehaviour
    {

        [SerializeField] GameTimeContoller gameTimeContoller;
        [SerializeField] WeatherTable weatherTable;
        [SerializeField] WeatherDescriptions weatherDescriptions;
        [SerializeField] Light[] lightSources;


        int currentWeatherDurationHours = 1;
        int currentWeatherHourSoFar = 10;
        Dice dice;
        Weathers currentWeather;

        float[] lighySourceStartIntensities;

        public event Action weatherHasChanged;

        public Weathers CurrentWeather {  get { return currentWeather; } }

        // Start is called before the first frame update
        void Start()
        {
            StoreStartLightSOurceIntensities();
            dice = FindObjectOfType<Dice>();
            gameTimeContoller.hourHasPassed += GenerateWeather;
            GenerateWeather();
        }

        private void StoreStartLightSOurceIntensities()
        {
            lighySourceStartIntensities = new float[lightSources.Length];
            for (int i = 0; i < lightSources.Length; i++)
            {
                lighySourceStartIntensities[i] = lightSources[i].intensity;
            }
        }

        private void GenerateWeather()
        {
            currentWeatherHourSoFar++;
            if (NewWeatherNeeded())
            {
                int randomWeatherDiceRoll = dice.RollDice(100, 1);
                Weathers newWeather = weatherTable.GetWeather(gameTimeContoller.GetCurrentMonth(), randomWeatherDiceRoll);
                Debug.Log("Generate Weather " + newWeather);
                currentWeatherDurationHours = dice.RollDice(4, 1);
                currentWeatherHourSoFar = 0;
                currentWeather = newWeather;
                SetWeatherEffect(newWeather);
                if (weatherHasChanged!= null)
                {
                    weatherHasChanged();
                }
            }
        }

        private bool NewWeatherNeeded()
        {
            if (currentWeatherDurationHours <= currentWeatherHourSoFar)
            {
                return true;
            }
            return false;
        }

        private void SetWeatherEffect(Weathers weather)
        {
            float newLightIntensityPercentage = weatherDescriptions.GetWeatherEffect(weather).LightIntesityPercentage;
            for (int i = 0; i < lightSources.Length; i++)
            {
                lightSources[i].intensity = lighySourceStartIntensities[i] * (newLightIntensityPercentage/100);
            }
        }
    }
}


