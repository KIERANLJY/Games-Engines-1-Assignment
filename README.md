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
