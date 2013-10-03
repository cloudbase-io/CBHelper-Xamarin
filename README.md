Copyright (C) 2013 cloudbase.io
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

CBHelper-Xamarin
================

The cloudbase.io Xamarin helper class.

To start using the library register at https://manage.cloudbase.io/register and create an application.

This repository contains a solution to build the CBHelper-Xamarin.dll client library and its platform-specific counterparts:

The cloudbase.io helper class for Xamarin utilizes the Newtonsoft.Json package. While this package
is included in the solution you can get new updates and fixes using NuGet.

The library differs from the standard Windows Phone library in its initialization code:

	CBHelper helper = new CBHelper (
		"xamarin-app-code", // the application code on cloudbase.io 
		"aa11aa", // the application unique code on cloudbase.io 
		new iOSDeviceInfo () // the CBDeviceInfo object, in this case for iOS
	);
	
	// If you were building for Android you'd use the AndroidDeviceInfo class.
	
	helper.SetPassword("app-password-md5");
