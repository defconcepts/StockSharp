﻿#region S# License
/******************************************************************************************
NOTICE!!!  This program and source code is owned and licensed by
StockSharp, LLC, www.stocksharp.com
Viewing or use of this code requires your acceptance of the license
agreement found at https://github.com/StockSharp/StockSharp/blob/master/LICENSE
Removal of this comment is a violation of the license agreement.

Project: StockSharp.Quik.Lua.QuikPublic
File: LuaFixTransactionMessageAdapter.cs
Created: 2015, 11, 11, 2:32 PM

Copyright 2010 by StockSharp, LLC
*******************************************************************************************/
#endregion S# License
namespace StockSharp.Quik.Lua
{
	using System;
	using System.Security;

	using Ecng.Common;
	using Ecng.ComponentModel;
	using Ecng.Localization;

	using StockSharp.Fix;
	using StockSharp.Fix.Native;
	using StockSharp.Localization;
	using StockSharp.Messages;

	/// <summary>
	/// Адаптер сообщений для Quik LUA FIX.
	/// </summary>
	[Icon("Quik_logo.png")]
	[DisplayNameLoc("Quik LUA. Transactions")]
	[Doc("http://stocksharp.com/doc/html/769f74c8-6f8e-4312-a867-3dc6e8482636.htm")]
	[TargetPlatform(Languages.Russian)]
	[CategoryLoc(LocalizedStrings.RussiaKey)]
	public class LuaFixTransactionMessageAdapter : FixMessageAdapter
	{
		/// <summary>
		/// Создать <see cref="LuaFixTransactionMessageAdapter"/>.
		/// </summary>
		/// <param name="transactionIdGenerator">Генератор идентификаторов транзакций.</param>
		public LuaFixTransactionMessageAdapter(IdGenerator transactionIdGenerator)
			: base(transactionIdGenerator)
		{
			this.RemoveMarketDataSupport();

			Login = "quik";
			Password = "quik".To<SecureString>();
			Address = QuikTrader.DefaultLuaAddress;
			TargetCompId = "StockSharpTS";
			SenderCompId = "quik";
			//ExchangeBoard = ExchangeBoard.Forts;
			Version = FixVersions.Fix44_Lua;
			RequestAllPortfolios = true;
			MarketData = FixMarketData.None;
			TimeZone = TimeHelper.Moscow;
		}

		/// <summary>
		/// Создать для заявки типа <see cref="OrderTypes.Conditional"/> условие, которое поддерживается подключением.
		/// </summary>
		/// <returns>Условие для заявки. Если подключение не поддерживает заявки типа <see cref="OrderTypes.Conditional"/>, то будет возвращено <see langword="null"/>.</returns>
		public override OrderCondition CreateOrderCondition()
		{
			return new QuikOrderCondition();
		}

		/// <summary>
		/// Финальная инициализация условия заявки.
		/// </summary>
		/// <param name="ordType">Тип заявки.</param>
		/// <param name="condition">Условие заявки.</param>
		protected override void PostInitCondition(char ordType, OrderCondition condition)
		{
		}

		/// <summary>
		/// Записать данные по условию заявки.
		/// </summary>
		/// <param name="writer">Писатель FIX данных.</param>
		/// <param name="regMsg">Сообщение, содержащее информацию для регистрации заявки.</param>
		protected override void WriteOrderCondition(IFixWriter writer, OrderRegisterMessage regMsg)
		{
			writer.WriteOrderCondition((QuikOrderCondition)regMsg.Condition, DateTimeFormat);
		}

		/// <summary>
		/// Прочитать условие регистрации заявки <see cref="OrderRegisterMessage.Condition"/>.
		/// </summary>
		/// <param name="reader">Читатель данных.</param>
		/// <param name="tag">Тэг.</param>
		/// <param name="getCondition">Функция, возвращающая условие заявки.</param>
		/// <returns>Успешно ли обработаны данные.</returns>
		protected override bool ReadOrderCondition(IFixReader reader, FixTags tag, Func<OrderCondition> getCondition)
		{
			return reader.ReadOrderCondition(tag, TimeZone, DateTimeFormat, () => (QuikOrderCondition)getCondition());
		}
	}
}