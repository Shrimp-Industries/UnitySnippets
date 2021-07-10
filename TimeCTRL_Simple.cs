using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// --- AUTHOR ---
// Luke Kabat, 2021
//
// --- DESCRIPTION ---
// Simple static class that can be used to pause/unpause physics timescale safely (from various scripts simultaneously).
// It also allows to set timescale to any value and gradually increase it to normal value with time.
// It's a simple static class, could easilly be transformed into singleton pattern object.
//
// --- PUBLIC PARAMETERS TO SET IN EDITOR ---
//      NONE
//
// USECASES:
// 1. Use TryPauseTimescale() and UnpauseTimescale() to pause and unpause (useful for menus, inventories, etc.).
// 2. Use SetCurrentTimescale(float newTimescale) and then SetNormalizeTimeSpeed(float newNormalizeTimeSpeed) to slowdown time to newTimescale and gradually
// increase it back to 1.0f with newNormalizeTimeSpeed.
// ex.: 
// TimeCTRL.SetCurrentTimescale(0.5f);
// TimeCTRL.SetNormalizeTimeSpeed(0.002f);

public class TimeCTRL_Simple : MonoBehaviour
{

    private static float currentTimescale = 1.0f;
    private static float savedTimescale = 1.0f;
    private static bool isTimescaleLocked = false;
    private static float normalizeTimeSpeed = 0.0f;
    private static bool isSpeedNormalizationLocked = false;

    public static float GetCurrentTimescale() {
        return currentTimescale;
    }

    public static void SetCurrentTimescale(float newTimescale) {
        if (!isTimescaleLocked) {
            currentTimescale = newTimescale;
            Time.timeScale = currentTimescale;
            Time.fixedDeltaTime = 0.02F * currentTimescale;
        }
    }

    public static bool CanPauseTimescale() {
        return isSpeedNormalizationLocked;
    }
    public static bool TryPauseTimescale() {
        if (CanPauseTimescale()) {
            PauseTimescale();
        } else {
            return false;
        }
        return true;
    }
    public static void PauseTimescale() {
        isSpeedNormalizationLocked = false;
        isTimescaleLocked = true;
        savedTimescale = currentTimescale;
        currentTimescale = 0.0f;
        Time.timeScale = currentTimescale;
    }

    public static void UnpauseTimescale() {
        isSpeedNormalizationLocked = true;
        isTimescaleLocked = false;
        currentTimescale = savedTimescale;
        Time.timeScale = currentTimescale;
        Time.fixedDeltaTime = 0.02F * currentTimescale;
    }

    public static void SetNormalizeTimeSpeed(float newNormalizeTimeSpeed) {
        normalizeTimeSpeed = newNormalizeTimeSpeed;
        isSpeedNormalizationLocked = true;
    }

    private void FixedUpdate() {
        if (isSpeedNormalizationLocked) {
            if (currentTimescale < 1.0f && normalizeTimeSpeed > 0.0f) {
                currentTimescale += normalizeTimeSpeed;
                Time.timeScale = currentTimescale;
                Time.fixedDeltaTime = 0.02F * currentTimescale;
            } else if (normalizeTimeSpeed > 0.0f) {
                normalizeTimeSpeed = 0.0f;
            }
        }
    }
}
