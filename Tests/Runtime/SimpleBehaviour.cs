namespace AncientLightStudios.Nanoject.Tests
{
    using AncientLightStudios.Nanoject;
    using UnityEngine;
    
    public class SimpleBehaviour : MonoBehaviour
    {
        public SimpleService SimpleService { get; private set; }
        
        [LateInit]
        public void SetUp(SimpleService simpleService)
        {
            SimpleService = simpleService;
        } 
    }
}
