# Unity Mesh Destruction

<img align="right" src="https://i.imgur.com/IyTemGk.png" width="30%">

This project is an attempt at realtime mesh destruction in Unity, through an implementation of recursive cutting.

While the final solution is inefficient on large models, it is functional and could be utilised for smaller models in a live project.

The report within the repo details the creation of the system, background research conducted, and possible future steps for improving performance.

## Creating a material

This system bases its destruction calculations on the material properties of an object, which can be configured using MaterialTool in the root folder.

Once a material has been created and configured, adding the script "ObjectMaterial" to your mesh GameObject in Unity will allow you to select it.

Your mesh GameObject should also have a Rigidbody component, and convex Mesh Collider component.
