# Unity-YulioCapture
Export Yulio-friendly stereoscopic cubemaps from Unity. 

### Yulio
Yulio lets you quickly upload, share, and present designs in virtual reality using high-fidelity stereoscopic cubemaps.

See [Yulio](http://www.yulio.com) for more information or to set up a free account. 

Unity is not officially supported by Yulio. This export script was created to assist in creating rapid prototype environments in VR to aid our user research and prototyping activities.

### Usage

1. Import the .unitypackage to your Unity Project (Download from the [Releases](https://github.com/PixelTours/Unity-YulioCapture/releases) page)
2. Select Tools > Export Yulio Cubemaps
3. Select which camera you would like to render from.

*Note: Yulio only accepts a cube face resolution of 1536. The ability to render at higher (or lower) resolution is still in present, in case you want to work in higher res for other purposes.*

4. Select PNG or JPEG for the file format. 
5. Click Export
6. Select a location and file name
7. Done! Upload your cubemap to Yulio. 

### Warnings
- Some post-processing effects like Bloom will not render correctly due to how Unity captures cubemaps. 
- UI Canvases will not be captured.

### License
[MIT](https://github.com/PixelTours/Unity-YulioCapture/blob/master/LICENSE)

This internal utility is provided "as is" and carries no warranty. 


