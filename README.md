## The problem
The problem of the challenge was to come up with an efficient, working simulation of the direct and cross echoes of a car's ultrasound sensors. The ultrasound signal is a continuous signal, therefore a greedy or iterative algorithm could not have been a good enough solution, so I worked on an analytical solution.

## Playing modes
If you start the game, you can control the car manually (with the DOWN and UP arrow), in which way the "intelligent" braking system won't do anything. However, if you press the Enter button, then the car will start to go backwards, and stop until there is an obstacle behind it. If it has stopped and you move the obstacle, then it can start again (with pressing the Enter again).

## Controls

### Camera navigation
You can move the camera by using the following buttons:
* W - forward
* S - backward
* A -  left
* D - right
* E - up
* Q - down

Besides these, you can rotate the camera, if you are moving your mouse while pressing the right mouse button.

### Obstacle controls
You can move the boxes and cylinders by dragging them with your left mouse button. If you would like to rotate them, press your left Shift button while dragging the object.

### Other
* H - Hide/Unhide GUI
* M - Mute/Unmute (sound effects are from my car's DIY budget sensor :D)

## Technical details
For the rendering, I have used the Unity framework, but for the algorithmic part, I mostly used the basic geometrical and trigonometrical equations.

## Steps of implementation
1. Sitting on the mathematical problem with a paper for a while
2. Trying out different methods in Unity Debug Mode ( = a lot of rays)
3. Coming up with a seemingly working solution
4. Implement it in a well-designed architecture
5. Playing with the UI for too much time
5. Last minute video cutting and description writing

Hi everyone!
My name is Gabor and this is my solution for the software development challenge.
Peter the Parker bot is an ultrasound sensor simulator in a 3D environment. For the rendering, I have used the Unity framework, but for the algorithmic part, I mostly used the basic geometrical and trigonometrical equations.

Firstly, I had to come up with the idea

Peter the Parker Bot, an easy-to-use, scalable simulator for ultrasound sensors in a 3D environment. Feel free to try it out on itch.io (link below).
