namespace AncientLightStudios.Nanoject.Tests
{
    public class BehaviourHolder
    {
        public SimpleBehaviour SimpleBehaviour { get; private set; }
        
        public BehaviourHolder(SimpleBehaviour simpleBehaviour)
        {
            SimpleBehaviour = simpleBehaviour;
        }
    }
}
