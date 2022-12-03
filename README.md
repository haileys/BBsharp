bbsharp
=======

bbsharp is a library for converting BBCode to HTML. It is written in C#.

It will parse BBCode into a DOM tree, where it will then be able to be exported into a range of formats. At the moment, we're focusing on HTML.

### Helping out

Want to lend a helping hand with bbsharp? Awesome! Just fork it with the button above and get familiar with the code.

At the moment, it would be nice to have some extra renderers. If you'd like to improve the core code, that's fine too.

### FAQ
* **Hey, what's that funny character at the start of all the files?**
> That's the Byte Order Mark (BOM) which is an artifact of the Unicode encoding used to save bbsharp source files. It doesn't seem to be handled correctly by Git (and many other Linux tools, I hear.) It's not my fault, but I am looking into ways of resolving the issue. Please let me know if it causes any troubles compiling.

* **Where can I find a prebuilt assembly?**
> For stability's sake, I don't ever commit the cutting edge debug build. I do commit and push the release build though, you can find it in `bin/Release/`. Please note that the release build may not always be up to date with the currently committed code. A release is only committed once it is stable.

* **Where is the documentation?**
> You will find that there is no `doc` folder or anything similar full of documentation files. Rest assured that there is documentation included in the form Intellisense XML comments. bbsharp is not yet complex enough to require dedicated documentation, and I feel that good Intellisense documentation is more convenient than anything else!

* **How do I get started?**
> Getting started with bbsharp is easy! Just read through our [Getting Started guide](http://github.com/charliesome/BBsharp/blob/master/Getting%20Started.md)
