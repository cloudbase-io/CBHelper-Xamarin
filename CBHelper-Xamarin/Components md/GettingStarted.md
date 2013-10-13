Cloudbase.io with Xamarin
=========================

The cloudbase.io client library for Xamarin consists of three DLLs. A main CBHelper dll and two additional DLLs for each platform: iOS and Android.

Your project will need the main CBHelper dll as well as the platform-specific dll. Your classes will need to use the **Cloudbase** namespace.

To initialize the client library create a new CBHelper object with your application's unique codes from the management console.


	CBHelper helper = new CBHelper (
    	"xamarin-app-code", // the application code on cloudbase.io 
    	"aa11aa", // the application unique code on cloudbase.io 
    	new iOSDeviceInfo () // the CBDeviceInfo object, in this case for iOS.
    						// If you are building for Android it would be AndroidDeviceInfo
	);
 
	helper.SetPassword("app-password-md5");
	
Once the helper object is initialized you will have access to all of the cloudbase.io API. For the complete documentation and reference see the [Windows Documentation][http://cloudbase.io/documentation/windows-phone-8/get-started].

The example below inserts a new object in the Cloud Database and then retrieves it.

	using System.Collections;
	using System.Collections.Generic;
				
	using Cloudbase;
	using Cloudbase.DataCommands;
 
	public class MyClass {
		// Here we initialize the helper object
		void MyClass () {
			// This is all you need to do to initialize the CBHelper object and
			// make your first connection to cloudbase.io
			CBHelper helper  = new CBHelper("your-app-code", "your-app-unique-code", new iOSDeviceInfo());
			helper.SetPassword("app-password-md5");
 
			// Now we insert a new record in the cloud database
			// in this case we are send an object of type AppUser
			// defined in our application
			AppUser newUser = new AppUser();
			newUser.FirstName = "Cloud";
			newUser.LastName = "Base";
			newUser.Address = ".io";
			// Send the insert to the cloud database in a collection named players
			helper.InsertDocument("players", newUser, delegate(CBResponseInfo resp) {
            	Debug.Log(resp.OutputString);
 
           		// Now that the insert is complete we use create a search
            	// condition to retrieve the data from the cloud database.
           		// We are simply looking for a record where the FirstName field
				// is Cloud
				CBHelperSearchCondition playerSearch = new CBHelperSearchCondition(
					"FirstName", 
					CBConditionOperator.CBOperatorEqual, 
					"Cloud");
				
				helper.SearchDocument("players", mySearch, delegate(CBResponseInfo respSearch) {
					// The Data property of the response object will contain
					// a List of Dictionaries with all of the records retrieved.
					// Additionally you could also parse the OutputString with
					// the included NewtonSoft parser directly to its object representation 
					// in your game
					List<AppUser> SearchOutput = JsonReader.Deserialize<List<AppUser>>(respSearch.OutputString);
					return true;
				});
				return true;
			});
		}
	}