# BTDF Standard Extension
This implementation of BTDF targets BizTalk Standard installation with its limitation of 5 Application.

What it does is to add one or more integrations in one application. These are deployed and undeployd in such a maner that it they do not affect each other.
This allows one to create VS solutions with an Integration scope instead of creating on big solution file.

## IntegrationTask

Executable IntegrationTask replaces BizTalkDeploymentFramework.Tasks.BizTalk regarding _Stopping, Removing and Starting_ each individual Integrations.

## Configuration

Bellow configuarion must added 
- One existing BTDF property, _BiztalkAppName_, must be set to the specific Application being deployed to
- A new property, _Integration_, must be added that contains a specific integration prefix.

![image](https://user-images.githubusercontent.com/17280237/143480295-6eac9b68-1d04-47f8-8bd1-58e86ee684a6.png)

All artifacts and ports must be prefixed with its specific integration ID.

Artifacts

![image](https://user-images.githubusercontent.com/17280237/143480872-2c2857b7-3128-4fa5-befe-234e7cae6f86.png)

Ports

![image](https://user-images.githubusercontent.com/17280237/143480951-34365c97-978d-404f-a866-2c1d60c65a8f.png)


## Standard Targets File

To simplyfy usage a specific targets file has been created, modified slightly from the standard targets file. This needs to be updated in the Deployment.btdfproj.

![image](https://user-images.githubusercontent.com/17280237/143479553-87e4e494-9022-471a-8e5a-1f150d7ab677.png)

## Binding Files

Each integration is created with its own bindingfile

![image](https://user-images.githubusercontent.com/17280237/143480069-9bded4b9-382b-42ef-a18a-e86350631da3.png)

## Filter

Thanks to MS we can filter specific integration artifacts and ports using the integration prefix

![image](https://user-images.githubusercontent.com/17280237/143482688-1e5369ab-19ae-4953-970e-50d11144a51a.png)

## Installation

1. Follow instructions to install  [BTDF](https://github.com/BTDF/DeploymentFramework)

2. Copy BizTalkDeploymentFramework.Standard.targets to the BTDF specific MSBuild folder %programfiles%\MSBuild\DeploymentFrameworkForBizTalk\5.0
![image](https://user-images.githubusercontent.com/17280237/143490848-3f76d5cc-6c02-49ff-b2d1-6759d1f9b1a7.png)

3. Copy IntegrationTask.exe to the DeployTools folder
![image](https://user-images.githubusercontent.com/17280237/143491052-84c8cb49-a29d-4e39-a791-5743072b115b.png)






