Added dependency on A* Pathfinding to the Agents Assembly Definitions in order to access the Seeker class.

If that's undesirable, one might move the codebase for A* (as it references to itself) to the Agents definitions. I wasn't entirely sure whether to do so, given how the inclusion of A* was optional.