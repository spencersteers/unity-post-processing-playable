# Unity Post-Processing Playable

Control Post-Process Effects with Timeline.

## Requirements

- Unity [Post-processing (v2)](https://github.com/Unity-Technologies/PostProcessing)

This playable works by animating the `weight` of PostProcessVolume so that it overrides an existing Volume already present in the scene. This playable is also limited to working with global volumes setup such that there is a single global Volume in the scene that has a profile containing the _'base'_ post-process effect setup. The playable needs a profile that overrides specific paramaters base parameters. 

The included example scene showcases the intended usecase.
