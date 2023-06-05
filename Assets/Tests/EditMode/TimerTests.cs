using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;

public class TimerTests
{
    [Test]
    public void SetTimerText_ValidTime_TextUpdated()
    {
        // Arrange
        Timer timer = new GameObject().AddComponent<Timer>();
        TextMeshProUGUI textMesh = timer.gameObject.AddComponent<TextMeshProUGUI>();
        timer.timerText = textMesh;
        timer.currentTime = 10f;

        // Act
        timer.SetTimerText();

        // Assert
        Assert.AreEqual("10.00", textMesh.text);
    }

    [Test]
    public void GetTime_ValidTime_ReturnsTime()
    {
        // Arrange
        Timer timer = new GameObject().AddComponent<Timer>();
        timer.currentTime = 20f;

        // Act
        int time = timer.GetTime();

        // Assert
        Assert.AreEqual(20, time);
    }

    [Test]
    public void UpdateTimer_CountDownTrue_DecreasesTime()
    {
        // Arrange
        Timer timer = new GameObject().AddComponent<Timer>();
        timer.countDown = true;
        timer.currentTime = 10f;
        float deltaTime = 0.5f;

        // Act
        timer.UpdateTimer(deltaTime); // Simulate 1 second

        // Assert
        Assert.AreEqual(9.5f, timer.currentTime);
    }

    [Test]
    public void UpdateTimer_CountDownFalse_IncreasesTime()
    {
        // Arrange
        Timer timer = new GameObject().AddComponent<Timer>();
        timer.countDown = false;
        timer.currentTime = 10f;
        float deltaTime = 1f;

        // Act
        timer.UpdateTimer(deltaTime); // Simulate 1 second

        // Assert
        Assert.AreEqual(11f, timer.currentTime);
    }
}
