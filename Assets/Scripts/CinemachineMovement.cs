using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineMovement : MonoBehaviour
{

    public static CinemachineMovement Instance;
    private CinemachineVirtualCamera cinemachineVirtualCamera; 
    private CinemachineBasicMultiChannelPerlin cinemachineMultiChannelPerlin;
    private float timeMovement;
    private float totalTimeMovement;
    private float intensityInicial;


    private void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachineMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();    
    }



    public void MoveCamera(float intensity,float frequency, float time)
    {
        cinemachineMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineMultiChannelPerlin.m_FrequencyGain = frequency;
        intensityInicial = intensity;
        totalTimeMovement = time;
        timeMovement = time;
    }

    private void Update()
    {
        if(timeMovement > 0)
        {
            timeMovement -= Time.deltaTime;
            cinemachineMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(intensityInicial, 0, 1 - (timeMovement / totalTimeMovement));
        }
    }
}
