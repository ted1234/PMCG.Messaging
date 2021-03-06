﻿using System;


namespace PMCG.Messaging.Client.Configuration
{
	public class MessageDelivery
	{
		public readonly string ExchangeName;
		public readonly string TypeHeader;
		public readonly MessageDeliveryMode DeliveryMode;
		public readonly Func<Message, string> RoutingKeyFunc;


		public MessageDelivery(
			string exchangeName,						// Can be empty where publishing via direct exchange,
			string typeHeader,
			MessageDeliveryMode deliveryMode,
			Func<Message, string> routingKeyFunc)
		{
			Check.RequireArgument("exchangeName", exchangeName, (exchangeName == string.Empty || exchangeName.Trim() != string.Empty));
			Check.RequireArgumentNotEmpty("typeHeader", typeHeader);
			Check.RequireArgument("deliveryMode", deliveryMode, Enum.IsDefined(typeof(MessageDeliveryMode), deliveryMode));
			Check.RequireArgumentNotNull("routingKeyFunc", routingKeyFunc);
			
			this.ExchangeName = exchangeName;
			this.TypeHeader = typeHeader;
			this.DeliveryMode = deliveryMode;
			this.RoutingKeyFunc = routingKeyFunc;
		}
	}
}