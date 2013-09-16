﻿using PMCG.Messaging.Client.Configuration;
using PMCG.Messaging.Client.Utility;
using RabbitMQ.Client;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace PMCGMessaging.RabbitMQ.Interactive
{
	public class Subscriber
	{
		private IConnection c_connection;
		private CancellationTokenSource c_cancellationTokenSource;
		private PMCG.Messaging.Client.Subscriber c_subscriber;


		public void Run_Where_We_Instruct_To_Stop_The_Broker()
		{
			this.InstantiateSubscriber();
			new Task(this.c_subscriber.Start).Start();

			Console.WriteLine("Stop the broker by running the following command as an admin");
			Console.WriteLine("\t rabbitmqctl.bat stop");
			Console.WriteLine("After stopping the broker hit enter to exit");
			Console.ReadLine();
		}


		public void Run_Where_We_Close_The_Connection_Using_The_DashBoard()
		{
			this.InstantiateSubscriber();
			new Task(this.c_subscriber.Start).Start();

			Console.WriteLine("Close the connection from the dashboard");
			Console.WriteLine("After closing the connecton hit enter to exit");
			Console.ReadLine();
		}


		public void Run_Where_We_Start_Then_Cancel_Token_And_Then_Close_Connection()
		{
			this.InstantiateSubscriber();
			new Task(this.c_subscriber.Start).Start();

			Console.WriteLine("Hit enter to cancel the token, should terminate the subscriber, subject to the dequeue timeout");
			Console.ReadLine();
			this.c_cancellationTokenSource.Cancel();

			Console.WriteLine("Hit enter to close connection (Channel should already be closed - check the dashboard)");
			Console.ReadLine();
			this.c_connection.Close();

			Console.WriteLine("Hit enter to exit");
			Console.ReadLine();
		}


		public void InstantiateSubscriber()
		{
			var _busConfigurationBuilder = new BusConfigurationBuilder();
			_busConfigurationBuilder.ConnectionUri = "amqp://guest:guest@localhost:5672/dev";
			_busConfigurationBuilder.DisconnectedMessagesStoragePath = @"D:\temp\rabbitdisconnectedmessages";

			this.InstantiateSubscriber(_busConfigurationBuilder.Build());
		}


		public void InstantiateSubscriber(
			BusConfiguration busConfiguration)
		{
			var _logger = new ConsoleLogger();
			this.c_connection = new ConnectionFactory { Uri = busConfiguration.ConnectionUri }.CreateConnection();
			this.c_cancellationTokenSource = new CancellationTokenSource();

			this.c_subscriber = new PMCG.Messaging.Client.Subscriber(
				_logger,
				this.c_connection,
				busConfiguration,
				this.c_cancellationTokenSource.Token);
		}
	}
}