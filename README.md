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

![](images/Script3_loc1.png)

### Build the project for Linux platform
Open Player Settings (menu: **Edit** > **Project Settings** > **Player**).

Under **Resolution and Presentation**:
   - Ensure that **Run in Background** is checked.

Open the Build Settings window (menu:**File** > **Build Settings**).

Select **Linux** as Target Platform 
   - (Optional) **Development Build** is checked. 

![Unity Hub_3](images/Build1.png)

Press **Build And Run**

![Unity Hub_4](images/Build2.png)

We name a collection of built files as **RollerBall_build_000**.

Here is how the built files look like in the **RollerBall** folder. 

![Unity Hub_5](images/Build3.png)

Copy the flowing files and folder and upload them to S3. 
   - LinuxPlayer_s.debug	
   - RollerBall_build_000_Data/	
   - RollerBall_build_000.x86_64	
   - UnityPlayer_s.debug	
   - UnityPlayer.so

![Unity Hub_6](images/S3.png)

Please take note of S3 URI. You are going to need that for **rl_unity_cloud_simulation_sample.ipynb** 

## Amazon SageMaker RL
### Amazon SageMaker RL on SageMaker notebook
Start **Amazon SageMaker** and create **Notebook Instance**. (menu:**Notebook** > **Notebook instance** > **Create notebook instance**).
   - t3.xlarge instance is adequate. 
	
Click **Open Jupyter** and select **SageMaker Examples**

SageMaker provides dozens of sample notebooks. You can find a samle notebook named **rl_unity_ray** under **Reinforcement Learning** tab. 

![SageMaker_1](SageMakerExample1.png)

Select **rl_unity_ray** and create a directry.

```
/home/ec2-user/SageMaker/rl_unity_ray_YYYY-MM-DD
```

Go to **rl_unity_ray_YYYY-MM-DD**

![SageMaker_1](SageMakerRLUnityRay.png)

Upload following files from the original repo.
   - rl_unity_cloud_simulation_sample.ipynb
   - src/evaluate-unity.py
   - src/train-unity.py

### Proceed rl_unity_cloud_simulation_sample.ipynb







