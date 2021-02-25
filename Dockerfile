ARG CPU_OR_GPU
ARG AWS_REGION
FROM 462105765813.dkr.ecr.${AWS_REGION}.amazonaws.com/sagemaker-rl-ray-container:ray-0.8.5-tf-${CPU_OR_GPU}-py36

WORKDIR /opt/ml

# Unity dependencies

RUN pip install --upgrade \
    pip \
    gym-unity \
    mlagents-envs

RUN pip install sagemaker-containers --upgrade

ENV PYTHONUNBUFFERED 1

ENV LD_LIBRARY_PATH=/usr/lib/x86_64-linux-gnu:$LD_LIBRARY_PATH

RUN apt update && apt install gdebi-core -y
RUN apt-get --purge remove xvfb -y
RUN wget http://security.ubuntu.com/ubuntu/pool/main/libx/libxfont/libxfont1_1.5.1-1ubuntu0.16.04.4_amd64.deb && \
  yes | gdebi libxfont1_1.5.1-1ubuntu0.16.04.4_amd64.deb
  
RUN  wget http://security.ubuntu.com/ubuntu/pool/universe/x/xorg-server/xvfb_1.18.4-0ubuntu0.11_amd64.deb && \
  yes | gdebi xvfb_1.18.4-0ubuntu0.11_amd64.deb

############################################
# Test Installation
############################################
# Test to verify if all required dependencies installed successfully or not.
RUN python -c "import gym;import sagemaker_containers.cli.train;import ray; from sagemaker_containers.cli.train import main; from mlagents_envs.environment import UnityEnvironment; from mlagents_envs.registry import default_registry; from gym_unity.envs import UnityToGymWrapper"

# Make things a bit easier to debug
WORKDIR /opt/ml/code

ADD entrypoint.sh /entrypoint.sh
RUN ["chmod", "+x", "/entrypoint.sh"]
ENTRYPOINT ["/entrypoint.sh"]
