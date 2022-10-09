using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.WeatherControl
{
    public class CameraWeatherVFX : MonoBehaviour
    {
        [SerializeField] WeatherVFX[] weatherVFXes;
        [SerializeField] WeatherContoller weatherContoller;

        [System.Serializable]
        public struct WeatherVFX
        {
            [SerializeField] public Weathers weather;
            [SerializeField]  public GameObject vfx;
        }

        // Start is called before the first frame update
        void Start()
        {
            if (weatherContoller != null)
            {
                weatherContoller.weatherHasChanged += SetVFX;
            }
        }

        public void SetVFX()
        {
            foreach (var weatherVFX in weatherVFXes)
            {
                Debug.Log("CameraWeatherVFX " + weatherVFX.weather + " " + weatherContoller.CurrentWeather);
                weatherVFX.vfx.SetActive(false);
                if (weatherContoller.CurrentWeather == weatherVFX.weather)
                {
                    weatherVFX.vfx.SetActive(true);
                }
            }
        }
    }

}


