CityBuilderWithAsync
------------------------------------------------------------------------------
Made from the starter project here : https://www.kodeco.com/26799311-introduction-to-asynchronous-programming-in-unity
Copyright (c) 2021 Razeware LLC.	
------------------------------------------------------------------------------
Purpose : Learning **and teaching** async operations and tasks.
What I did : I wrote the ConstructionManager script with 3 versions, for teaching purpose. 
	Version 0 : show what an async operation is, show the difference with an non asyn operation, show errors occurring when exiting Play mode without cancelling C# tasks.
	Version 1 : show basics of Cancelling.
	Version 2 : show try / catch, finale version of the script that handles correctly construction cancellation and destroys on going constructions.
Note that you should have only 1 uncommented version to run the script in Unity.