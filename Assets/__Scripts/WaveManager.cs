using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public Slider waveBar;
    public TextMeshProUGUI waveClearedText;
    public TextMeshProUGUI waveNumberText;

    private int currentWave = 1;
    private bool waveCleared = false;

    private float waveDuration = 20f;
    private float waveStartTime;

    private void Start()
    {
        // Initialize the UI elements and start the first wave
        UpdateUI();
        StartWave(currentWave);
    }

    private void Update()
    {
        if (waveCleared)
        {
            // Check if it's time to proceed to the next wave
            if (Time.time >= waveClearedTime + 5f)
            {
                waveCleared = false;
                StartNextWave();
            }
        }
        else
        {
            float elapsedTime = Time.time - waveStartTime;
            waveBar.value = Mathf.Clamp01(elapsedTime / waveDuration);

            if (elapsedTime >= waveDuration)
            {
                // The current wave took too long, so mark it as cleared
                waveCleared = true;
                waveClearedTime = Time.time;
                waveClearedText.gameObject.SetActive(true);
            }
        }
    }

    private void StartWave(int waveNumber)
    {
        // Implement logic to start a new wave
        // For example, you can spawn enemies and other wave-specific actions here.
        waveStartTime = Time.time;
        waveBar.value = 0;
        waveClearedText.gameObject.SetActive(false);
    }

    private void StartNextWave()
    {
        currentWave++;
        UpdateUI();
        StartWave(currentWave);
    }

    private void UpdateUI()
    {
        waveNumberText.text = "Wave " + currentWave;
        waveClearedText.gameObject.SetActive(false);
    }

    private float waveClearedTime; // Time when the wave was cleared
}
