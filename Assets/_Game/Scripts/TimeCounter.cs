using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// General Purpose Time Counter
/// </summary>
public class TimeCounter
{
    #region Properties
    public float TotalTime { get; private set; }
    private float timeLeft = 0;
    #endregion
    #region Constructor
    public TimeCounter(float totaltime)
    {
        this.TotalTime = totaltime;
        this.timeLeft = this.TotalTime;
    }
    #endregion
    #region Public Methods
  
    public void ResetTimer()
    {
        this.timeLeft = TotalTime;
    }

    public void SetTimer(float time)
    {
        this.timeLeft = time;
    }

    public bool IsTickFinished(float deltatime)
    {
        if (this.timeLeft > 0)
        {
            this.timeLeft -= deltatime;
            return false;
        }
        else
        {
            this.timeLeft = this.TotalTime; // reset Timer
        }
        return true;
    }


  
    public float GetTimeLeft()
    {
        return this.timeLeft;
    }

  
    #endregion
}