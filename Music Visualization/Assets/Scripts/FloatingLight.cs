using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FloatingLight : MonoBehaviour
{
    Light _light;

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        // Change intensity of the light according to amplitude of the audio source
        _light.intensity = Audio._ratioAmplitudeBuffer;
    }
}
