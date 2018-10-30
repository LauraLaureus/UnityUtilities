using System.Collections.Generic;

namespace LaureusUtils.SceneManagement
{
    public static class SceneManagementExtensioins
    {      
        public static bool ContainsPK(this List<ActionOnSceneLoaded> currentDS, ActionOnSceneLoaded action)
        {
            bool result = false;

            foreach (ActionOnSceneLoaded currentAction in currentDS)
            {
                if (currentAction.ActionName.Equals(action.ActionName))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }
    }
}
