Getting started
===============

BBsharp was designed from the ground up to provide a simple library for converting BBCode to HTML, while also providing powerful tools to parse and mutate a DOM representation of a BBCode document.

It's totally up to you whether you decide to use the more advanced features, or just stay with the simple 'BBCode In, HTML Out' subset of the API.

### Converting BBCode to HTML - the easy way

Simple load up the `bbsharp.Easy` namespace and away you go!

    using bbsharp.Easy;
    
    class Program
    {
    public static void Main()
    	{
    		string BBCode = "[b]Hello there![/b] I've been converted to HTML with [i]BBsharp[i].";
    		
    		Console.WriteLine("BBCode: {0}", BBCode);
    		Console.WriteLine("HTML: {0}", BBCode.BbToHtml());
    	}
    }
   
That's it. However, using the easy way means that you have no way of setting how BBsharp works. If you wish to add some custom tags, you'll need to use the more advanced way which is detailed below.

### Converting BBCode to HTML - more advanced

BBsharp was designed to be modular, and therefore only provides the HTML output features if you request them. Let's load up the BBsharp namespace and the HTML renderer namespace:
	
    using bbsharp;
    using bbsharp.Renderers.Html;

Now use the `BBCodeDocument.Load()` method to parse a string of BBCode into a BBCode DOM. BBsharp is designed around a base class: `BBCodeNode`. Both the `BBCodeDocument` and `BBCodeTextNode` (a non-tag node) classes inherit from it. `BBCodeNode` was inspired by the `System.Xml.XmlNode` class from the .NET Framework and implements most of the methods that `XmlNode` does.

    BBCodeDocument document = BBCodeDocument.Load("[b]Hello there![/b] I've been converted to HTML with [name]BBsharp[name].");
    
Notice how we've used a custom tag - `[name]` - here. The BBCode parser doesn't care about specifics such as which tags exist and which don't. In fact, the only information we can give the BBCode parser about tags is which tags are singular (self closing)

Because we've used a custom tag, we need to implement a callback method for the HTML renderer to use. It must fit the method signature `string (BBCodeNode, bool, object)`. The only two parameters we need to care about are the first two. The first parameter (of type `BBCodeNode`)