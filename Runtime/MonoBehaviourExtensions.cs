namespace AncientLightStudios.Nanoject.MonoBehaviours
{
    using System;
    using UnityEngine;
    using UObject = UnityEngine.Object;

    public static class MonoBehaviourExtensions
    {

        /// <summary>
        /// Scans the current scene for MonoBehaviours that derive from the given type and declares them in the
        /// dependency context. The MonoBehaviours will not be qualified.
        /// </summary>
        /// <param name="context">the context into which the instances should be declared.</param>
        /// </param>
        public static void DeclareMonoBehavioursFromScene<T>(this DependencyContext context) where T : MonoBehaviour
        {
            DeclareMonoBehavioursFromScene<T>(context, false);
        }

        /// <summary>
        /// Scans the current scene for MonoBehaviours that derive from the given type and declares them in the
        /// dependency context. The MonoBehaviours will be qualified using their name in the scene hierarchy.
        /// </summary>
        /// <param name="context">the context into which the classes should be declared.</param>
        /// </param>
        public static void DeclareMonoBehavioursFromSceneQualified<T>(this DependencyContext context) where T : MonoBehaviour
        {
            DeclareMonoBehavioursFromScene<T>(context, true);
        }

        /// <summary>
        /// Scans the current scene for MonoBehaviours that derive from the given type and declares them in the
        /// dependency context.
        /// </summary>
        /// <param name="context">the context into which the classes should be declared.</param>
        /// <param name="qualify">if true, the scanned component will be declared with their name as qualifier.
        /// If false, they will be declared without a qualifier. 
        /// </param>
        private static void DeclareMonoBehavioursFromScene<T>(DependencyContext context, bool qualify) where T:MonoBehaviour
        {
            var type = typeof(T);
            var results = UObject.FindObjectsOfType<T>();

            foreach (var result in results) 
            {
                var qualifier = "";
                if (qualify)
                {
                    qualifier = result.name;
                }
                    
                context.DeclareQualified(type, qualifier, result);
            }
        }
        
    }
}
