﻿using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;
using System.Threading.Tasks;

namespace Rosie
{
	public class DeviceDatabase
	{
		public SQLiteAsyncConnection DatabaseConnection { get; set;}

		public DeviceDatabase () : this ("devices.db")
		{

		}

		public DeviceDatabase (string databasePath)
		{
			DatabaseConnection = new SQLiteAsyncConnection (databasePath, true);
			var s = DatabaseConnection.CreateTablesAsync<Device,DeviceGroup,DeviceKeyGrouping, DeviceState> ().Result;
		}

		public static DeviceDatabase Shared { get; set; } = new DeviceDatabase ();

		public List<Device> TestDevices { get; set; } = new List<Device> {
			new Device{
				Name = "Bedroom Thermostat",
				Id = "720e95b3-9498-4e86-af8d-1f177c9e25b8",
				DeviceType = DeviceType.Thermostat
			},
			new Device{
				Name = "Kitchen Lights",
				Id = "476b66f1-29fe-42fe-bb9c-d17421bf5f1e",
			},
		};

		//public async void TestUpdates ()
		//{
		//	try {
		//		var device = TestDevices [0];
		//		var update = new DeviceState { DeviceId = device.Id, Key = "Temperature", Value = 70, DataType = DataTypes.Decimal, DataFormat = "F" };
		//		var json = update.ToSimpleObject ().ToJson ();
		//		await DatabaseConnection.InsertOrReplaceAsync (update);
		//		var resp = await DatabaseConnection.Table<DeviceState> ().Where (x => x.DeviceId == device.Id).ToListAsync();

		//		Console.WriteLine (resp [0]);
		//	} catch (Exception ex) {
		//		Console.WriteLine (ex);
		//	}

		//}

		public Task<Device> GetDevice (string id)
		{
			return DatabaseConnection.Table<Device> ().Where (x => x.Id == id).FirstOrDefaultAsync ();
		}

		public Task<List<Device>> GetAllDevices ()
		{
			return DatabaseConnection.Table<Device> ().ToListAsync ();
		}

		public async Task<bool> InsertDevice (Device device)
		{
			var s = await DatabaseConnection.InsertAsync (device);
			return s > 0;
		}

		public async Task<bool> DeleteDevice (Device device)
		{
			var s = await DatabaseConnection.DeleteAsync (device);
			return s > 0;
		}
	}
}

