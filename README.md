# Nanoject - MonoBehaviours

## What it is
Nanoject MonoBehaviours is an extension for the [Nanoject](https://github.com/derkork/nanoject-unity.git) dependency injection solution. It provides extension methods for declaring and injecting `MonoBehaviours` from the currently open scene in Nanoject's `DependencyContext`. Since this is not needed in every project it is not part of Nanoject itself. 

## Installation

In order to install this package to your Unity project, open `Packages\manifest.json` and add the following dependencies:

```json
"dependencies" : {
    "com.ancientlightstudios.nanoject": "https://github.com/derkork/nanoject-unity.git#2.0.0",
    "com.ancientlightstudios.nanoject-monobehaviours": "https://github.com/derkork/nanoject-unity-monobehaviours.git#1.0.0"
}
```

Unity currently does not support transitive dependency management for Git, so you will have to enter both URLs.
## Basic Usage

Use the `DeclareMonoBehavioursFromScene` extension method on `DependencyContext` to scan the current scene for suitable `MonoBehaviours` and declare them:

```csharp
// new context
var context = new DependencyContext();

// declare the objects that you want to fetch from the scene
context.DeclareMonoBehaviourFromScene<MyMonoBehaviour>();
context.DeclareMonoBehaviourFromScene<MyOtherMonoBehaviour>();

// declare the objects that need references from the scene
context.Declare<MyService>();

context.Resolve();

```

## How can I ...

### Fetch a specific object from the scene if I have multiple of them?

In case you use Unity's GUI system you might want to fetch references to buttons, etc. You can do this by using a qualifier. The qualifier must be the object's name in the scene hierarchy. For example if you have two buttons `nextButton` and `previousButton` in your scene hierarchy and want to inject them into a service that should handle the clicks, you can do it like this:

```csharp
// declare all buttons from the scene using their name as qualifier.
context.DeclareMonoBehavioursFromSceneQualified<Button>();


// then in your service just reference them:
public class UIService {
    public UIService(
        [Qualifier("nextButton")] Button nextButton,
        [Qualifier("previousButton")] Button previousButton) {
        // use the buttons somehow.
    }
}
``` 

### Inject dependencies into my `MonoBehaviour`s?

`MonoBehaviour`s are managed by Unity, so constructor injection does not work for them. In order to have dependencies injected into your `MonoBehaviours` you can add a method that is annotated with the `LateInit` attribute:

```csharp
public class MyBehaviour : MonoBehaviour {
    private MyService _myService;

    [LateInit]
    // the function must take all dependencies as arguments
    public void SetUp(MyService myService)
    {
        _myService = myService;
    } 
}
```

This method will be called when the context is resolved. You can also use qualifiers. For example if you want to reference the `nextButton` and `previousButton` from the previous example, you could do it like this:

```csharp
public class MyUIBehaviour : MonoBehaviour {
    private Button _nextButton;
    private Button _previousButton;

    [LateInit]
    public void SetUp(
            [Qualifier("nextButton")] Button nextButton,
            [Qualifier("previousButton")] Button previousButton)
    {
        _nextButton = nextButton;
        _previousButton = previousButton;
    } 
}
```

Note however, that usually it is easier to wire up `MonoBehaviour`s directly in the scene. The approach shown above is useful if you instantiate your behaviours dynamically from prefabs and cannot wire them up in the scene beforehand.
 

