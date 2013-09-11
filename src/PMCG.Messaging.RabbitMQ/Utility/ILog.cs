﻿using System;


namespace PMCG.Messaging.RabbitMQ.Utility
{
	public interface ILog
	{
		void DebugFormat(
			string formatMessage,
			params object[] arguments);

		void Info();

		void Info(
			string message);

		void InfoFormat(
			string formatMessage,
			params object[] arguments);

		void ErrorFormat(
			string formatMessage,
			params object[] arguments);
	}
}