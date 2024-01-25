using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{

    PlayerMovementController playerMovementController;
    private void Start(){
        playerMovementController = GetComponent<PlayerMovementController>();
        playerMovementController.transformMovement.enable = true;
    }
}
