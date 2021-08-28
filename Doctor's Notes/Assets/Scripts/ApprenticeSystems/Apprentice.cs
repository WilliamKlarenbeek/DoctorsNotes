using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apprentice : MonoBehaviour
{
    [SerializeField] GameObject apprentice;
    [SerializeField] public float maximumExperience;
    [SerializeField] public float minimumExperience;

    #region Singleton class: Apprentice

    public static Apprentice Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        UpdateApprenticeScale();
    }

    public void UpdateApprenticeScale()
    {
        ApprenticeDataManager.ScaleApprentice(apprentice);
    }
}
