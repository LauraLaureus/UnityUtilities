using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LaureusUtils.SceneManagement
{

    public class SceneManagementController {

        #region private members
        static List<ActionOnSceneLoaded> actionsOnLoad = new List<ActionOnSceneLoaded>();
        static bool alreadyRegistered = false;
        #endregion

        /// <summary>
        /// Load synchronous scene by name
        /// </summary>
        /// <param name="name">Name of the scene to load (case sensitive)</param>
        /// <param name="mode">Mode of loading, Single for remove previous scene.Additive for loading over the current scene </param>
        /// <param name="action">Action to do when the scene has finished to load</param>
        public static void LoadSceneByName(string name, LoadSceneMode mode = LoadSceneMode.Single, Action action = null)
        {
            LoadSceneByName(name, mode, action == null ? null : new ActionOnSceneLoaded() { ActionName = (actionsOnLoad.Count + 1).ToString(), ActionToExecute = action, RemoveAfterExecution = true });
        }

        /// <summary>
        /// Load synchronous scene by name
        /// </summary>
        /// <param name="name">Name of the scene to load (case sensitive)</param>
        /// <param name="mode">Mode of loading, Single for remove previous scene.Additive for loading over the current scene </param>
        /// <param name="action">Action to do when the scene has finished to load</param>
        public static void LoadSceneByName(string name, LoadSceneMode mode = LoadSceneMode.Single, ActionOnSceneLoaded action = null)
        {
            SceneManager.LoadScene(name, mode);

            if (action != null)
            {
                tryRegisterForEvent();
                registerNewActionOnSceneLoad(action);
            }
        }

        
        /// <summary>
        /// Load asynchronous scene by name
        /// </summary>
        /// <param name="name">Name of the scene to load (case sensitive)</param>
        /// <param name="mode">Mode of loading, Single for remove previous scene.Additive for loading over the current scene </param>
        /// <param name="action">Action to do when the scene has finished to load</param>
        public static void LoadSceneAsyncByName(string name, LoadSceneMode mode = LoadSceneMode.Single, Action action = null)
        {
            LoadSceneAsyncByName(name, mode, action == null ? null : new ActionOnSceneLoaded() { ActionName = (actionsOnLoad.Count + 1).ToString(), ActionToExecute = action, RemoveAfterExecution = true });
        }

        public static void LoadSceneAsyncByName(string name, LoadSceneMode mode = LoadSceneMode.Single, ActionOnSceneLoaded action = null)
        {
            SceneManager.LoadSceneAsync(name, mode);

            if (action != null)
            {
                tryRegisterForEvent();
                registerNewActionOnSceneLoad(action);
            }
        }

        #region private functions

        private static void registerNewActionOnSceneLoad(ActionOnSceneLoaded action)
        {
            if (!actionsOnLoad.ContainsPK(action))
                actionsOnLoad.Add(action);
            else
                throw new SceneLoadAlreadyRegisteredActionException(action);
        }

         ///Maybe deprecated
        /// <summary>
        /// Register a new action as ActionOnSceneLoaded with policy of removal after first execution
        /// </summary>
        /// <param name="act">Action to execute after scene load</param>
        private static void registerNewActionOnSceneLoad(Action act)
        {
            registerNewActionOnSceneLoad(new ActionOnSceneLoaded() { ActionName = (actionsOnLoad.Count + 1).ToString(), ActionToExecute = act, RemoveAfterExecution = true });
        }

        private static void tryRegisterForEvent()
        {
            if (!alreadyRegistered)
            {
                SceneManager.sceneLoaded += SceneManager_sceneLoaded;
                alreadyRegistered = true;
            }
        }

        private static void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            List<ActionOnSceneLoaded> toRemove = new List<ActionOnSceneLoaded>();
            if (alreadyRegistered)
            {
                foreach (ActionOnSceneLoaded act in actionsOnLoad)
                {
                    act.ActionToExecute.Invoke();
                    if (act.RemoveAfterExecution)
                    {
                        toRemove.Add(act);
                    }
                }
            }

            foreach (ActionOnSceneLoaded removable in toRemove)
            {
                actionsOnLoad.Remove(removable);
            }
        }
        #endregion

    }
}

