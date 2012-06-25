/*
 *  JDRBurn.c
 *  JDRLib
 *
 *  Created by Eddy Zavaleta on 09/Jun/07.
 *  Copyright 2007 Click Sistemas. All rights reserved.
 *
 */

#include <CoreFoundation/CoreFoundation.h>
#include <DiscRecording/DiscRecording.h>
#include "JDRLib.h"

JNIEXPORT jlong JNICALL Java_jdrlib_JDRBurn_nativeCreate(JNIEnv *env, jobject obj, jlong dev)
{
	jlong res;
	DRDeviceRef device = (DRDeviceRef)(long)dev;
	DRBurnRef burn = DRBurnCreate(device);

	res = (long)burn;
	
	return res;
}

JNIEXPORT void JNICALL Java_jdrlib_JDRBurn_nativeRelease(JNIEnv *env, jobject obj, jlong pBurn)
{
	DRBurnRef burn = (DRBurnRef)(long)pBurn;
	
	free(burn);
}

JNIEXPORT void JNICALL Java_jdrlib_JDRBurn_nativeAbort(JNIEnv *env, jobject obj, jlong pBurn)
{
	DRBurnRef burn = (DRBurnRef)(long)pBurn;
	
	DRBurnAbort(burn);
}

JNIEXPORT jboolean JNICALL Java_jdrlib_JDRBurn_nativeWrite(JNIEnv *env, jobject obj, jlong pBurn, jlong pLayout)
{
	DRBurnRef burn = (DRBurnRef)(long)pBurn;
	CFTypeRef layout = (CFTypeRef)(long)pLayout;

	return DRBurnWriteLayout(burn, layout)== noErr ? 1 : 0;
}

JNIEXPORT jlong JNICALL Java_jdrlib_JDRBurn_nativeGetDevice(JNIEnv *env, jobject obj, jlong pBurn)
{
	jlong res;
	DRBurnRef burn = (DRBurnRef)(long)pBurn;
	DRDeviceRef device = DRBurnGetDevice(burn);
	
	res = (long)device;
	
	return res;
}

JNIEXPORT jobjectArray JNICALL Java_jdrlib_JDRBurn_nativeGetStatus(JNIEnv *env, jobject obj, jlong pBurn)
{
	DRBurnRef burn = (DRBurnRef)(long)pBurn;
	CFDictionaryRef dict = DRBurnCopyStatus(burn);
	
}

JNIEXPORT jobjectArray JNICALL Java_jdrlib_JDRBurn_nativeGetProperties(JNIEnv *env, jobject obj, jlong pBurn)
{
	DRBurnRef burn = (DRBurnRef)(long)pBurn;
	CFDictionaryRef dict = DRBurnGetProperties(burn);
	CFBooleanRef boolValue;
	
	/*
	kDRBurnAppendableKey CFBoolean
	kDRBurnCompletionActionKey CFString
	kDRBurnFailureActionKey CFString
	kDRBurnOverwriteDiscKey CFBoolean
	kDRBurnRequestedSpeedKey CFNumber
	kDRBurnStrategyKey CFString CFArray
	kDRBurnTestingKey CFBoolean
	kDRBurnUnderrunProtectionKey CFBoolean
	kDRBurnVerifyDiscKey CFBoolean
	*/
	
	if (CFDictionaryGetValueIfPresent(dict, kDRBurnAppendableKey, (const void**)&boolValue)) {
		if (CFBooleanGetValue(boolValue)) {
			CFStringRef str = CFStringCreateWithFormat(NULL, NULL, CFSTR("%@"), boolValue);
			
			CFRelease(str);
		}
	}
	else {
		// set default value
	}
}

JNIEXPORT void JNICALL Java_jdrlib_JDRBurn_nativeSetProperties(JNIEnv *env, jobject obj, jlong pBurn, jobjectArray props)
{
	DRBurnRef burn = (DRBurnRef)(long)pBurn;

}
