using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class TutorialFinished : MonoBehaviour
{
    public static event Action OnTutorialFinished;
    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        OnTutorialFinished?.Invoke();
    }
}
