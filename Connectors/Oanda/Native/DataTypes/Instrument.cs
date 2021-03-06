﻿#region S# License
/******************************************************************************************
NOTICE!!!  This program and source code is owned and licensed by
StockSharp, LLC, www.stocksharp.com
Viewing or use of this code requires your acceptance of the license
agreement found at https://github.com/StockSharp/StockSharp/blob/master/LICENSE
Removal of this comment is a violation of the license agreement.

Project: StockSharp.Oanda.Native.DataTypes.Oanda
File: Instrument.cs
Created: 2015, 11, 11, 2:32 PM

Copyright 2010 by StockSharp, LLC
*******************************************************************************************/
#endregion S# License
namespace StockSharp.Oanda.Native.DataTypes
{
	using Newtonsoft.Json;

	class Instrument
    {
		[JsonProperty("instrument")]
		public string Code { get; set; }

		[JsonProperty("displayName")]
        public string DisplayName { get; set; }

		[JsonProperty("pip")]
		public double Pip { get; set; }

		[JsonProperty("maxTradeUnits")]
		public int MaxTradeUnits { get; set; }

		[JsonProperty("halted")]
		public bool Halted { get; set; }
    }
}