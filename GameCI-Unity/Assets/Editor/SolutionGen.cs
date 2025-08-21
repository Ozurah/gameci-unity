// https://discussions.unity.com/t/how-can-i-generate-csproj-files-during-continuous-integration-builds/842493/2

#if UNITY_EDITOR
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor;

namespace GitTools
{
    public static class Solution
    {
        public static void Sync()
        {
            ProjectGeneration projectGeneration = new ProjectGeneration();
            AssetDatabase.Refresh();
            projectGeneration.GenerateAndWriteSolutionAndProjects();
        }
    }
}
#endif