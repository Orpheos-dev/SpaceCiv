using UnityEngine;
using System.Collections.Generic;

public class OptionGenerator : MonoBehaviour
{
    [System.Serializable]
    public class PhaseOptions
    {
        public string phaseName;
        public List<GameObject> options;
        public List<string> optionNames; // New variable to store option names
    }

    public List<PhaseOptions> phases;
    private int currentPhase = 0;

    // Flag to prevent duplicate calls to NextPhase
    private bool actionConfirmed = false;

    public void GenerateOptions()
    {
        // Hide all options
        foreach (var phase in phases)
        {
            foreach (var option in phase.options)
            {
                option.SetActive(false);
            }
        }

        // Activate current phase options
        if (currentPhase < phases.Count)
        {
            for (int i = 0; i < phases[currentPhase].options.Count; i++)
            {
                phases[currentPhase].options[i].SetActive(true);
                Debug.Log("Option: " + (i < phases[currentPhase].optionNames.Count ? phases[currentPhase].optionNames[i] : "Unnamed"));
            }
        }
    }

    public void NextPhase()
    {
        if (actionConfirmed)
        {
            if (currentPhase < phases.Count - 1)
            {
                currentPhase++;
                GenerateOptions();
                actionConfirmed = false;  // Reset the actionConfirmed flag after phase transition
            }
            else
            {
                Debug.Log("Final Phase Reached");
            }
        }
    }

    public void ResetPhases()
    {
        currentPhase = 0;
        GenerateOptions();
    }

    // Use this method to confirm that a phase change has been triggered
    public void ConfirmPhaseChange()
    {
        actionConfirmed = true;
    }
}
