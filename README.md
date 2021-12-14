# 3D Music Visualizer
Name: Jiyuan Liu

Student Number: D17129141

Class Group: TU856

# Description of the project
This will be a 3D project implemented with colorful 3D objects, which will move following the beats of the music.

# Instructions for use
The project will play a music automatically and users can see the visualization of the music.

# How it works
The project contains several sections.  In the middle of the scene, there are 128 cubes in a circle rotating around the center of the circle.  These cubes change their heights according to the spectrum samples of the music.  Inside the circle, there are 8 cubes changing their heights and colors according to the spectrum frequency bands of the music.  On the top left and top right of the scene, there are two floating objects.  In each oject, there is a sphere in the middle and 8 spheres in a circle moving around it.  All these spheres will change their scales according to the amplitude of the music.  And the 8 spheres can also change color according to the amplitude.  Besides, there are 3 spot lights implemented and they are also affected by the amplitude of the music.  And a particle system is added into the project.

For dealing with the audio source, I got 512 spectrum samples using GetSpectrumData function first.  Parameters in this function are sample array, channel, and FFTWindow type respectively.  Here is the code:
```
_audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
```
Then I also got 8 frequency bands of the audio source according to frequency subsets ranges.  I went through the 8 frequency bands using a for loop.  The numbers of samples in each band are 2, 4, 8, 16, 32, 64, 128, 258 respectively, which can be calculated by power of 2.  I stored the numbers of samples in a variable called _samplesInFB.  I calculated the average spectrum values of samples in the bands.  Here is the code:
```
int _samplesCount = 0;
for (int i = 0; i < 8; i ++)
{
    int _samplesInFB = (int)Mathf.Pow(2, i + 1);
    for (int j = 0; j < _samplesInFB; j ++)
    {
        _sumSpectrum += _samples[_samplesCount];
        _samplesCount ++;
    }
    float _averageSpectrum = _sumSpectrum / _samplesInFB;
}
```
After getting the frequency bands, I would like to make the value change smoothly.  So I created bands buffers based on the frequeny bands.  If the value of band buffer is lower than band, it will reach the higher value in an instant.  But if the value of band buffer is higher, which means it should be decreased, a variable called _bufferDecrease will control the speed of decreasing.  The value of this varible will increase and the speed of decreasing will become faster.  Here is the code:
```
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
```
Besides, I also calculated the amplitude of the audio source by adding values of 8 frequency bands together.

For instantiate objects in a circle, I calculation the position of each object by trigonometric.  First, I got the angle of one object by diving 2 PI by the number of objects.  Then I get the position of the object by the angle and radius of the circle.  Here is the sample code for instantiating 128 cubes in a circle with radius of 30:
```
float _theta = (2.0f * Mathf.PI) / (float)128;
for (int i = 0; i < 128; i ++)
{
    GameObject _instanceCube = (GameObject)Instantiate(_cubePrefab);
    float _angle = _theta * i;
    _instanceCube.transform.position = new Vector3(Mathf.Sin(_angle) * 30, 0, Mathf.Cos(_angle) * 30);
}
```
In order to achieve objects moving follow the audio, I changed the scale or color of the objects based on the public variables that I created before in audio script file.  The spot lights I created are also affected by the audio.  Here is the sample code for changing scale of a sphere based on change of amplitude:
```
_bodySphere.transform.localScale = new Vector3((Audio._ratioAmplitudeBuffer + 1) * 5, (Audio._ratioAmplitudeBuffer + 1) * 5, (Audio._ratioAmplitudeBuffer + 1) * 5);
```

In addition to the code part, I also did something else to implement the project.  First, I created a cube object in Blender.  The cube is different from the normal cubes because the pivot of the cube is in the middle of its bottom, which means it can only move upwards when the height changes.  I also created some materials.  Among them, I created a 6 sides skybox material to implement the background.  And I created a particle system to make the project look better.

# List of classes/assets in the project
| Class/assets | Source |
| --- | --- |
| Audio.cs | Modified from [reference](https://www.youtube.com/watch?v=4Av788P9stk&list=PL3POsQzaCw53p2tA6AWf7_AWgplskR0Vo) |
| InstantiateStage.cs | Self written |
| InstantiateCircleCubes.cs | Self written |
| InstantiateMiddleCubes.cs | Modified from [reference](https://www.youtube.com/watch?v=mHk3ZiKNH48&list=PL3POsQzaCw53p2tA6AWf7_AWgplskR0Vo) |
| InstantiateFloatingObj.cs | Self written |
| MainLight.cs | Self written |
| FloatingLight.cs | Self written |

# References
1. https://www.youtube.com/watch?v=_ojeeuNtJM8
2. https://www.youtube.com/watch?v=4Av788P9stk&list=PL3POsQzaCw53p2tA6AWf7_AWgplskR0Vo
3. https://www.youtube.com/watch?v=mHk3ZiKNH48&list=PL3POsQzaCw53p2tA6AWf7_AWgplskR0Vo
4. https://docs.unity3d.com/560/Documentation/Manual/HOWTO-UseSkybox.html
5. https://learn.unity.com/tutorial/introduction-to-particle-systems#

# What I am most proud of in the assignment
The part that I am most proud of the project is the cubes in the middle part.  The 8 cubes can move and also change color according to frequency bands of the music.  It is also a little bit tricky to calculate the values of bands based on 512 samples and adjust the values to make it look better.



# Youtube video of the assignment
https://youtu.be/lsN_yk4Oh3s

