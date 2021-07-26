## UnitySnippets
Collection of useful Unity snippets for various usecases

### List of snippets:

#### General managers and controllers

1. `TimeCTRL_Simple.cs` - Allows for pausing/unpausing physics timescale from various scripts simultaneously

#### Misc

1. `TryUnblock.cs`      - Unblocks/unstucks 2D collider object if it is spawned/moved inside another 2D collider




### How to contribute:

Create a .cs script and add following comment before class name:

```cs
// --- AUTHOR ---
// [author], [year]
//
// --- DESCRIPTION ---
// [description]
//
// --- PUBLIC PARAMETERS TO SET IN EDITOR ---
//      [parameter]         - [parameter description]
//
//  [Any additional info]
```

Additional .cs file with sample usecase can be added with the same name, except with a `_u.cs` prefix.

After that create a Pull Request with .cs file(s) added to the repo (and information about exisiting snippet category or a new one to add).
