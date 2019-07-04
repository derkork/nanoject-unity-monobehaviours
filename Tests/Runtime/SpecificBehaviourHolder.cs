namespace AncientLightStudios.Nanoject.Tests
{
    using AncientLightStudios.Nanoject;
    
    public class SpecificBehaviourHolder
    {
        public SimpleBehaviour SimpleBehaviour { get; private set; }
        
        public SpecificBehaviourHolder([Qualifier("specific")] SimpleBehaviour simpleBehaviour)
        {
            SimpleBehaviour = simpleBehaviour;
        }
    }
}
