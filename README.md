[![openupm](https://img.shields.io/npm/v/com.zchfvy.gizmosplus?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.zchfvy.gizmosplus/)


# GizmosPlus
A toolkit for easier Gizmo usage in Unity.
Contains various commonly used useful shapes and also functionality for
drawing Gizmos outside of normal OnDrawGizmos flow.

![some gizmos from this package](Images/showcase.png)


## Installation

### Via OpenUPM

This package is available on the [OpenUPM registry](https://openupm.com). You
can install it form there using
[openupm-cli](https://github.com/openupm/openupm-cli):

```
openupm add com.zchfvy.gizmosplus
```

### Via git

Alternatively, this can be isntalled directly from github.Simply open your
`manifest.json` file located in the `Packages` folder of your unity project and
add the follwing line to the list of `dependencies`:
```json
"com.zchfvy.gizmosplus": "https://github.com/zchfvy/GizmosPlus.git"
```

## Usage

At the top of your file use the Plus namespace
```c#
using Zchfvy.Plus
```

Then simply call methods out of the `GizmosPlus` class
```c#
GizmosPlus.Cross(transform.position, 1.0f)
```

For creating Gizmos outside normal flow use the `GizmosPlusAsync` class
```c#
GizmosPlusAsync.DrawAsync(() => {
    // Draw Gizmos in here
});
```

For a complete listing of available methods and their parameters please consult
the [Documentation](https://zchfvy.github.io/GizmosPlus/Documentation/html/annotated.html)
