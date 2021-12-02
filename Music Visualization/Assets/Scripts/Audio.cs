using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    AudioSource _audioSource;
    public static float[] _samples = new float[512];
    public static float[] _frequencyBands = new float[8];

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        GetFrequencyBands();
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void GetFrequencyBands()
    {
        int _samplesCount = 0;
        for (int i = 0; i < 8; i ++)
        {
            int _samplesInFB = (int)Mathf.Pow(2, i + 1);
            if (i == 7)
            {
                _samplesInFB += 2;
            }

            float _sumSpectrum = 0;
            for (int j = 0; j < _samplesInFB; j ++)
            {
                _sumSpectrum += _samples[_samplesCount];
                _samplesCount ++;
            }
            float _averageSpectrum = _sumSpectrum / _samplesInFB;
            _frequencyBands[i] = _averageSpectrum * 10;
        }
    }
}
