﻿using System;
using Newtonsoft.Json;
using SQLite;
namespace Rosie
{
	public enum BriConversionType
	{
		Percent,
		Byte,
		Math,
	}

	public enum DeviceType
	{
		Switch,
		Thermostat,
	}
	public class Device
	{
		[PrimaryKey]
		public string Id { get; set; } 

		public string Name { get; set; }

		public string Description { get; set; }

		public DeviceType DeviceType { get; set; } = DeviceType.Switch;

		public string OffUrl { get; set; }

		public string OnUrl { get; set; }

		public BriConversionType ConversionType { get; set; }

		[Indexed]
		public string Service { get; set; } = WebRequestHandler.Identifier;

	}
}
