# Changelog

## [3.0.0-alpha.3] - 2023-06-24

### Change
- Reflex 4.3.2
- Heist 5.2.1

## [3.0.0-alpha.2] - 2023-05-29

### Change
- Split Heist-related bindings into HeistInstaller

### Fixed
- wrong name of parameter in IReactToAction

## [3.0.0-alpha.1] - 2023-05-29

### Added
- ReactiveSystems that may react to actions and commands

### Changed
- Updated Heist to 5.2.0
- Changed how CoreInstaller works

## [2.2.0] - 2023-05-01

### Added
- AddSingletonAsInterfaces and AddSingletonAsInterfacesAndSelf
- Validate and Process for IAction in FramedBehaviour

### Fixed
- array not being resized correctly in AddSingletonAsSelf

## [2.1.1] - 2023-04-30

### Fixed
- Nullref if no contracts are provided for AddSingletonAsSelf

## [2.1.0] - 2023-04-30

### Added
- Extension methods for object and Component injection
- Extension method for singleton binding 
- Added Heist 1.0.0

### Changed
- Updated Reflex to 4.1.1

## [2.0.1-alpha.2] - 2023-04-17

### Fixed
- removed reference to non-existent type in CoreInstaller 

## [2.0.1-alpha.1] - 2023-04-16

### Changed
- updated Mirzipan.Clues dependency to 2.1.0

## [2.0.0-alpha.1] - 2023-04-16

### Added
- DefinitionsConfiguration
- SchedulerConfiguration
- FramedBehaviour now has Ticker
- FramedScheduler for ticking Ticker and Updater

### Changed
- CoreConfiguration renamed to CoreInstaller
- Swapped Mirzipan.Infusion for Reflex for dependency injection

### Removed
- Module and its variants (Module, SceneModule, CoreModule) - basic modules now live in Bibliotheca
- Scene
- Core (and CoreState)
- Configuration and its variants (component, scriptable object, class, etc.)

## [1.0.1-alpha.4] - 2023-03-14

### Changed
- now using the 2.X series of Infusion

### Fixed
- critical issue with wrong configuration sorting

## [1.0.1-alpha.3] - 2023-03-10

### Fixed
- CoreConfiguration now properly registers its modules

## [1.0.1-alpha.2] - 2023-03-05

### Added
- OnGameReady method for FramedBehaviour

## [1.0.1-alpha] - 2023-03-05

### Fixed
- Version number again, it seems we cannot go back to pre-1.0.0

## [0.0.3] - 2023-03-05

### Fixed
- Unintentional behaviour injection after core loaded callback

## [0.0.2] - 2023-03-04

### Fixed
- Version number

## [0.0.1] - 2023-03-04
Very early preview version

### Added
- Core (the brains of the operation that has to always be loaded first)
- Configurations (MonoBehaviours, ScriptableObjects and regular C# classes) for injection
- FramedBehaviour (Base behaviour to be used for injection)
- Module generic type for use in anything modular
- Scene (WIP)