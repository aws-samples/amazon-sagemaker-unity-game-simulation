# unity-roller-ball-simulation
This is a sample unity project for Unity Game Simulation with ML-Agents running on Amazon SageMaker RL.

## Preparation
### Download this repo
First, download a zip file from the repo and open it. 

![Github repo](images/DownloadRepo.png)

You will find a folder called **unity-roller-ball-simulation-master**.

### Unity
Start **Unity Hub**. In **Projects** window, press **Add** and select **RollerBall** folder in the folder described above. 

![Unity Hub_0](images/AddProject.png)

You may need to download the version 2019.3.14.f1 version of Unity with Linux support module. 

This is how the sample game, **RollerBall** looks like. 

![Unity Hub_1](images/RollerBall1.png)
![Unity Hub_3](images/RollerBall3.png)

There are three important scripts, ObstacleManager.cs, RollerAgent.cs, and RollerBallConfig.json.

![](images/Script1.png)
![](images/Script2.png)
![](images/Script3.png)

RollerBallConfig.json is found in Resources/Congig/. 

![](images/Script3_loc2.png)

### Build the project for Linux platform
Open Player Settings (menu: **Edit** > **Project Settings** > **Player**).

Under **Resolution and Presentation**:
   - Ensure that **Run in Background** is checked.

Open the Build Settings window (menu:**File** > **Build Settings**).

Select **Linux** as Target Platform 
   - (Optional) **Development Build** is checked. 

![Unity Hub_3](images/Build1.png)

Press **Build And Run**

![Unity Hub_3](images/Build2.png)

We name the sample collection of built files as **RollerBall_build_000**.

Here is how the built files look like in the **RollerBall** folder. 

![Unity Hub_3](images/Build3.png)

Upload built files to S3. 

![Unity Hub_3](images/S3.png)


