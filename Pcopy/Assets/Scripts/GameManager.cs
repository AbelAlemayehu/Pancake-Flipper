using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string pancakeStatus = "Raw";
    public int totalPancakes = 0;
    public string lastQuality = "None";

    private Pancake[] pancakes;
    private HashSet<Pancake> countedPancakes = new HashSet<Pancake>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadGameState();
    }

    void Update()
    {
        pancakes = FindObjectsOfType<Pancake>();

        if (pancakes.Length == 0)
        {
            pancakeStatus = "Raw";
            return;
        }

        foreach (var p in pancakes)
        {
            float cookTime = p.cookTime - p.batterDelay;

            if (!p.isCooking)
                pancakeStatus = "Raw";
            else if (cookTime < 0)
                pancakeStatus = "Batter";
            else if (cookTime < p.perfectTime)
            {
                pancakeStatus = "Cooking";
                lastQuality = "Undercooked";
            }
            else if (cookTime < p.burntTime)
            {
                pancakeStatus = "Flipped";
                lastQuality = "Perfect";

                // Count only once per pancake
                if (!countedPancakes.Contains(p))
                {
                    totalPancakes++;
                    countedPancakes.Add(p);
                }
            }
            else
            {
                pancakeStatus = "Burnt";
                lastQuality = "Burnt";

                if (!countedPancakes.Contains(p))
                {
                    totalPancakes++;
                    countedPancakes.Add(p);
                }
            }
        }

        // --- Keyboard shortcuts for Save/Load ---
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveGameState();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadGameState();
        }
    }

    public void IncrementTotal()
    {
        totalPancakes++;
    }

    public void SaveGameState()
    {
        SaveSystem.SaveGame(pancakeStatus, totalPancakes, lastQuality);
        Debug.Log($"Saved: {pancakeStatus}, {totalPancakes}, {lastQuality}");
    }

    public void LoadGameState()
    {
        var data = SaveSystem.LoadGame();
        if (data != null)
        {
            pancakeStatus = data.pancakeStatus;
            totalPancakes = data.totalPancakes;
            lastQuality = data.lastQuality;
            Debug.Log($"Loaded: {pancakeStatus}, {totalPancakes}, {lastQuality}");
        }
    }
}
