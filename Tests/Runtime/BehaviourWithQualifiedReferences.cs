namespace  AncientLightStudios.Nanoject.Tests
{
    using UnityEngine;
    using AncientLightStudios.Nanoject;
    
    public class BehaviourWithQualifiedReferences : MonoBehaviour
    {
        public SimpleBehaviour First { get; private set; }
        public SimpleBehaviour Second { get; private set; }

        [LateInit]
        public void SetUp([Qualifier("first")] SimpleBehaviour first, [Qualifier("second")] SimpleBehaviour second)
        {
            First = first;
            Second = second;
        }
    }
}
