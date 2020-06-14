# VLM-demo
VLM-demo is an ameteur aerodynamics simulation, utilizing the vortex lattice method (https://en.wikipedia.org/wiki/Vortex_lattice_method).

It is currently capable of simulating isolated straight and untapered wings with no washout.

## Structure
It is written in C# using Blazor client-side (web-assembly) and split into two projects: 
the Blazor UI and the business logic in a class library.

## How it works
The Blazor UI captures all the important parameters and invokes a method of IOManager (in the class library), containing all gathered data.
IOManager in turn parses the data and publishes an event containing the parsed data, which is handled by a method in SimulationState.

The method in SimulationState calls different methods from the Utilities namespace, thus carrying out the simulation. Once complete, 
it publishes a new event with data gathered from the simulation run.

This event is subsequently handled by a method in IOManager, resulting in its output properties being updated. The Blazor render tree is 
then updaetd with the new properites.

## Publish notes

Copy VortexLatticeBlazorUI/bin/Release/netstandard2.1/publish/wwwroot to $web.
