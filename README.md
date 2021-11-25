# BTDF-Standard
BTDF for BizTalk Standard 

This implementation of BTDF targets BizTalk Standard installation with its limitation of 5 Application.

What it does is to add one or more integrations in one application. These are deployed and undeployd in such a maner that it they do not affect each other.

**IntegrationTask**

Executable IntegrationTask replaces BizTalkDeploymentFramework.Tasks.BizTalk regarding Stopping, Removing and Starting each individual Integration.

**Configuration**

Bellow configuarion must added 
- One existing BTDF property, _BiztalkAppName_, must be set to the specific Application being deployed to
- A new property, _Integration_, must be added that contains a specific integration prefix.

![image](https://user-images.githubusercontent.com/17280237/143480295-6eac9b68-1d04-47f8-8bd1-58e86ee684a6.png)

All artifacts and ports must be prefixed with its specific integration ID.

Artifacts

![image](https://user-images.githubusercontent.com/17280237/143480872-2c2857b7-3128-4fa5-befe-234e7cae6f86.png)

Ports

![image](https://user-images.githubusercontent.com/17280237/143480951-34365c97-978d-404f-a866-2c1d60c65a8f.png)


**Standard Targets File**

To simplyfy usage a specific targets file has been created, modified slightly from the standard targets file. This needs to be updated in the Deployment.btdfproj.

![image](https://user-images.githubusercontent.com/17280237/143479553-87e4e494-9022-471a-8e5a-1f150d7ab677.png)

**Binding Files**

Each integration has its own bindingfile

![image](https://user-images.githubusercontent.com/17280237/143480069-9bded4b9-382b-42ef-a18a-e86350631da3.png)

