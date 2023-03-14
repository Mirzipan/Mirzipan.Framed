# Changelog

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