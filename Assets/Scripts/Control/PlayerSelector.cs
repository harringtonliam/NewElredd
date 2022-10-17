using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerSelector : MonoBehaviour
    {
        private PlayerController selectedPlayer;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                InteractWithPlayerSelection();
            }
        }

        private void InteractWithPlayerSelection()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue))
            {
                if (raycastHit.transform.TryGetComponent<PlayerController>(out PlayerController playerController))
                {
                    PlayerController[] allPlayers = FindObjectsOfType<PlayerController>();
                    foreach (var player in allPlayers)
                    {
                        player.SetSelected(false);
                    }
                    selectedPlayer = playerController;
                    selectedPlayer.SetSelected(true);
                }
            }
        }
    }

}

