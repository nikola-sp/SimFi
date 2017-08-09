using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;
using System.Diagnostics;

namespace DiffLinear
{
	static class Diff
	{
		//-----------------------------------------------------------------------------------------
		// public

		public static Results Compare( string[] aa, string[] ab )
		{
			Debug.WriteLine( String.Format( "\n\n*** STRING A:{0:N0} B:{1:N0} A+B:{2:N0} ***", aa.Length, ab.Length, aa.Length + ab.Length ) );

			var snakes = new List<Snake>();

			Compare( 0, snakes, aa, 0, aa.Length, ab, 0, ab.Length );

			return new Results( MergeSnakes( snakes, 0, aa.Length, 0, ab.Length ) );
		}

		//-----------------------------------------------------------------------------------------
		// Compare string

		static void Compare( int recursion, List<Snake> snakes,
			string[] pa, int a0, int N, string[] pb, int b0, int M )
		{
			var VForwardArray = new V( N, M, true );
			var VReverseArray = new V( N, M, false );

			Compare( recursion, snakes, pa, a0, N, pb, b0, M, VForwardArray, VReverseArray );
		}

		static void Compare( int recursion, List<Snake> snakes,
			string[] pa, int a0, int N, string[] pb, int b0, int M,
			V VForward, V VReverse )
		{
			//Debug.WriteLine( new String( '-', recursion ) + recursion + "> Compare( " + a0 + ", " + b0 + " ) + ( " + N + ", " + M + " ) = ( " + ( a0 + N ) + ", " + ( b0 + M ) + " )" );
#if MEMORY
			long memStart = GC.GetTotalMemory( true );
#endif

			if ( N == 0 && M > 0 )
			{
				var down = new Snake( a0, N, b0, M, true, a0, b0, 0, M, 0 );
				//Debug.WriteLine( "down: " + down );
				snakes.Add( down );
			}

			if ( M == 0 && N > 0 )
			{
				var right = new Snake( a0, N, b0, M, true, a0, b0, N, 0, 0 );
				//Debug.WriteLine( "right: " + right );
				snakes.Add( right );
			}

			if ( N <= 0 || M <= 0 ) return;

			SnakePair? middle = null;

			VForward[ 1 ] = 0;
			VReverse[ N - M - 1 ] = N;

			middle = DiffCommon.CalcForD.MiddleSnake( pa, a0, N, pb, b0, M, VForward, VReverse );

			if ( middle == null ) throw new ApplicationException( "No middle snake" );

			var m = middle.Value;
			int d = middle.Value.D;

			//Debug.WriteLine( "d:" + d + " " + m.Forward + " " + m.Reverse );

			if ( d > 1 )
			{
				var xy = ( m.Forward != null ? m.Forward.StartPoint : m.Reverse.EndPoint );
				var uv = ( m.Reverse != null ? m.Reverse.StartPoint : m.Forward.EndPoint );

				Compare( recursion + 1, snakes, pa, a0, xy.X - a0, pb, b0, xy.Y - b0, VForward, VReverse );

				if ( m.Forward != null ) snakes.Add( m.Forward );
				if ( m.Reverse != null ) snakes.Add( m.Reverse );

				Compare( recursion + 1, snakes, pa, uv.X, a0 + N - uv.X, pb, uv.Y, b0 + M - uv.Y, VForward, VReverse );

			}
			else
			{
				if ( m.Forward != null && m.Reverse != null ) // check for overlapping diagonal
					if ( m.Forward.XMid - m.Forward.YMid == m.Reverse.XMid - m.Reverse.YMid )
					{
						m.Forward.DiagonalLength = m.Reverse.XMid - m.Forward.XMid;
						m.Reverse.DiagonalLength = 0;
					}

				if ( m.Forward != null )
				{
					// D0
					if ( m.Forward.XStart > a0 )
					{
						if ( m.Forward.XStart - a0 != m.Forward.YStart - b0 ) throw new ApplicationException( "Missed D0 forward" );
						snakes.Add( new Snake( a0, N, b0, M, true, a0, b0, 0, 0, m.Forward.XStart - a0 ) );
					}

					snakes.Add( m.Forward );
				}

				if ( m.Reverse != null )
				{
					snakes.Add( m.Reverse );

					// D0
					if ( m.Reverse.XStart < a0 + N )
					{
						if ( a0 + N - m.Reverse.XStart != b0 + M - m.Reverse.YStart ) throw new ApplicationException( "Missed D0 reverse" );
						snakes.Add( new Snake( a0, N, b0, M, true, m.Reverse.XStart, m.Reverse.YStart, 0, 0, a0 + N - m.Reverse.XStart ) );
					}
				}
			}
		}

		//-----------------------------------------------------------------------------------------
		// MergeSnakes

		static IList<Snake> MergeSnakes( List<Snake> snakes, int a0, int N, int b0, int M )
		{
			var r = new List<Snake>( snakes.Count );

			var c = new Snake( a0, N, b0, M, true, 0, 0, 0, 0, 0 );

			foreach ( var s in snakes )
			{
				if ( s.IsForward )
				{
					if ( ( s.ADeleted > 0 || s.BInserted > 0 ) && c.DiagonalLength > 0 )
					{
						r.Add( c );
						c = new Snake( a0, N, b0, M, true, c.XEnd, c.YEnd, 0, 0, 0 );
					}

					c.ADeleted += s.ADeleted;
					c.BInserted += s.BInserted;
					c.DiagonalLength += s.DiagonalLength;

					Debug.Assert( c.EndPoint == s.EndPoint );
				}
				else
				{
					c.DiagonalLength += s.DiagonalLength;

					if ( ( s.ADeleted > 0 || s.BInserted > 0 ) && c.DiagonalLength > 0 )
					{
						r.Add( c );
						c = new Snake( a0, N, b0, M, true, c.XEnd, c.YEnd, 0, 0, 0 );
					}

					c.ADeleted += s.ADeleted;
					c.BInserted += s.BInserted;

					Debug.Assert( c.EndPoint == s.StartPoint );
				}
			}

			if ( c.StartPoint != c.EndPoint ) r.Add( c );

			return r;
		}


		//-----------------------------------------------------------------------------------------

	}
}
