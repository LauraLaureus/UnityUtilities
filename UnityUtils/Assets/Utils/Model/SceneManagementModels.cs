using System;

namespace LaureusUtils.SceneManagement
{   
    public class ActionOnSceneLoaded
    {
        public string ActionName { get; set; } //PK 
        public Action ActionToExecute { get; set; }
        public bool RemoveAfterExecution { get; set; }
    }

    public class SceneLoadAlreadyRegisteredActionException : Exception
    {
        static string format = "The list of actions to execute on scene load already contains a member called {0}" +
                "remove it before adding it again or make it unremovable";

        public SceneLoadAlreadyRegisteredActionException(ActionOnSceneLoaded a) : base(string.Format(format,a.ActionName))
        {
        }
    }
}


