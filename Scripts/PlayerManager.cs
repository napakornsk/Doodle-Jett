using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;


public class PlayerManager : MonoBehaviour
{
    [ReadOnly] [BoxGroup("Player Data")] MovementController playerMovementController;
    [ReadOnly] [BoxGroup("Player Data")] CollisionHandler playerCollisionHandler;

    [BoxGroup("Fuel Data")] [SerializeField] FuelData fuelData;
    [BoxGroup("Fuel Data")] public Image fuelBarImage;
    [BoxGroup("Fuel Data")] public GameObject ranOutOfFuelPanel;

    [BoxGroup("Scene Manager Data")] [SerializeField] float noFuelLoadLevelDelay;

    private void Awake() {
        playerMovementController = FindObjectOfType<MovementController>();
        playerCollisionHandler = FindObjectOfType<CollisionHandler>();
        fuelData.currentFuel = fuelData.maxFuel;
    }

    private void Update() {
        CheckFuel();
        UpdateFuel();
    }

    void UpdateFuel()
    {
        if (playerMovementController.isThrusting == true)
        {
            fuelData.currentFuel -= fuelData.usedRate * Time.deltaTime;
        }

        UpdateFuelBar();

        if (fuelData.currentFuel > fuelData.maxFuel)
        {
            fuelData.currentFuel = fuelData.maxFuel;
        }
    }

    private void UpdateFuelBar()
    {
        fuelBarImage.fillAmount = fuelData.currentFuel;
    }

    void CheckFuel(){
        if (fuelData.currentFuel <= 0){
            ranOutOfFuelPanel.SetActive(true);
            LeanTween.alpha(ranOutOfFuelPanel, 1f, 1f); 
            playerMovementController.enabled = false;
            playerCollisionHandler.Invoke("ReloadLevel", noFuelLoadLevelDelay);
        }
    }
}
