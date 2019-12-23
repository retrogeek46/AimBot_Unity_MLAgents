# AimBot_Unity_MLAgents
Training a bot using reinforcement learning to aim and fire at a target.

# Installation
This repo only contains the essential asset files at the moment. To run on local environment you need to first get MLAgents from Unity's official [repo](https://github.com/Unity-Technologies/ml-agents) and install necessary dependencies stated there. After that copy over MLAgents SDK into your local project, install Barracuda and fix references in the scripts and objects. This process is the same as creating a new envrionment, as described [here](https://github.com/Unity-Technologies/ml-agents/blob/164d1ab98efc620b2e8c18e680e5fc99c19d69f1/docs/Learning-Environment-Create-New.md).

# Training 
Training is done on an executable build using Python. More information can be found [here](https://github.com/Unity-Technologies/ml-agents/blob/164d1ab98efc620b2e8c18e680e5fc99c19d69f1/docs/Training-ML-Agents.md). These graphs are for the two different reward functions tried on the below mentioned observation space.

![](https://github.com/retrogeek46/AimBot_Unity_MLAgents/blob/master/Resources/tensorboard_graph.png "The Tensorboard Graphs")

# Environment
![](https://github.com/retrogeek46/AimBot_Unity_MLAgents/blob/master/Resources/aimbot_in_action.gif "The Bot in Action")

Currently the bot is a camera which can rotate on x and y axes. It is placed in a room in which a wall stands with a target appearing randomly. 

* ## Observation Space
    The bot is fed its rotation, the dispalcement vector between itself and the target and the angle between its forward vector and the displacement vector.

* ## Action Space
    The bot can take 3 actions : Move up-down, left-right and fire.

* ## Rewards
    The bot is rewarded for the following tasks with these values:
    * **+100**  : Correctly aiming at the target.
    * **+0.1**  : Moving the crosshair towards the target.
    * **-0.001**  : Moving the crosshair away from the target.
    * **-10**  : Moving the crosshair out of the wall.
    * **-0.001**  : Every frame, to encourage quicker aiming.
    * **-0.5**  : Every time the environment is reset due to time limit.

* ## Reset
    When resetting the environment the target is moved to a new random locaation on the wall and the bot's rotation is set to initial rotation. 
    The environment is reset if 
    * bot's aim/crosshair leaves the wall.
    * target has been hit.
    * fixed amount of time has passed without the above two happening.
    
# Controls
When using heuristics to play around and manually test the environment, the controls to interact with the environmment are as follows
* **Movement**  : WASD keys to aim the bot.
* **Fire**  : F key is used to fire. Currently not usable as target moves as soon as crosshair lines up with it. 
* **Camera**  :
    * **C**  - Change view to individual rooms and cycle between them. Rooms are numbered from 1 to 9.
    * **V**  - Switch back to aerial view showing all the training rooms at once.

# Other Features
There is also a "perfect aimmer" script attached to the gun gameObject (the camera that the bot controls) and it uses Quaternion rotation to perfectly aim at the target. This can be turned on or off by ticking a boolean in the inspector in the PerfectAim component unde the Gun gameobject.

# Current Progress
Currently the Unity environmet and python training funcitonality is working correctly. The bot is taking aim but with quite a bit of inaccuracy. Different reward parameters and values are bieng tried to correctly teach it to aim accurately. Training does not include actually firin at the moment and only aiming is being learned.

9 instances of the environment are run at the same time to train faster.

![](https://github.com/retrogeek46/AimBot_Unity_MLAgents/blob/master/Resources/unity_scene.png "The Unity Scene")
