[![openupm](https://img.shields.io/npm/v/net.mirzipan.framed?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/net.mirzipan.framed/) ![GitHub](https://img.shields.io/github/license/Mirzipan/Mirzipan.Framed)

# Mirzipan.Framed

A basic framework for an application or a game in Unity. Contains basic QoL improvements.
Currently there is only a preview version and it is not advisable to use it for anything outside testing.

## Core

Main brain of the operation. This is a singleton, therefore it is accessible from everywhere.

### Core Module

Modules that represent systems, services, or other mechanics that need to exist game-wide.

## Scene

A MonoBehaviour that represents the root of the scene.

### Scene Module

Modules that represent systems, services, or other mechanics that need to exist within a scene.

## Configuration Context

A group of configurations to be used for priming injection containers.
You should create your own child classes of any of the provided base implementations.
All enabled configurations are resolved in order from the **highest to lowest** priority.

### IConfiguration

```csharp
public interface IConfiguration
{
    bool IsEnabled { get; }
    int Priority { get; }
    void AddBindings();
}
```

### Configuration

A simple C# class that implements `IConfiguration` interface.

### Configuration Component

A MonoBehaviour that implements `IConfiguration` interface.

### Configuration Scriptable Object

A ScriptableObjects that implements `IConfiguration` interface.
