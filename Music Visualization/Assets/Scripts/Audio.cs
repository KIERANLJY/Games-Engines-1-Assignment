using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    AudioSource _audioSource;
    public static float[] _samples = new float[512];
    float[] _frequencyBands = new float[8];
    public static float[] _bandsBuffer = new float[8];
    float[] _bufferDecrease = new float[8];
    float[] _highestFreqBand = new float[8];
    public static float[] _ratioBandsBuffer = new float[8];
    public static float _ratioAmplitudeBuffer;
    float _highestAmplitude;

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
        GetBandsBuffer();
        GetRatioBandsBuffer();
        GetRatioAmplitudeBuffer();
        
        if(float.IsNaN(_ratioAmplitudeBuffer))
        {
            _ratioAmplitudeBuffer = 0f;
        }
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
                _sumSpectrum += _samples[_samplesCount] * (_samplesCount + 1);
                _samplesCount ++;
            }
            float _averageSpectrum = _sumSpectrum / _samplesInFB;
            if (i < 3)
            {
                _frequencyBands[i] = _averageSpectrum * 0.4f;
            }
            else
            {
                _frequencyBands[i] = _averageSpectrum * 0.2f;
            }
            
        }
    }

    void GetBandsBuffer()
    {
        for (int i = 0; i < 8; i ++)
        {
            if (_bandsBuffer[i] < _frequencyBands[i])
            {
                _bandsBuffer[i] = _frequencyBands[i];
                _bufferDecrease[i] = 0.0005f;
            }
            if (_bandsBuffer[i] > _frequencyBands[i])
            {
                _bandsBuffer[i] -= _bufferDecrease[i];
                _bufferDecrease[i] *= 1.1f;
            }
        }
    }

    void GetRatioBandsBuffer()
    {
        for (int i = 0; i < 8; i ++)
        {
            if (_highestFreqBand[i] < _frequencyBands[i])
            {
                _highestFreqBand[i] = _frequencyBands[i];
            }
            _ratioBandsBuffer[i] = _bandsBuffer[i] / _highestFreqBand[i];
        }
    }

    void GetRatioAmplitudeBuffer()
    {
        float _amplitude = 0;
        float _amplitudeBuffer = 0;
        for (int i = 0; i < 8; i ++)
        {
            _amplitude += _frequencyBands[i];
            _amplitudeBuffer += _bandsBuffer[i];
        }
        if (_highestAmplitude < _amplitude)
        {
            _highestAmplitude = _amplitude;
        }
        _ratioAmplitudeBuffer = Mathf.Clamp01(_amplitudeBuffer / _highestAmplitude);
    }
}
