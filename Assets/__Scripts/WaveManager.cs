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
                DestroyAllEnemies(); // Destroy all enemies
                StartNextWave();
            }
        }
        else if (Time.time >= waveStartTime + 20f)
        {
            //if wave go overtime
            waveCleared = true;
            waveClearedTime = Time.time;
            waveClearedText.gameObject.SetActive(true);
            DestroyAllEnemies(); // Destroy all enemies
        }

        // Update the wave progress bar
        float waveProgress = Mathf.Clamp01((Time.time - waveStartTime) / 20f);
        waveBar.value = waveProgress;
    }

    private void StartWave(int waveNumber)
    {
        
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

    private void DestroyAllEnemies()
    {
        // Find all enemies in the scene and destroy them
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }
}
