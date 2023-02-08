/*
 * Copyright (c) 2021 Razeware LLC
 * Modified by Pauline Andrault
 */

using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RayWenderlich.WenderlichTopia
{
    public class ConstructionManager : MonoBehaviour
    {
        public GameObject constructionTilePrefab;
        public UiManager uiManager;
        public Transform levelGeometryContainer;

        // Notice : Chose one version, comment the other ones
        // (this script is used for educational purpose -> show implementation of CancellationToken and how to manage cancellation of a task)

        #region V0 : No Cancelling

        //private void Start()
        //{

        //}

        //public async void BuildStructure(GameObject placementStructure, Vector3 buildPosition)
        //{

        //    if (placementStructure.TryGetComponent(out RoadBuildPropertiesContainer roadBuildPropertiesContainer))
        //    {
        //        Destroy(placementStructure);
        //        var roadProperties = roadBuildPropertiesContainer.roadBuildProperties;
        //        var buildRoadTask = BuildRoadAsync(roadProperties, buildPosition);

        //        // EXEMPLE AVEC SYNC METHODE
        //        // BuildRoadNonAsync(roadProperties, buildPosition);
        //        await buildRoadTask;
        //        uiManager.NewStructureComplete(roadProperties.roadCost, buildPosition);
        //    }
        //    else if (placementStructure.TryGetComponent(out HouseBuildPropertiesContainer houseBuildPropertiesContainer))
        //    {
        //        Destroy(placementStructure);
        //        var houseBuildProperties = houseBuildPropertiesContainer.houseBuildProperties;
        //        var buildHouseTask = BuildHouseAsync(houseBuildProperties, buildPosition);
        //        await buildHouseTask;

        //        uiManager.NewStructureComplete(buildHouseTask.Result, buildPosition);
        //    }
        //}

        //private async Task BuildRoadAsync(RoadBuildProperties roadProperties, Vector3 buildPosition)
        //{
        //    var constructionTile = Instantiate(constructionTilePrefab, buildPosition, Quaternion.identity, levelGeometryContainer);
        //    await Task.Delay(2500);
        //    Destroy(constructionTile);
        //    Instantiate(roadProperties.completedRoadPrefab, buildPosition, Quaternion.identity, levelGeometryContainer);
        //}

        //// EXEMPLE AVEC SYNC METHODE
        //private void BuildRoadNonAsync(RoadBuildProperties roadProperties, Vector3 buildPosition)
        //{
        //    var constructionTile = Instantiate(constructionTilePrefab, buildPosition, Quaternion.identity, levelGeometryContainer);
        //    Task.Delay(2500).Wait();
        //    Destroy(constructionTile);
        //    Instantiate(roadProperties.completedRoadPrefab, buildPosition, Quaternion.identity, levelGeometryContainer);
        //}

        //private async Task<int> BuildHouseAsync(HouseBuildProperties houseBuildProperties, Vector3 buildPosition)
        //{
        //    var constructionTile = Instantiate(constructionTilePrefab, buildPosition, Quaternion.identity, levelGeometryContainer);
        //    Task<int> buildFrame = BuildHousePartAsync(houseBuildProperties, houseBuildProperties.completedFramePrefab, buildPosition);
        //    await buildFrame;

        //    Task<int> buildRoof = BuildHousePartAsync(houseBuildProperties, houseBuildProperties.completedRoofPrefab, buildPosition);
        //    Task<int> buildFence = BuildHousePartAsync(houseBuildProperties, houseBuildProperties.completedFencePrefab, buildPosition);
        //    // Await multiple tasks :
        //    await Task.WhenAll(buildRoof, buildFence);

        //    Task<int> finalizeHouse = BuildHousePartAsync(houseBuildProperties, houseBuildProperties.completedHousePrefab, buildPosition);
        //    await finalizeHouse;

        //    Destroy(constructionTile);
        //    var totalHouseCost = buildFrame.Result + buildRoof.Result + buildFence.Result + finalizeHouse.Result;
        //    return totalHouseCost;
        //}

        //private async Task<int> BuildHousePartAsync(HouseBuildProperties houseBuildProperties, GameObject housePartPrefab, Vector3 buildPosition)
        //{
        //    var constructionTime = houseBuildProperties.GetConstructionTime();
        //    await Task.Delay(constructionTime);
        //    Instantiate(housePartPrefab, buildPosition, Quaternion.identity, levelGeometryContainer);
        //    var taskCost = constructionTime * houseBuildProperties.wage;
        //    return taskCost;
        //}


        //private void Update()
        //{

        //}

        //private void OnDisable()
        //{
        //    // Montrer le cas où l'annulation n'est pas prévue : commencer à construire une House puis quitter le Play mode
        //}

        #endregion

        #region V1 : With Simple Cancelling

        //private CancellationTokenSource cancellationTokenSource;

        //private void Start()
        //{
        //    cancellationTokenSource = new CancellationTokenSource();
        //}

        //public async void BuildStructure(GameObject placementStructure, Vector3 buildPosition)
        //{
        //    var cancellationToken = cancellationTokenSource.Token;

        //    if (placementStructure.TryGetComponent(out RoadBuildPropertiesContainer roadBuildPropertiesContainer))
        //    {
        //        Destroy(placementStructure);
        //        var roadProperties = roadBuildPropertiesContainer.roadBuildProperties;
        //        var buildRoadTask = BuildRoadAsync(roadProperties, buildPosition, cancellationToken);
        //        await buildRoadTask;
        //        uiManager.NewStructureComplete(roadProperties.roadCost, buildPosition);
        //    }
        //    else if (placementStructure.TryGetComponent(out HouseBuildPropertiesContainer houseBuildPropertiesContainer))
        //    {
        //        Destroy(placementStructure);
        //        var houseBuildProperties = houseBuildPropertiesContainer.houseBuildProperties;

        //        var buildHouseTask = BuildHouseAsync(houseBuildProperties, buildPosition, cancellationToken);

        //        try
        //        {
        //            await buildHouseTask;
        //            uiManager.NewStructureComplete(buildHouseTask.Result, buildPosition);
        //        }
        //        catch
        //        {
        //            Debug.LogWarning("Building House Cancelled");
        //        }
        //    }
        //}

        //private async Task BuildRoadAsync(RoadBuildProperties roadProperties, Vector3 buildPosition, CancellationToken cancellationToken)
        //{
        //    var constructionTile = Instantiate(constructionTilePrefab, buildPosition, Quaternion.identity, levelGeometryContainer);
        //    await Task.Delay(2500, cancellationToken);
        //    Destroy(constructionTile);
        //    Instantiate(roadProperties.completedRoadPrefab, buildPosition, Quaternion.identity, levelGeometryContainer);
        //}

        //private async Task<int> BuildHouseAsync(HouseBuildProperties houseBuildProperties, Vector3 buildPosition, CancellationToken cancellationToken)
        //{
        //    var constructionTile = Instantiate(constructionTilePrefab, buildPosition, Quaternion.identity, levelGeometryContainer);
        //    Task<int> buildFrame = BuildHousePartAsync(houseBuildProperties, houseBuildProperties.completedFramePrefab, buildPosition, cancellationToken);
        //    await buildFrame;

        //    Task<int> buildRoof = BuildHousePartAsync(houseBuildProperties, houseBuildProperties.completedRoofPrefab, buildPosition, cancellationToken);
        //    Task<int> buildFence = BuildHousePartAsync(houseBuildProperties, houseBuildProperties.completedFencePrefab, buildPosition, cancellationToken);
        //    // Await multiple tasks :
        //    await Task.WhenAll(buildRoof, buildFence);

        //    Task<int> finalizeHouse = BuildHousePartAsync(houseBuildProperties, houseBuildProperties.completedHousePrefab, buildPosition, cancellationToken);
        //    await finalizeHouse;

        //    Destroy(constructionTile);
        //    var totalHouseCost = buildFrame.Result + buildRoof.Result + buildFence.Result + finalizeHouse.Result;
        //    return totalHouseCost;
        //}

        //private async Task<int> BuildHousePartAsync(HouseBuildProperties houseBuildProperties, GameObject housePartPrefab, Vector3 buildPosition, CancellationToken cancellationToken)
        //{
        //    var constructionTime = houseBuildProperties.GetConstructionTime();
        //    await Task.Delay(constructionTime, cancellationToken);
        //    Instantiate(housePartPrefab, buildPosition, Quaternion.identity, levelGeometryContainer);
        //    var taskCost = constructionTime * houseBuildProperties.wage;
        //    return taskCost;
        //}

        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        cancellationTokenSource.Cancel();
        //        cancellationTokenSource.Dispose();
        //        cancellationTokenSource = new CancellationTokenSource();
        //    }
        //}

        //private void OnDisable()
        //{
        //    // Montrer le cas où l'annulation n'est pas prévue : commencer à construire une House puis quitter le Play mode
        //    cancellationTokenSource.Cancel();
        //}

        #endregion

        #region V2 - With Cancelling and Destruction

        private CancellationTokenSource cancellationTokenSource;
        private List<GameObject> underConstructionParts = new List<GameObject>();

        private void Start()
        {
            cancellationTokenSource = new CancellationTokenSource();
        }

        public async void BuildStructure(GameObject placementStructure, Vector3 buildPosition)
        {
            var cancellationToken = cancellationTokenSource.Token;


            if (placementStructure.TryGetComponent(out RoadBuildPropertiesContainer roadBuildPropertiesContainer))
            {
                Destroy(placementStructure);
                var roadProperties = roadBuildPropertiesContainer.roadBuildProperties;
                var buildRoadTask = BuildRoadAsync(roadProperties, buildPosition, cancellationToken);
                await buildRoadTask;
                uiManager.NewStructureComplete(roadProperties.roadCost, buildPosition);
            }
            else if (placementStructure.TryGetComponent(out HouseBuildPropertiesContainer houseBuildPropertiesContainer))
            {
                Destroy(placementStructure);
                var houseBuildProperties = houseBuildPropertiesContainer.houseBuildProperties;

                var buildHouseTask = BuildHouseAsync(houseBuildProperties, buildPosition, cancellationToken);

                try
                {
                    await buildHouseTask;
                    uiManager.NewStructureComplete(buildHouseTask.Result, buildPosition);
                }
                catch
                {
                    Debug.LogWarning("Building House Cancelled");
                }
            }
        }

        private async Task BuildRoadAsync(RoadBuildProperties roadProperties, Vector3 buildPosition, CancellationToken cancellationToken)
        {
            var constructionTile = Instantiate(constructionTilePrefab, buildPosition, Quaternion.identity, levelGeometryContainer);
            await Task.Delay(2500, cancellationToken);
            Destroy(constructionTile);
            Instantiate(roadProperties.completedRoadPrefab, buildPosition, Quaternion.identity, levelGeometryContainer);
        }

        private async Task<int> BuildHouseAsync(HouseBuildProperties houseBuildProperties, Vector3 buildPosition, CancellationToken cancellationToken)
        {
            var constructionTile = Instantiate(constructionTilePrefab, buildPosition, Quaternion.identity, levelGeometryContainer);
            List<GameObject> houseParts = new List<GameObject>();
            try
            {
                Task<(int, GameObject)> buildFrame = BuildHousePartAsync(houseBuildProperties, houseBuildProperties.completedFramePrefab, buildPosition, cancellationToken);
                await buildFrame;
                houseParts.Add(buildFrame.Result.Item2);

                Task<(int, GameObject)> buildRoof = BuildHousePartAsync(houseBuildProperties, houseBuildProperties.completedRoofPrefab, buildPosition, cancellationToken);
                Task<(int, GameObject)> buildFence = BuildHousePartAsync(houseBuildProperties, houseBuildProperties.completedFencePrefab, buildPosition, cancellationToken);
                // Await multiple tasks :
                await Task.WhenAll(buildRoof, buildFence);
                houseParts.Add(buildRoof.Result.Item2);
                houseParts.Add(buildFence.Result.Item2);

                Task<(int, GameObject)> finalizeHouse = BuildHousePartAsync(houseBuildProperties, houseBuildProperties.completedHousePrefab, buildPosition, cancellationToken);
                await finalizeHouse;
                houseParts.Add(finalizeHouse.Result.Item2);

                Destroy(constructionTile);
                foreach (GameObject part in houseParts)
                    underConstructionParts.RemoveAt(underConstructionParts.IndexOf(part));
                var totalHouseCost = buildFrame.Result.Item1 + buildRoof.Result.Item1 + buildFence.Result.Item1 + finalizeHouse.Result.Item1;
                return totalHouseCost;
            }
            catch
            {
                Debug.Log("Maison annulée");
                Destroy(constructionTile);
                foreach (GameObject part in underConstructionParts)
                    Destroy(part);
                underConstructionParts.Clear();
                return 0;
            }
        }

        private async Task<(int, GameObject)> BuildHousePartAsync(HouseBuildProperties houseBuildProperties, GameObject housePartPrefab, Vector3 buildPosition, CancellationToken cancellationToken)
        {
            var constructionTime = houseBuildProperties.GetConstructionTime();
            await Task.Delay(constructionTime, cancellationToken);
            GameObject housePart = Instantiate(housePartPrefab, buildPosition, Quaternion.identity, levelGeometryContainer);
            underConstructionParts.Add(housePart);
            var taskCost = constructionTime * houseBuildProperties.wage;
            return (taskCost, housePart);
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
                cancellationTokenSource = new CancellationTokenSource();
            }
        }

        private void OnDisable()
        {
            // Montrer le cas où l'annulation n'est pas prévue : commencer à construire une House puis quitter le Play mode
            cancellationTokenSource.Cancel();
        }

        #endregion

    }
}