#include <CoreFoundation/CoreFoundation.h>
#include <DiscRecording/DiscRecording.h>
#include <CoreServices/CoreServices.h>

int main (int argc, const char * argv[]) {
	/*
	CFArrayRef devs = DRCopyDeviceArray();
	CFIndex i, max = CFArrayGetCount(devs);

	for (i = 0; i < max; i++) {
		CFMutableStringRef str = CFStringCreateMutable(kCFAllocatorDefault, 0);
		DRDeviceRef dev = (DRDeviceRef)CFArrayGetValueAtIndex(devs, i);
		CFDictionaryRef info = DRDeviceCopyInfo(dev);
		
		CFStringAppend(str, (CFStringRef)CFDictionaryGetValue(info, kDRDeviceVendorNameKey));
		CFStringAppend(str, CFSTR(" "));
		CFStringAppend(str, (CFStringRef)CFDictionaryGetValue(info, kDRDeviceProductNameKey));
		
		CFShow(str);
		
		CFRelease(str);
	}

	CFRelease(devs);
	*/
	
	/*
	FSRef outRef;
	const char * path = "/Users/eddy/test.rtf";
	OSStatus err = FSPathMakeRef((const UInt8 *)path, &outRef, NULL);

	if(err == noErr) {
		FSDeleteObject( &outRef );
	}
	*/
	
	DRDeviceRef dev = DRDeviceCopyDeviceForBSDName(CFSTR("disk1"));
	DRBurnRef burn = DRBurnCreate(dev);
	float temp;
	CFNumberRef value;
	CFMutableDictionaryRef props = CFDictionaryCreateMutableCopy( NULL, 0, DRBurnGetProperties( burn ) );
	temp = DRDeviceKPSForCDXFactor(speed);
	value = CFNumberCreate(NULL, kCFNumberFloatType, &temp);
	
	kDRBurnAppendableKey CFBoolean
	kDRBurnCompletionActionKey
	kDRBurnFailureActionKey
	kDRBurnOverwriteDiscKey
	kDRBurnRequestedSpeedKey
	kDRBurnStrategyIsRequiredKey kCFBooleanFalse
	kDRBurnStrategyKey
	kDRBurnTestingKey CFBoolean
	kDRBurnUnderrunProtectionKey CFBoolean
	kDRBurnVerifyDiscKey CFBoolean
	
	CFDictionaryAddValue(props, kDRBurnAppendableKey, kCFBooleanTrue | kCFBooleanFalse );
	
	CFDictionaryAddValue(props, , value);
	CFDictionaryAddValue(props, , value);
	CFDictionaryAddValue(props, , value);
	CFDictionaryAddValue(props, , value);
	CFDictionaryAddValue(props, , value);
	CFDictionaryAddValue(props, , value);
	CFDictionaryAddValue(props, , value);
	CFDictionaryAddValue(props, , value);
	
	
	
	CFRelease(value);

	DRBurnSetProperties( burn, properties);
	CFRelease( props );
	
	CFRelease(burn);

	
    return 0;
}
