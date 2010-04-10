BBsharp
=======

BBsharp is a library for converting BBCode to HTML. It is written in C#.

It will parse BBCode into a DOM tree, where it will then be able to be exported into a range of formats. At the moment, we're focusing on HTML.

### FAQ
* **Hey, what's that funny character at the start of all the files?**
> That's the Byte Order Mark (BOM) which is an artifact of the Unicode encoding used to save BBsharp source files. It doesn't seem to be handled correctly by Git (and many other Linux tools, I hear.) It's not my fault, but I am looking into ways of resolving the issue. Please let me know if it causes any troubles compiling.

* **Where can I find a prebuilt assembly?**
> For stability's sake, I don't ever commit the cutting edge debug build. I do commit and push the release build though, you can find it in `bin/Release/`. Please note that the release build may not always be up to date with the currently committed code. A release is only committed once it is stable.

* **Where is the documentation?**
> You will find that there is no `doc` folder or anything similar full of documentation files. Rest assured that there is documentation included in the form Intellisense XML comments. BBsharp is not yet complex enough to require dedicated documentation, and I feel that good Intellisense documentation is more convenient than anything else!

* **How do I get started?**
> Getting started with BBsharp is easy! Just read through our [Getting Started guide](http://github.com/charliesome/BBsharp/blob/master/Getting Started.md)