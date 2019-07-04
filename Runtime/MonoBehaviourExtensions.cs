namespace AncientLightStudios.Nanoject.MonoBehaviours
{
    using System;
    using UnityEngine;
    using UObject = UnityEngine.Object;

    public static class MonoBehaviourExtensions
    {

        /// <summary>
        /// Scans the current scene for a MonoBehaviour that derives from the given type and declares it in the
        /// dependency context. The MonoBehaviour will not be qualified. If the scan yields
        /// multiple objects of the given type, an <see cref="InvalidOperationException"/> will be thrown as you
        /// can only have one unqualified object of the same type in a dependency context.
        /// </summary>
        /// <param name="context">the context into which the instance should be declared.</param>
        /// </param>
        public static void DeclareMonoBehaviourFromScene<T>(this DependencyContext context) where T : MonoBehaviour
        {
            DeclareMonoBehavioursFromScene<T>(context, false);
        }

        /// <summary>
        /// Scans the current scene for MonoBehaviours that derive from the given type and declares them in the
        /// dependency context. The MonoBehaviours will be qualified using their name in the scene hierarchy. If the
        /// scan yields multiple objects of the given type with the same name, an <see cref="InvalidOperationException"/>
        /// will be thrown as you can only have one qualified object of the same type and name in a dependency context.
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
        /// If false, they will be declared without a qualifier. Note that when this is false and the scan yields
        /// multiple objects of the given type, an <see cref="InvalidOperationException"/> will be thrown as you
        /// can only have one unqualified object of the same type in a dependency context.
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

                if (context.IsDeclared(type, qualifier))
                {
                    if (qualifier == "")
                    {
                        throw new InvalidOperationException( $"Found multiple objects of type {type.Name} with no qualifier.");
                    }
                    else
                    {
                        throw new InvalidOperationException( $"Found multiple objects of type {type.Name} with the same qualifier '{qualifier}'.");
                    }
                }
                    
                context.DeclareQualified(type, qualifier, result);
            }
        }
        
    }
}
