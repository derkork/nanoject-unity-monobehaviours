# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.0] 2019-12-17
### Changed
* [Breaking Change] It is now possible to have multiple `MonoBehaviours` with the same qualifier declared. This is useful if you want to inject a list of `MonoBehaviours` of a certain type into another component. The method `DeclareMonoBehaviourFromScene` has been renamed to `DeclareMonoBehavioursFromScene` (singular to plural) to indicate that all `MonoBehaviours` of the given type will now be added to the context.
* [Breaking Change] Now requires Nanoject 3.0.0.

## [1.0.0] 2019-07-04
* Initial release. 
