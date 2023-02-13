Here are some "mini games" I made for educational purpose. By "educational", I mean 2 things : 
- Learning : games made at school or games I made to apply a self-taught concept 
- Teaching : I led a group of alumni students willing to continue learning best practices and design patterns after our formation

------------------------------------------------------------------------------
1 - FPSAudioExercise (school project)
Purpose : learning about Unity audio tools, interface implementation and 3D character controller. I am not sure about why it is called FPS though, because it is more of a FPWalkingSim.
Material : "Game" concept and audio samples were given by the school. 3D models and textures are free assets from the Unity Asset Store.
What I did : Everything except concept and art. 
- 3D controller and basic interaction with some interactable objects (door, NPC)
- Audio effects : cave reverb effect, lowpass effect on music if the Player opens or closes the nightclub door, spatial blending, dynamic footsteps sound...
- Nightclub's lights' behaviour based on music rythm (custom script using an AudioSource data)
------------------------------------------------------------------------------
2 - CityBuilderWithAsync :
Purpose : Learning **and teaching** async operations and tasks.
Material : The starter project comes from https://www.kodeco.com/26799311-introduction-to-asynchronous-programming-in-unity. Copyright (c) 2021 Razeware LLC.
What I did : I wrote the ConstructionManager script with 3 versions, for teaching purpose. Note that you should have only 1 uncommented version to run the script in Unity.
- Version 0 : show what an async operation is, show the difference with an non asyn operation, show errors occurring when exiting Play mode without cancelling C# tasks.
- Version 1 : show basics of Cancelling.
- Version 2 : show try / catch, finale version of the script that handles correctly construction cancellation and destroys on going constructions.
------------------------------------------------------------------------------
