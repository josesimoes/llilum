// ==++==
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--==
/*============================================================
**
** Class:  SByte
**
**
** Purpose:
**
**
===========================================================*/
namespace System
{
    using System;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Runtime.CompilerServices;

    // A place holder class for signed bytes.
    [Microsoft.Zelig.Internals.WellKnownType( "System_SByte" )]
    [Serializable]
    [CLSCompliant( false )]
    [StructLayout( LayoutKind.Sequential )]
    public struct SByte : IComparable, IFormattable, IConvertible, IComparable<SByte>, IEquatable<SByte>
    {
        public const sbyte MaxValue =            (sbyte)0x7F  ; // The maximum value that a Byte may represent: 127.
        public const sbyte MinValue = unchecked( (sbyte)0x80 ); // The minimum value that a Byte may represent: -128.

        internal sbyte m_value;


        // Compares this object to another object, returning an integer that
        // indicates the relationship.
        // Returns a value less than zero if this  object
        // null is considered to be less than any instance.
        // If object is not of type SByte, this method throws an ArgumentException.
        //
        public int CompareTo( Object obj )
        {
            if(obj == null)
            {
                return 1;
            }

            if(!(obj is SByte))
            {
#if EXCEPTION_STRINGS
                throw new ArgumentException( Environment.GetResourceString( "Arg_MustBeSByte" ) );
#else
                throw new ArgumentException();
#endif
            }

            return CompareTo( (SByte)obj );
        }

        public int CompareTo( SByte value )
        {
            return m_value - value;
        }

        // Determines whether two Byte objects are equal.
        public override bool Equals( Object obj )
        {
            if(!(obj is SByte))
            {
                return false;
            }

            return Equals( (SByte)obj );
        }

        public bool Equals( SByte obj )
        {
            return m_value == obj;
        }

        // Gets a hash code for this instance.
        public override int GetHashCode()
        {
            return ((int)m_value ^ (int)m_value << 8);
        }


        // Provides a string representation of a byte.
        public override String ToString()
        {
            return Number.FormatInt32( m_value, /*null,*/ NumberFormatInfo.CurrentInfo );
        }

        public String ToString( IFormatProvider provider )
        {
            return Number.FormatInt32( m_value, /*null,*/ NumberFormatInfo.GetInstance( provider ) );
        }
    
        public String ToString( String format )
        {
            return ToString( format, NumberFormatInfo.CurrentInfo );
        }
    
        public String ToString( String format, IFormatProvider provider )
        {
            return ToString( format, NumberFormatInfo.GetInstance( provider ) );
        }
    
        private String ToString( String format, NumberFormatInfo info )
        {
            if(m_value < 0 && format != null && format.Length > 0 && (format[0] == 'X' || format[0] == 'x'))
            {
                uint temp = (uint)(m_value & 0x000000FF);
    
                return Number.FormatUInt32( temp, format, info );
            }
    
            return Number.FormatInt32( m_value, format, info );
        }
    
        [CLSCompliant( false )]
        public static sbyte Parse( String s )
        {
            return Parse( s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo );
        }
    
        [CLSCompliant( false )]
        public static sbyte Parse( String       s     ,
                                   NumberStyles style )
        {
            NumberFormatInfo.ValidateParseStyleInteger( style );
    
            return Parse( s, style, NumberFormatInfo.CurrentInfo );
        }
    
        [CLSCompliant( false )]
        public static sbyte Parse( String          s        ,
                                   IFormatProvider provider )
        {
            return Parse( s, NumberStyles.Integer, NumberFormatInfo.GetInstance( provider ) );
        }
    
        // Parses a signed byte from a String in the given style.  If
        // a NumberFormatInfo isn't specified, the current culture's
        // NumberFormatInfo is assumed.
        //
        [CLSCompliant( false )]
        public static sbyte Parse( String          s        ,
                                   NumberStyles    style    ,
                                   IFormatProvider provider )
        {
            NumberFormatInfo.ValidateParseStyleInteger( style );
    
            return Parse( s, style, NumberFormatInfo.GetInstance( provider ) );
        }
    
        private static sbyte Parse( String           s     ,
                                    NumberStyles     style ,
                                    NumberFormatInfo info  )
        {
            int i = 0;
    
            try
            {
                i = Number.ParseInt32( s, style, info );
            }
            catch(OverflowException e)
            {
#if EXCEPTION_STRINGS
                throw new OverflowException( Environment.GetResourceString( "Overflow_SByte" ), e );
#else
                throw new OverflowException( null, e );
#endif
            }
    
            if((style & NumberStyles.AllowHexSpecifier) != 0)
            { // We are parsing a hexadecimal number
                if((i < 0) || i > Byte.MaxValue)
                {
#if EXCEPTION_STRINGS
                    throw new OverflowException( Environment.GetResourceString( "Overflow_SByte" ) );
#else
                    throw new OverflowException();
#endif
                }
                return (sbyte)i;
            }
    
            if(i < MinValue || i > MaxValue)
            {
#if EXCEPTION_STRINGS
                throw new OverflowException( Environment.GetResourceString( "Overflow_SByte" ) );
#else
                throw new OverflowException();
#endif
            }
    
            return (sbyte)i;
        }
    
        [CLSCompliant( false )]
        public static bool TryParse(     String s      ,
                                     out SByte  result )
        {
            return TryParse( s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result );
        }
    
        [CLSCompliant( false )]
        public static bool TryParse(     String          s        ,
                                         NumberStyles    style    ,
                                         IFormatProvider provider ,
                                     out SByte           result   )
        {
            NumberFormatInfo.ValidateParseStyleInteger( style );
    
            return TryParse( s, style, NumberFormatInfo.GetInstance( provider ), out result );
        }
    
        private static bool TryParse(     String           s      ,
                                          NumberStyles     style  ,
                                          NumberFormatInfo info   ,
                                      out SByte            result )
        {
            result = 0;
    
            int i;
    
            if(!Number.TryParseInt32( s, style, info, out i ))
            {
                return false;
            }
    
            if((style & NumberStyles.AllowHexSpecifier) != 0)
            { // We are parsing a hexadecimal number
                if((i < 0) || i > Byte.MaxValue)
                {
                    return false;
                }
    
                result = (sbyte)i;
                return true;
            }
    
            if(i < MinValue || i > MaxValue)
            {
                return false;
            }
    
            result = (sbyte)i;
            return true;
        }

        #region IConvertible

        public TypeCode GetTypeCode()
        {
            return TypeCode.SByte;
        }
    
    
        /// <internalonly/>
        bool IConvertible.ToBoolean( IFormatProvider provider )
        {
            return Convert.ToBoolean( m_value );
        }
    
        /// <internalonly/>
        char IConvertible.ToChar( IFormatProvider provider )
        {
            return Convert.ToChar( m_value );
        }
    
        /// <internalonly/>
        sbyte IConvertible.ToSByte( IFormatProvider provider )
        {
            return m_value;
        }
    
        /// <internalonly/>
        byte IConvertible.ToByte( IFormatProvider provider )
        {
            return Convert.ToByte( m_value );
        }
    
        /// <internalonly/>
        short IConvertible.ToInt16( IFormatProvider provider )
        {
            return Convert.ToInt16( m_value );
        }
    
        /// <internalonly/>
        ushort IConvertible.ToUInt16( IFormatProvider provider )
        {
            return Convert.ToUInt16( m_value );
        }
    
        /// <internalonly/>
        int IConvertible.ToInt32( IFormatProvider provider )
        {
            return m_value;
        }
    
        /// <internalonly/>
        uint IConvertible.ToUInt32( IFormatProvider provider )
        {
            return Convert.ToUInt32( m_value );
        }
    
        /// <internalonly/>
        long IConvertible.ToInt64( IFormatProvider provider )
        {
            return Convert.ToInt64( m_value );
        }
    
        /// <internalonly/>
        ulong IConvertible.ToUInt64( IFormatProvider provider )
        {
            return Convert.ToUInt64( m_value );
        }
    
        /// <internalonly/>
        float IConvertible.ToSingle( IFormatProvider provider )
        {
            return Convert.ToSingle( m_value );
        }
    
        /// <internalonly/>
        double IConvertible.ToDouble( IFormatProvider provider )
        {
            return Convert.ToDouble( m_value );
        }
    
        /// <internalonly/>
        Decimal IConvertible.ToDecimal( IFormatProvider provider )
        {
            return Convert.ToDecimal( m_value );
        }
    
        /// <internalonly/>
        DateTime IConvertible.ToDateTime( IFormatProvider provider )
        {
#if EXCEPTION_STRINGS
            throw new InvalidCastException( Environment.GetResourceString( "InvalidCast_FromTo", "SByte", "DateTime" ) );
#else
            throw new InvalidCastException();
#endif
        }
    
        /// <internalonly/>
        Object IConvertible.ToType( Type type, IFormatProvider provider )
        {
            return Convert.DefaultToType( (IConvertible)this, type, provider );
        }

        #endregion
    }
}