Here are some of the "mini games" I made for educational purpose. By "educational", I mean 2 things : 
- Learning : games made at school or games I made to apply a self-taught concept 
- Teaching : I led a group of alumni students willing to continue learning best practices and design patterns after our formation

------------------------------------------------------------------------------
1 - CityBuilderWithAsync :
	Purpose : Learning **and teaching** async operations and tasks.
	Material : The starter project comes from https://www.kodeco.com/26799311-introduction-to-asynchronous-programming-in-unity. Copyright (c) 2021 Razeware LLC.
	What I did : I wrote the ConstructionManager script with 3 versions, for teaching purpose. Note that you should have only 1 uncommented version to run the script in Unity.
		Version 0 : show what an async operation is, show the difference with an non asyn operation, show errors occurring when exiting Play mode without cancelling C# tasks.
		Version 1 : show basics of Cancelling.
		Version 2 : show try / catch, finale version of the script that handles correctly construction cancellation and destroys on going constructions.
------------------------------------------------------------------------------