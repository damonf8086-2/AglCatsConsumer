# AGL Programming Challenge

This solution contains a command line application that calls a json web service setup at http://agl-developer-test.azurewebsites.net/people.json 

The application consumes the json and outputs a list of all the cats in alphabetical order under the header of the gender of their owner.

The application is written in C# using .NET Core 2.0.

You can open the solution using Visual Studio 2017 or Visual Studio Code.

### Projects
CatsConsumer - the command line application.  
UnitTest - contains the unit tests for CatsConsumer.

## Setting up and Running

### Using Visual Studio 2017

- git clone https://github.com/damonf8086-2/AglCatsConsumer.git
- Open solution AglCatsConsumer.sln with Visual Studio 2017
- Set the VS startup project to the "CatsConsumer" project 
- Hit F5! 

### From the CLI with Docker

- git clone https://github.com/damonf8086-2/AglCatsConsumer.git
- cd AglCatsConsumer
- docker-compose build
- docker container run --rm damonf8086/aglcatsconsumer

NOTE: You will need to switch to linux containers if using docker on windows.

## Travis CI and Docker Hub

This repository is configured for a Travis CI build which pushes a docker image to docker hub (https://hub.docker.com/r/damonf8086/aglcatsconsumer/).
This image can be run locally with the following command;
```bash
docker container run --rm damonf8086/aglcatsconsumer
```

You will need to have docker installed, and ensure you have switched to Linux containers if running on windows.

[![Build Status](https://travis-ci.org/damonf8086-2/AglCatsConsumer.svg?branch=master)](https://travis-ci.org/damonf8086-2/AglCatsConsumer)

