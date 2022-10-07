using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.GameTime
{
    public class GameTimeContoller : MonoBehaviour
    {
        [SerializeField] int hoursInDay = 24;
        [SerializeField] float hourLenghtInMinutes = 10f;
        [SerializeField] int startHour = 10;


        //Properties
        private int currentHour;
        float timeSinceStartOfHour = 0f;


        // Start is called before the first frame update
        void Start()
        {
            currentHour = startHour;
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceStartOfHour += Time.deltaTime;
            CheckForStartOfHour();
        }

        private void CheckForStartOfHour()
        {
            if ((timeSinceStartOfHour / 60) >= hourLenghtInMinutes)
            {
                currentHour++;
                CheckForNewDay();
                timeSinceStartOfHour = 0f;
                Debug.Log("GameTimeContoller New Hour Started " + currentHour);
            }
        }

        private void CheckForNewDay()
        {
            if (currentHour >= hoursInDay)
            {
                currentHour = 0; 
                Debug.Log("GameTimeContoller New DayStarted Started " + currentHour);
            }
        }
    }


}






