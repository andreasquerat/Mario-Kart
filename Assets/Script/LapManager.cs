using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System.Collections;

public class LapManager : MonoBehaviour
{
    private int _lapNumber;
    private List<CheckPoint> _checkpoints;
    private int _numberOfCheckpoints;

    [SerializeField] private TMP_Text Lap;

    private void Start()
    {
        _numberOfCheckpoints = FindObjectsByType<CheckPoint>(FindObjectsSortMode.None).Length;
        _checkpoints = new List<CheckPoint>();
        Lap.text = $"LAP : {_lapNumber} / 3";
    }

    public void AddCheckPoint(CheckPoint checkPointToAdd)
    {
        if (checkPointToAdd.isFinishLine)
        {
            FinishLap();
        }

        if (_checkpoints.Contains(checkPointToAdd) == false)
        {
            _checkpoints.Add(checkPointToAdd);
        }
    }

    private void FinishLap()
    {
        if (_checkpoints.Count > _numberOfCheckpoints / 2)
        {
            _lapNumber++; 
            Lap.text = $"LAP : {_lapNumber} / 3";
            _checkpoints.Clear();
            if (_lapNumber >= 3)
            {
                Debug.Log("Gg WP");
            }
        }
    }
}