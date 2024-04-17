using System;
using Source.Scripts.Data;
using Source.Scripts.Data.Models;
using UnityEngine;

namespace Source.Scripts.GameExtensions
{
//     public class GameExtensionService
//     {
//         public event Action<GameExtension> GameExtensionUnlocked;
//         public event Action<GameExtension> GameExtensionLocked;
//         
//         private readonly ProjectDatasContainer _projectDatasContainer;
//
//         public GameExtensionService(ProjectDatasContainer projectDatasContainer)
//         {
//             _projectDatasContainer = projectDatasContainer;
//         }
//
//         public void ResolveGameExtensions()
//         {
//             using GameExtensionResolver resolver = new GameExtensionResolver(this);
//         }
//         
//         public void UnlockGameExtension(GameExtension gameExtension)
//         {
//             if (gameExtension!= GameExtension.None)
//             {
//                 
// #if UNITY_EDITOR
//                 Debug.Log("gameExtension UnlockGameExtension " + gameExtension);
// #endif
//                 _projectDatasContainer.GameExtensionModel.UnlockGameExtension(gameExtension);
//                 
//                 GameExtensionUnlocked?.Invoke(gameExtension);
//             }
//         }
//         
//         public void LockGameExtension(GameExtension gameExtension)
//         {
//             _projectDatasContainer.GameExtensionModel.LockGameExtension(gameExtension);
//             
//             GameExtensionLocked?.Invoke(gameExtension);
//         }
//
//         public bool IsUnlockedGameExtension(GameExtension gameExtension) => _projectDatasContainer.GameExtensionModel.IsGameExtensionUnlocked(gameExtension);
//     }
}
