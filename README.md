# AimBot_Unity_MLAgents
Training a bot using reinforcement learning to aim and fire at a target.

# Installation
This repo only contains the essential asset files at the moment. To run on local environment you need to first get MLAgents from Unity's official [repo](https://github.com/Unity-Technologies/ml-agents) and install necessary dependencies stated there. After that copy over MLAgents SDK into your local project, install Barracuda and fix references in the scripts and objects. This process is the same as creating a new envrionment, as described [here](https://github.com/Unity-Technologies/ml-agents/blob/164d1ab98efc620b2e8c18e680e5fc99c19d69f1/docs/Learning-Environment-Create-New.md).

# Training 
Training is done on an executable build using Python. More information can be found [here](https://github.com/Unity-Technologies/ml-agents/blob/164d1ab98efc620b2e8c18e680e5fc99c19d69f1/docs/Training-ML-Agents.md).

# Environment
Currently the bot is a camera which can rotate on x and y axes. It is placed in a room in which a wall stands with a target appearing randomly. 

* ## Observation Space
    The bot is fed its forward vector and the dispalcement vector between itself and the target. Rewards are given for reducing the angle between those two vectors and succesfully hitting the target

* ## Action Space
    The bot can 3 actions. Move up-down, left-right and fire.

* ## Rewards
    The bot is rewarded for reducing the angle between itself and the target displacement vector and for successfully hitting the target

* ## Reset
    The environment is reset if 
    * bot's aim/crosshair leaves the wall
    * target has been hit
    * fixed amount of time has passed
    
# Current Progress
Currently the Unity environmet and python training funcitonality is working correctly. The actual training of the bot results in it taking random actions. More work is needed for the bot to correctly learn how to aim.
