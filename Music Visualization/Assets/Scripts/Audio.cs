using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    AudioSource _audioSource;
    // Samples of spectrum data
    public static float[] _samples = new float[512];
    // Frequency bands and buffers
    float[] _frequencyBands = new float[8];
    public static float[] _bandsBuffer = new float[8];
    float[] _bufferDecrease = new float[8];
    float[] _highestFreqBand = new float[8];
    public static float[] _ratioBandsBuffer = new float[8];
    // Amplitude and buffer
    float _highestAmplitude;
    public static float _ratioAmplitudeBuffer;

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
        
        // Avoid _ratioAmplitudeBuffer being NaN in first frame
        if(float.IsNaN(_ratioAmplitudeBuffer))
        {
            _ratioAmplitudeBuffer = 0f;
        }
    }

    // Get 512 samples of spectrum data from the audio source
    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    // Divide samples into 8 frequenct subsets according to frequency range
    void GetFrequencyBands()
    {
        int _samplesCount = 0;
        for (int i = 0; i < 8; i ++)
        {
            // The numbers of samples in frequency bands are 2, 4, 8, 16, 32, 64, 128, 258 separately
            int _samplesInFB = (int)Mathf.Pow(2, i + 1);
            // Set the number of samples in the last frequency bands as 258
            if (i == 7)
            {
                _samplesInFB += 2;
            }

            float _sumSpectrum = 0;
            // Calculate and adjust the total spectrum value of the each frequency band
            for (int j = 0; j < _samplesInFB; j ++)
            {
                _sumSpectrum += _samples[_samplesCount] * (_samplesCount + 1);
                _samplesCount ++;
            }
            // Set the spectrum values of frequency bands as average spectrum values of the samples
            float _averageSpectrum = _sumSpectrum / _samplesInFB;
            // Adjust spectrum values of first few bands and last few bands to make it looks even
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

    // Get the buffer values of the 8 frequency bands and make the values change smoothly
    void GetBandsBuffer()
    {
        for (int i = 0; i < 8; i ++)
        {
            // Set the value of buffer as the value of band
            if (_bandsBuffer[i] < _frequencyBands[i])
            {
                _bandsBuffer[i] = _frequencyBands[i];
                _bufferDecrease[i] = 0.0005f;
            }
            // Decrease the value of buffer to the value of band smoothly
            if (_bandsBuffer[i] > _frequencyBands[i])
            {
                _bandsBuffer[i] -= _bufferDecrease[i];
                _bufferDecrease[i] *= 1.1f;
            }
        }
    }

    // Get ratios of the 8 band buffer values between 0 and 1
    void GetRatioBandsBuffer()
    {
        for (int i = 0; i < 8; i ++)
        {
            // Get highest value of frequency band
            if (_highestFreqBand[i] < _frequencyBands[i])
            {
                _highestFreqBand[i] = _frequencyBands[i];
            }
            // Get ratio of frequency band
            _ratioBandsBuffer[i] = _bandsBuffer[i] / _highestFreqBand[i];
        }
    }

    // Get the ratio Amplitude buffer value between 0 and 1
    void GetRatioAmplitudeBuffer()
    {
        float _amplitude = 0;
        float _amplitudeBuffer = 0;
        // Get value of amplitude and amplitude buffer
        for (int i = 0; i < 8; i ++)
        {
            _amplitude += _frequencyBands[i];
            _amplitudeBuffer += _bandsBuffer[i];
        }
        // Get highest value of amplitude
        if (_highestAmplitude < _amplitude)
        {
            _highestAmplitude = _amplitude;
        }
        // Get ratio of amplitude buffer
        _ratioAmplitudeBuffer = _amplitudeBuffer / _highestAmplitude;
    }
}
