using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.GameTime;
using System;
using System.Linq;

namespace RPG.Control
{
    public class AIBehaviour : MonoBehaviour
    {
        [SerializeField] BehaviourDescription[] behaviourDescriptions;


        [System.Serializable]
        public class BehaviourDescription
        {
            public bool appliesToAllMonths = true;
            public Months month;
            public bool appliesToSpecificWeekDay = false;
            public WeekDays weekDay;
            public bool appliesToAllDays = true;
            public int dayFrom;
            public int dayTo;
            public int hourFrom;
            public int hourTo;
            public PatrolPath patrolPath;
            public float waypointPauseTime = 2f;
            public float patrolSpeedFraction = 0.2f;

            public string Print()
            {
                return "Behaviour Description applies to all months " + appliesToAllMonths + " month  " + month + " appliestospecfic weekday " + appliesToSpecificWeekDay + ": " + weekDay +"appplies to all days " + appliesToAllDays + " day from and to " + dayFrom + " " + dayTo + " hours from and to " + hourFrom + " " + hourTo;
            }
        }

        AIControler aIControler;
        GameTimeContoller gameTimeContoller;

        // Start is called before the first frame update
        void Start()
        {
            aIControler = GetComponent<AIControler>();
            gameTimeContoller = FindObjectOfType<GameTimeContoller>();
            gameTimeContoller.hourHasPassed += CheckBehaviour;
        }


        private void CheckBehaviour()
        {

            var sorted = behaviourDescriptions.OrderBy(m => m.appliesToAllMonths).ThenByDescending(w => w.appliesToSpecificWeekDay).ThenBy(d => d.appliesToAllDays).ToArray();

            for (int i = 0; i < sorted.Length; i++)
            {
                Debug.Log(" sprted array " + i + " " + sorted[i].Print());
            }

            BehaviourDescription[] specificMonthAndDay = behaviourDescriptions.Where(b => !b.appliesToAllDays  && !b.appliesToAllMonths).ToArray();
            Debug.Log(" Specfic Month and Day len " + specificMonthAndDay.Length.ToString());
            foreach (var behaviourDescription in specificMonthAndDay)
            {
                Debug.Log("Checking Specfic Month and Day");
                if (BehaviourApplies(behaviourDescription))
                {
                    ApplyBehaviour(behaviourDescription);
                    return;
                }
            }
            BehaviourDescription[] specificMonthAndAllDays = behaviourDescriptions.Where(b => b.appliesToAllDays && !b.appliesToAllMonths).ToArray();
            foreach (var behaviourDescription in specificMonthAndAllDays)
            {
                if (BehaviourApplies(behaviourDescription))
                {
                    ApplyBehaviour(behaviourDescription);
                    return;
                }
            }
            BehaviourDescription[] allMonthsAndSpecificDays = behaviourDescriptions.Where(b => !b.appliesToAllDays && b.appliesToAllMonths).ToArray();
            Debug.Log(" All Month and Specfic Day len " + specificMonthAndDay.Length.ToString());
            foreach (var behaviourDescription in specificMonthAndAllDays)
            {
                if (BehaviourApplies(behaviourDescription))
                {
                    ApplyBehaviour(behaviourDescription);
                    return;
                }
            }

            BehaviourDescription[] allMonthsAndAllDays = behaviourDescriptions.Where(b => b.appliesToAllDays && b.appliesToAllMonths).ToArray();
            foreach (var behaviourDescription in behaviourDescriptions)
            {
                if (BehaviourApplies(behaviourDescription))
                {
                    ApplyBehaviour(behaviourDescription);
                    return;
                }
            }
        }

        private void ApplyBehaviour(BehaviourDescription behaviourDescription)
        {
            aIControler.SetPatrolPath(behaviourDescription.patrolPath);
            aIControler.SetPatrolSpeedFraction(behaviourDescription.patrolSpeedFraction);
            aIControler.SetWayPointPauseTime(behaviourDescription.waypointPauseTime);
        }

        private bool BehaviourApplies(BehaviourDescription behaviourDescription)
        {
            bool useThisBehavior = true;

            if(!behaviourDescription.appliesToAllMonths && behaviourDescription.month != gameTimeContoller.GetCurrentMonth())
            {
                Debug.Log("Failed Month check " + behaviourDescription.appliesToAllMonths + " " + behaviourDescription.month + " " + gameTimeContoller.GetCurrentMonth());
                useThisBehavior = false;
            }
            if (!behaviourDescription.appliesToAllDays && (behaviourDescription.dayFrom < gameTimeContoller.CurrentDayOfMonth  || gameTimeContoller.CurrentDayOfMonth >behaviourDescription.dayTo))
            {
                Debug.Log("Failed day check " + behaviourDescription.appliesToAllDays + " " + behaviourDescription.dayFrom + " " + behaviourDescription.dayTo + " " + gameTimeContoller.CurrentDayOfMonth);
                useThisBehavior = false;
            }
            if (behaviourDescription.hourFrom < gameTimeContoller.CurrentHour|| gameTimeContoller.CurrentHour > behaviourDescription.hourTo)
            {
                Debug.Log("Failed hour check "  + " " + behaviourDescription.hourFrom + " " + behaviourDescription.hourTo + " " + gameTimeContoller.CurrentHour);
                useThisBehavior = false;
            }

            return useThisBehavior;
        }
    }
}

