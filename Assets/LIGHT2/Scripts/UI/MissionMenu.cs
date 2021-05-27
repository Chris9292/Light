using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionMenu : MonoBehaviour
{
    public TMP_Text missionTitle;
    public TMP_Text missionDescription;
    public Image missionImage;

    enum Mission { Repair, Sampling}
    Mission currentMission;

    [System.Serializable]
    public struct MissionStep
    {
        public string description;
        public Sprite image;
    }
    public MissionStep[] RepairSteps;
    public MissionStep[] SamplingSteps;

    int currentStep = 0;

    private void Start()
    {
        currentMission = Mission.Repair;
        SetStepInfo();
    }

    public void ChangeMission()
    {
        if (currentMission == Mission.Repair)
            currentMission = Mission.Sampling;
        else if (currentMission == Mission.Sampling)
            currentMission = Mission.Repair;

        currentStep = 0;
        SetStepInfo();
    }

    public void NextStep()
    {
        if (currentMission == Mission.Repair)
        {
            if (currentStep < RepairSteps.Length - 1)
                currentStep += 1;
        }
        else if (currentMission == Mission.Sampling)
        {
            if (currentStep < SamplingSteps.Length - 1)
                currentStep += 1;
        }
        SetStepInfo();
    }

    public void PreviousStep()
    {
        if (currentMission == Mission.Repair)
        {
            if (currentStep > 0)
                currentStep -= 1;
        }
        else if (currentMission == Mission.Sampling)
        {
            if (currentStep > 0)
                currentStep -= 1;
        }
        SetStepInfo();
    }

    void SetStepInfo()
    {
        if (currentMission == Mission.Repair)
        {
            missionTitle.text = "Repair (" + (currentStep + 1).ToString() + "/" + RepairSteps.Length.ToString() + ")";
            missionDescription.text = RepairSteps[currentStep].description;
            missionImage.sprite = RepairSteps[currentStep].image;
        }
        else if (currentMission == Mission.Sampling)
        {
            missionTitle.text = "Sampling (" + (currentStep + 1).ToString() + "/" + SamplingSteps.Length.ToString() + ")";
            missionDescription.text = SamplingSteps[currentStep].description;
            missionImage.sprite = SamplingSteps[currentStep].image;
        }
    }
}
