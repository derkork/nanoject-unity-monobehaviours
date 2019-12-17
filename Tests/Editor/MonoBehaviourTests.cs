namespace AncientLightStudios.Nanoject.Tests
{
    using System.Collections.Generic;
    using MonoBehaviours;
    using NUnit.Framework;
    using UnityEngine;

    [TestFixture]
    public class MonoBehaviourTests
    {
        private DependencyContext _context;
        private List<GameObject> _dispose;
            
        [SetUp]
        public void SetUp()
        {
            _context = new DependencyContext();
            _dispose = new List<GameObject>();
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var disposable in _dispose)
            {
                GameObject.DestroyImmediate(disposable);
            }   
        }

        private GameObject MakeGameObject()
        {
            var go = new GameObject();
            _dispose.Add(go);
            return go;
        }

        [Test]
        public void ObjectsFromSceneReceiveLateInits()
        {
            var go = MakeGameObject();
            var behaviour = go.AddComponent<SimpleBehaviour>();
            
            _context.Declare<SimpleService>();
            _context.DeclareMonoBehavioursFromScene<SimpleBehaviour>();
            _context.Resolve();

            Assert.AreSame(_context.Get<SimpleService>(), behaviour.SimpleService);
        }

        
        [Test]
        public void ObjectsCanReceiveObjectsFromScene()
        {
            
            var go = MakeGameObject();
            var behaviour = go.AddComponent<SimpleBehaviour>();
            
            _context.Declare<SimpleService>();
            _context.Declare<BehaviourHolder>();
            _context.DeclareMonoBehavioursFromScene<SimpleBehaviour>();
            _context.Resolve();

            Assert.AreSame(behaviour, _context.Get<BehaviourHolder>().SimpleBehaviour);
        }

        [Test]
        public void QualifiersWork()
        {
            var go = MakeGameObject();
            go.name = "specific";
            var behaviour = go.AddComponent<SimpleBehaviour>();

            go = MakeGameObject();
            go.name = "gummiente";
            go.AddComponent<SimpleBehaviour>();

            _context.Declare<SimpleService>();
            _context.Declare<SpecificBehaviourHolder>();
            _context.DeclareMonoBehavioursFromSceneQualified<SimpleBehaviour>();
            _context.Resolve();

            Assert.AreSame(behaviour, _context.Get<SpecificBehaviourHolder>().SimpleBehaviour);
        }

        [Test]
        public void QualifiedLateInitsWork()
        {
            var go = MakeGameObject();
            go.name = "first";
            var first = go.AddComponent<SimpleBehaviour>();

            go = MakeGameObject();
            go.name = "second";
            var second = go.AddComponent<SimpleBehaviour>();

            go = MakeGameObject();
            var component = go.AddComponent<BehaviourWithQualifiedReferences>();

            _context.Declare<SimpleService>();
            _context.DeclareMonoBehavioursFromSceneQualified<SimpleBehaviour>();
            _context.DeclareMonoBehavioursFromScene<BehaviourWithQualifiedReferences>();
            _context.Resolve();

            Assert.AreSame(first, component.First);
            Assert.AreSame(second, component.Second);
        }
    }
}
