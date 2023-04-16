[![openupm](https://img.shields.io/npm/v/net.mirzipan.framed?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/net.mirzipan.framed/) ![GitHub](https://img.shields.io/github/license/Mirzipan/Mirzipan.Framed)

# Mirzipan.Framed

A basic framework for an application or a game in Unity. Contains basic QoL improvements.
Currently there is only a preview version and it is not advisable to use it for anything outside testing.

Used libraries:
* [Reflex](https://github.com/gustavopsantos/Reflex) for dependency injection.
* [Mirzipan.Bibliotheca](https://github.com/Mirzipan/Mirzipan.Bibliotheca) for extra types, such as `CompositeDisposable`.
* [Mirzipan.Clues](https://github.com/Mirzipan/Mirzipan.Clues) for data-driven workflows.
* [Mirzipan.Extensions](https://github.com/Mirzipan/Mirzipan.Extensions) for convenience.
* [Mirzipan.Scheduler](https://github.com/Mirzipan/Mirzipan.Scheduler) for scheduler and fixed callbacks.

## Getting Started

The setup is very similar to *Reflex*.

### Core
1. Open a `Resources` folder.
2. Create a prefab named `ProjectContext`and attach the `ProjectContext` component to it. Or just use the context menu to do it for you.
3. Attach the `CoreInstaller` component to the aforementioned prefab.
4. Create `SchedulerConfiguration` asset and assign it to `CoreInstaller`.
5. Create `DefinitionsConfiguration` asset and assign it to `CoreInstaller`.

### Scene
1. Open your starting scene.
2. Create a new GameObject and attach `FramedScheduler` component to it.

## Contents

### Core Installer

This will take care of the basic configuration of Framed. 
Currently, it adds `Updater`, `Ticker`, and `Definitions`.

## Framed Scheduler

A singleton that makes sure that `Updater` and `Tick` receive ticks.

## Framed Behaviour

A base behaviour for Framed.
It is not necessary to use it, but it does come with a bunch of quality of life improvements over the Unity `MonoBehaviour`.
