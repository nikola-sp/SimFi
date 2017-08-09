using System;
using System.Diagnostics;
using System.Reflection;

internal static class Resources
{
	[Conditional("DEBUG")]
	public static void EnumResources()
	{
		string[] a = Assembly.GetEntryAssembly().GetManifestResourceNames();
		Debug.WriteLine( "\nResourceNames:" );
		foreach ( string s in a ) Debug.WriteLine( "\t" + s );
		Debug.WriteLine( String.Empty );
	}

	// Use: ( no namespace )
	//
	// new Icon( typeof( Form1 ), "Images.network_center.ico" );
}