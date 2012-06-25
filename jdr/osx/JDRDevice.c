/*
 *  JDRDevice.c
 *  JDRLib
 *
 *  Created by Eddy Zavaleta on 09/Jun/07.
 *  Copyright 2007 Click Sistemas. All rights reserved.
 *
 */

#include <CoreFoundation/CoreFoundation.h>
#include <DiscRecording/DiscRecording.h>
#include "JDRLib.h"

JNIEXPORT jlongArray JNICALL Java_jdrlib_JDRDevice_nativeGetDevices(JNIEnv *env, jclass cls)
{
	CFArrayRef devs = DRCopyDeviceArray();
	CFIndex i, max = CFArrayGetCount(devs);
	jlongArray res;
	jlong *devsArr;
	
	devsArr = calloc(max, 8);
	
	if (devsArr == NULL) {
		return NULL; /* out of memory error thrown */
	}
	
	res = (*env)->NewLongArray(env, max);
	
	if (res == NULL) {
		return NULL; /* out of memory error thrown */
	}
	
	for (i = 0; i < max; i++) {
		long dev = (long)CFArrayGetValueAtIndex(devs, i);
		devsArr[i] = dev;
		//printf("NDEBUG: %ld\n", dev);
	}

	(*env)->SetLongArrayRegion(env, res, 0, max, devsArr);

	free(devsArr);

	CFRelease(devs);
	
	return res;
}

JNIEXPORT jlong JNICALL Java_jdrlib_JDRDevice_nativeGetDeviceFromName(JNIEnv *env, jclass cls, jstring str)
{
	jlong res;
	const jchar *chars = (*env)->GetStringChars(env, str, NULL);
	CFStringRef name = CFStringCreateWithCharacters(kCFAllocatorDefault, chars, (*env)->GetStringLength(env, str));
	
	(*env)->ReleaseStringChars(env, str, chars);

	res = (long)DRDeviceCopyDeviceForBSDName(name);
	
	return res;
}

JNIEXPORT jboolean JNICALL Java_jdrlib_JDRDevice_nativeLock(JNIEnv *env, jobject obj, jlong dev)
{
	DRDeviceRef device = (DRDeviceRef)(long)dev;
	
	return DRDeviceAcquireExclusiveAccess(device) == noErr ? 1 : 0;
}

JNIEXPORT void JNICALL Java_jdrlib_JDRDevice_nativeUnlock(JNIEnv *env, jobject obj, jlong dev)
{
	DRDeviceRef device = (DRDeviceRef)(long)dev;
	
	DRDeviceReleaseExclusiveAccess(device);
}

JNIEXPORT void JNICALL Java_jdrlib_JDRDevice_nativeAcquireReservation(JNIEnv *env, jobject obj, jlong dev)
{
	DRDeviceRef device = (DRDeviceRef)(long)dev;
	
	DRDeviceReleaseMediaReservation(device);
}

JNIEXPORT void JNICALL Java_jdrlib_JDRDevice_nativeReleaseReservation(JNIEnv *env, jobject obj, jlong dev)
{
	DRDeviceRef device = (DRDeviceRef)(long)dev;
	
	DRDeviceReleaseMediaReservation(device);
}

JNIEXPORT jobjectArray JNICALL Java_jdrlib_JDRDevice_nativeGetInfo(JNIEnv *env, jobject obj, jlong dev)
{
//	DRDeviceRef device = (DRDeviceRef)(long)dev;
//	CFDictionaryRef dict = DRDeviceCopyInfo(device);
//	CFIndex i, max = CFDictionaryGetCount(dict);
//	void **keys, **values;
//	jobjectArray res;
//	jclass clsStrArr = (*env)->FindClass(env, "[Ljava/lang/String;");
//	jclass clsStr = (*env)->FindClass(env, "Ljava/lang/String;");
//	
//	if (clsStrArr == NULL || clsStr == NULL) {
//		return NULL; /* exception thrown */
//	}
//	
//	printf("NDEBUG: CLS FOUND\n");
//	
//	res = (*env)->NewObjectArray(env, max, clsStrArr, NULL);
//	
//	if (res == NULL) {
//		return NULL; /* out of memory error thrown */
//	}
//	
//	printf("NDEBUG: NEW jobjectArray\n");
//	
//	keys = (void **)calloc(max,4);
//	values = (void **)calloc(max,4);
//	
//	if (keys == NULL || values == NULL) {
//		return NULL; /* out of memory error thrown */
//	}
//	
//	printf("NDEBUG: N-ARRAYS RESERVED\n");
//	
//	CFDictionaryGetKeysAndValues(dict, (const void **)keys, (const void **)values);
//	
//	printf("NDEBUG: CFDictionaryGetKeysAndValues\n");
//	
//	for (i = 0; i < max; i++) {
//		CFRange range;
//		UniChar *charBuf;
//		jstring str;
//		jobjectArray pair = (*env)->NewObjectArray(env, 2, clsStr, NULL);
//		
//		if (pair == NULL) {
//			(*env)->DeleteLocalRef(env, res);
//			res = NULL;
//			break; /* out of memory error thrown */
//		}
//
//		printf("NDEBUG: pair -> NewObjectArray\n");
//		
//		printf("NDEBUG: KEY = ");
//		CFShowStr(keys[i]);
//		range.location = 0;
//		range.length = CFStringGetLength(keys[i]);
//		charBuf = calloc(range.length, 2);
//		CFStringGetCharacters(keys[i], range, charBuf);
//		str = (*env)->NewString(env, (jchar *)charBuf, (jsize)range.length);
//		(*env)->SetObjectArrayElement(env, pair, 0, str);
//		(*env)->DeleteLocalRef(env, str);
//		free(charBuf);
//		
//		printf("NDEBUG: pair -> SetObjectArrayElement -> key\n");
//		
//		printf("NDEBUG: VALUE = ");
//		CFShowStr(values[i]);
//		range.location = 0;
//		range.length = CFStringGetLength(values[i]);
//		charBuf = calloc(range.length, 2);
//		CFStringGetCharacters(values[i], range, charBuf);
//		str = (*env)->NewString(env, (jchar *)charBuf, (jsize)range.length);
//		(*env)->SetObjectArrayElement(env, pair, 1, str);
//		(*env)->DeleteLocalRef(env, str);
//		free(charBuf);
//		
//		printf("NDEBUG: pair -> SetObjectArrayElement -> value\n");
//		
//		(*env)->SetObjectArrayElement(env, res, i, pair);
//		(*env)->DeleteLocalRef(env, pair);
//		printf("NDEBUG: res -> SetObjectArrayElement -> pair\n");
//	}
//	
//	printf("NDEBUG: CLEANINNG...");
//	
//	free(keys);
//	free(values);
//	CFRelease(dict);
//	
//	printf("OK\n");
//	
//	return res;
	return NULL;
}

JNIEXPORT jobjectArray JNICALL Java_jdrlib_JDRDevice_nativeGetStatus(JNIEnv *env, jobject obj, jlong dev)
{
	return NULL;
}

JNIEXPORT jboolean JNICALL Java_jdrlib_JDRDevice_nativeEject(JNIEnv *env, jobject obj, jlong dev)
{
	DRDeviceRef device = (DRDeviceRef)(long)dev;
	
	return DRDeviceEjectMedia(device) == noErr ? 1 : 0;
}

JNIEXPORT jboolean JNICALL Java_jdrlib_JDRDevice_nativeIsValid(JNIEnv *env, jobject obj, jlong dev)
{
	DRDeviceRef device = (DRDeviceRef)(long)dev;
	
	return DRDeviceIsValid(device) == noErr ? 1 : 0;
}

JNIEXPORT jboolean JNICALL Java_jdrlib_JDRDevice_nativeOpenTray(JNIEnv *env, jobject obj, jlong dev)
{
	DRDeviceRef device = (DRDeviceRef)(long)dev;
	
	return DRDeviceOpenTray(device) == noErr ? 1 : 0;
}

JNIEXPORT jboolean JNICALL Java_jdrlib_JDRDevice_nativeCloseTray(JNIEnv *env, jobject obj, jlong dev)
{
	DRDeviceRef device = (DRDeviceRef)(long)dev;
	
	return DRDeviceCloseTray(device) == noErr ? 1 : 0;
}
