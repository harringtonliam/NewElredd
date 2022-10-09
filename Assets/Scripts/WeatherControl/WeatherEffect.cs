using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.WeatherControl
{
    [System.Serializable]
    public class WeatherEffect 
    {
        [SerializeField] Weathers weather;
        [SerializeField] float lightIntensityPercentage;
        public Weathers Weather { get { return weather; } }
        public float LightIntesityPercentage { get { return lightIntensityPercentage; } }

    }



}


