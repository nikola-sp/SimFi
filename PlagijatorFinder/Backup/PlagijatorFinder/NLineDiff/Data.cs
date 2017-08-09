using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Data
{

	//-----------------------------------------------------------------------------------------
	// Results

	public class Results
	{
		public IList<Snake> Snakes { get; protected set; }

		public Results( IList<Snake> snakes )
		{
			Snakes = snakes;
		}
	}

	//-----------------------------------------------------------------------------------------
	// SnakePair

	public struct SnakePair
	{
		public int D;
		public Snake Forward;
		public Snake Reverse;
	}

	//-----------------------------------------------------------------------------------------
	// Snake

	public class Snake
	{
		public int XStart;
		public int YStart;
		public int ADeleted;
		public int BInserted;
		public int DiagonalLength;
		public bool IsForward = true;
		public int DELTA = 0;

		public Point StartPoint { get { return new Point( XStart, YStart ); } }

		public int XMid { get { return ( IsForward ? XStart + ADeleted : XStart - ADeleted ); } }
		public int YMid { get { return ( IsForward ? YStart + BInserted : YStart - BInserted ); } }
		public Point MidPoint { get { return new Point( XMid, YMid ); } }

		public int XEnd { get { return ( IsForward ? XStart + ADeleted + DiagonalLength : XStart - ADeleted - DiagonalLength ); } }
		public int YEnd { get { return ( IsForward ? YStart + BInserted + DiagonalLength : YStart - BInserted - DiagonalLength ); } }
		public Point EndPoint { get { return new Point( XEnd, YEnd ); } }

		public override string ToString()
		{
			return "Snake " + ( IsForward ? "F" : "R" ) + ": ( " + XStart + ", " + YStart + " ) + ( " +
				( ADeleted > 0 ? "D" + ADeleted : "" ) +
				"," +
				( BInserted > 0 ? " I" + BInserted : "" ) +
				" ) + " + DiagonalLength + " -> ( " + XEnd + ", " + YEnd + " )" +
				" k=" + ( XMid - YMid );
		}

		Snake( bool isForward, int delta )
		{
			Debug.Assert( !( isForward && delta != 0 ) );

			IsForward = isForward;

			if ( !IsForward ) DELTA = delta;
		}

		void RemoveStubs( int a0, int N, int b0, int M )
		{
			if ( IsForward )
			{
				if ( XStart == a0 && YStart == b0 - 1 && BInserted == 1 )
				{
					Debug.WriteLine( "Stub before:" + this );

					YStart = b0;
					BInserted = 0;

					Debug.WriteLine( "Stub after:" + this );
				}
			}
			else
			{
				if ( XStart == a0 + N && YStart == b0 + M + 1 && BInserted == 1 )
				{
					Debug.WriteLine( "Stub before: " + this );

					YStart = b0 + M;
					BInserted = 0;

					Debug.WriteLine( "Stub after:" + this );
				}
			}

			Debug.Assert( a0 <= XStart && XStart <= a0 + N );
			Debug.Assert( a0 <= XMid && XMid <= a0 + N );
			Debug.Assert( a0 <= XEnd && XEnd <= a0 + N );

			Debug.Assert( b0 <= YStart && YStart <= b0 + M );
			Debug.Assert( b0 <= YMid && YMid <= b0 + M );
			Debug.Assert( b0 <= YEnd && YEnd <= b0 + M );

			Debug.Assert( XMid - YMid == XEnd - YEnd ); // k
		}

		public Snake( int a0, int N, int b0, int M, bool isForward, int xStart, int yStart, int aDeleted, int bInserted, int diagonal )
		{
			XStart = xStart;
			YStart = yStart;
			ADeleted = aDeleted;
			BInserted = bInserted;
			DiagonalLength = diagonal;
			IsForward = isForward;

			RemoveStubs( a0, N, b0, M );
		}

		public Snake( int a0, int N, int b0, int M, bool isForward, int xStart, int yStart, bool down, int diagonal )
		{
			XStart = xStart;
			YStart = yStart;
			ADeleted = down ? 0 : 1;
			BInserted = down ? 1 : 0;
			DiagonalLength = diagonal;
			IsForward = isForward;

			RemoveStubs( a0, N, b0, M );
		}

		public Snake( int a0, int N, int b0, int M, bool forward, int delta, V V, int k, int d, string[] pa, string[] pb )
			: this( forward, delta )
		{
			Calculate( V, k, d, pa, a0, N, pb, b0, M );
		}

		public Snake Calculate( V V, int k, int d, string[] pa, int a0, int N, string[] pb, int b0, int M )
		{
			if ( IsForward ) return CalculateForward( V, k, d, pa, a0, N, pb, b0, M );

			return CalculateBackward( V, k, d, pa, a0, N, pb, b0, M );
		}

		Snake CalculateForward( V V, int k, int d, string[] pa, int a0, int N, string[] pb, int b0, int M )
		{
			bool down = ( k == -d || ( k != d && V[ k - 1 ] < V[ k + 1 ] ) );

			int xStart = down ? V[ k + 1 ] : V[ k - 1 ];
			int yStart = xStart - ( down ? k + 1 : k - 1 );

			int xEnd = down ? xStart : xStart + 1;
			int yEnd = xEnd - k;

			int snake = 0;
			while ( xEnd < N && yEnd < M && pa[ xEnd + a0 ] == pb[ yEnd + b0 ] ) { xEnd++; yEnd++; snake++; }

			XStart = xStart + a0;
			YStart = yStart + b0;
			ADeleted = down ? 0 : 1;
			BInserted = down ? 1 : 0;
			DiagonalLength = snake;

			RemoveStubs( a0, N, b0, M );

			return this;
		}

		Snake CalculateBackward( V V, int k, int d, string[] pa, int a0, int N, string[] pb, int b0, int M )
		{
			bool up = ( k == d + DELTA || ( k != -d + DELTA && V[ k - 1 ] < V[ k + 1 ] ) );

			int xStart = up ? V[ k - 1 ] : V[ k + 1 ];
			int yStart = xStart - ( up ? k - 1 : k + 1 );

			int xEnd = up ? xStart : xStart - 1;
			int yEnd = xEnd - k;

			int snake = 0;
			while ( xEnd > 0 && yEnd > 0 && pa[ xEnd + a0 - 1 ] == pb[ yEnd + b0 - 1 ] ) { xEnd--; yEnd--; snake++; }

			XStart = xStart + a0;
			YStart = yStart + b0;
			ADeleted = up ? 0 : 1;
			BInserted = up ? 1 : 0;
			DiagonalLength = snake;

			RemoveStubs( a0, N, b0, M );

			return this;
		}
	}

	//-----------------------------------------------------------------------------------------
	// V

	public class V
	{
		public bool IsForward { get; private set; }
		public int N { get; private set; }
		public int M { get; private set; }

		public int _Max;
		public int _Delta;
		public int[] _Array;

		public int this[ int k ]
		{
			get
			{
				return _Array[ k - _Delta + _Max ];
			}

			set
			{
				_Array[ k - _Delta + _Max ] = value;
			}
		}

		public int Y( int x, int k ) { return x - k; }

		public long Memory { get { return sizeof( int ) * ( 3 + _Array.Length ); } }

		V() { }

		public V( int n, int m, bool forward )
		{
			Debug.Assert( n >= 0 && m >= 0, "V.ctor N:" + n + " M:" + m );

			IsForward = forward;
			N = n;
			M = m;
			_Max = n + m;
			if ( _Max <= 0 ) _Max = 1;
			if ( !forward ) _Delta = n - m;
			_Array = new int[ 2 * _Max + 1 ];

			InitStub();
		}

		public void InitStub()
		{
			if ( IsForward ) this[ 1 ] = 0; // stub for forward
			else this[ N - M - 1 ] = N; // stub for backward
		}

		public override string ToString()
		{
			return "V [ " + ( _Delta - _Max ) + " .. " + _Delta + " .. " + ( _Delta + _Max ) + " ]";
		}
	}

	//-----------------------------------------------------------------------------------------
}
