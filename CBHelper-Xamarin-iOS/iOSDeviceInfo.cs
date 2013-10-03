/* Copyright (c) 2013 Cloudbase.io Limited
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.Runtime.InteropServices;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Cloudbase
{
	/// <summary>
	/// Uses the MonoTouch framework to retrieve the device information used by cloudbase
	/// to register the client.
	/// </summary>
	public class iOSDeviceInfo : CBDeviceInfo
	{
		[DllImport(MonoTouch.Constants.SystemLibrary)]
		static internal extern int sysctlbyname([MarshalAs(UnmanagedType.LPStr)] string property, IntPtr output, IntPtr oldLen, IntPtr newp, uint newlen);

		public const string HardwareProperty = "hw.machine";

		public iOSDeviceInfo () {
			this.DeviceModel = this.loadDeviceModel () + " " + MonoTouch.Constants.Version;
			this.DeviceName = this.DeviceModel;
			this.DeviceType = "iOS";
			this.DeviceUniqueIdentifier = UIDevice.CurrentDevice.IdentifierForVendor.ToString ();
		}

		private string loadDeviceModel() {
			var pLen = Marshal.AllocHGlobal(sizeof(int));
			sysctlbyname(iOSDeviceInfo.HardwareProperty, IntPtr.Zero, pLen, IntPtr.Zero, 0);

			var length = Marshal.ReadInt32(pLen);

			if (length == 0) {
				Marshal.FreeHGlobal(pLen);

				return "Unknown";
			}

			var pStr = Marshal.AllocHGlobal(length);
			sysctlbyname(iOSDeviceInfo.HardwareProperty, pStr, pLen, IntPtr.Zero, 0);

			var hardwareStr = Marshal.PtrToStringAnsi(pStr);

			Marshal.FreeHGlobal(pLen);
			Marshal.FreeHGlobal(pStr);

			return hardwareStr;
		}
	}
}

