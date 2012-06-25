/* DO NOT EDIT THIS FILE - it is machine generated */
#include <jni.h>
/* Header for class jdrlib_JDRLib */

#ifndef _Included_jdrlib_JDRLib
#define _Included_jdrlib_JDRLib
#ifdef __cplusplus
extern "C" {
#endif
#ifdef __cplusplus
}
#endif
#endif
/* Header for class jdrlib_JDRDevice */

#ifndef _Included_jdrlib_JDRDevice
#define _Included_jdrlib_JDRDevice
#ifdef __cplusplus
extern "C" {
#endif
/*
 * Class:     jdrlib_JDRDevice
 * Method:    nativeGetDevices
 * Signature: ()[J
 */
JNIEXPORT jlongArray JNICALL Java_jdrlib_JDRDevice_nativeGetDevices
  (JNIEnv *, jclass);

/*
 * Class:     jdrlib_JDRDevice
 * Method:    nativeGetDeviceFromName
 * Signature: (Ljava/lang/String;)J
 */
JNIEXPORT jlong JNICALL Java_jdrlib_JDRDevice_nativeGetDeviceFromName
  (JNIEnv *, jclass, jstring);

/*
 * Class:     jdrlib_JDRDevice
 * Method:    nativeLock
 * Signature: (J)Z
 */
JNIEXPORT jboolean JNICALL Java_jdrlib_JDRDevice_nativeLock
  (JNIEnv *, jobject, jlong);

/*
 * Class:     jdrlib_JDRDevice
 * Method:    nativeUnlock
 * Signature: (J)V
 */
JNIEXPORT void JNICALL Java_jdrlib_JDRDevice_nativeUnlock
  (JNIEnv *, jobject, jlong);

/*
 * Class:     jdrlib_JDRDevice
 * Method:    nativeAcquireReservation
 * Signature: (J)V
 */
JNIEXPORT void JNICALL Java_jdrlib_JDRDevice_nativeAcquireReservation
  (JNIEnv *, jobject, jlong);

/*
 * Class:     jdrlib_JDRDevice
 * Method:    nativeReleaseReservation
 * Signature: (J)V
 */
JNIEXPORT void JNICALL Java_jdrlib_JDRDevice_nativeReleaseReservation
  (JNIEnv *, jobject, jlong);

/*
 * Class:     jdrlib_JDRDevice
 * Method:    nativeGetInfo
 * Signature: (J)[[Ljava/lang/String;
 */
JNIEXPORT jobjectArray JNICALL Java_jdrlib_JDRDevice_nativeGetInfo
  (JNIEnv *, jobject, jlong);

/*
 * Class:     jdrlib_JDRDevice
 * Method:    nativeGetStatus
 * Signature: (J)[[Ljava/lang/String;
 */
JNIEXPORT jobjectArray JNICALL Java_jdrlib_JDRDevice_nativeGetStatus
  (JNIEnv *, jobject, jlong);

/*
 * Class:     jdrlib_JDRDevice
 * Method:    nativeEject
 * Signature: (J)Z
 */
JNIEXPORT jboolean JNICALL Java_jdrlib_JDRDevice_nativeEject
  (JNIEnv *, jobject, jlong);

/*
 * Class:     jdrlib_JDRDevice
 * Method:    nativeIsValid
 * Signature: (J)Z
 */
JNIEXPORT jboolean JNICALL Java_jdrlib_JDRDevice_nativeIsValid
  (JNIEnv *, jobject, jlong);

/*
 * Class:     jdrlib_JDRDevice
 * Method:    nativeOpenTray
 * Signature: (J)Z
 */
JNIEXPORT jboolean JNICALL Java_jdrlib_JDRDevice_nativeOpenTray
  (JNIEnv *, jobject, jlong);

/*
 * Class:     jdrlib_JDRDevice
 * Method:    nativeCloseTray
 * Signature: (J)Z
 */
JNIEXPORT jboolean JNICALL Java_jdrlib_JDRDevice_nativeCloseTray
  (JNIEnv *, jobject, jlong);

#ifdef __cplusplus
}
#endif
#endif
/* Header for class jdrlib_JDRBurn */

#ifndef _Included_jdrlib_JDRBurn
#define _Included_jdrlib_JDRBurn
#ifdef __cplusplus
extern "C" {
#endif
/*
 * Class:     jdrlib_JDRBurn
 * Method:    nativeCreate
 * Signature: (J)J
 */
JNIEXPORT jlong JNICALL Java_jdrlib_JDRBurn_nativeCreate
  (JNIEnv *, jobject, jlong);

/*
 * Class:     jdrlib_JDRBurn
 * Method:    nativeRelease
 * Signature: (J)V
 */
JNIEXPORT void JNICALL Java_jdrlib_JDRBurn_nativeRelease
  (JNIEnv *, jobject, jlong);

/*
 * Class:     jdrlib_JDRBurn
 * Method:    nativeAbort
 * Signature: (J)V
 */
JNIEXPORT void JNICALL Java_jdrlib_JDRBurn_nativeAbort
  (JNIEnv *, jobject, jlong);

/*
 * Class:     jdrlib_JDRBurn
 * Method:    nativeWrite
 * Signature: (JJ)Z
 */
JNIEXPORT jboolean JNICALL Java_jdrlib_JDRBurn_nativeWrite
  (JNIEnv *, jobject, jlong, jlong);

/*
 * Class:     jdrlib_JDRBurn
 * Method:    nativeGetDevice
 * Signature: (J)J
 */
JNIEXPORT jlong JNICALL Java_jdrlib_JDRBurn_nativeGetDevice
  (JNIEnv *, jobject, jlong);

/*
 * Class:     jdrlib_JDRBurn
 * Method:    nativeGetStatus
 * Signature: (J)[[Ljava/lang/String;
 */
JNIEXPORT jobjectArray JNICALL Java_jdrlib_JDRBurn_nativeGetStatus
  (JNIEnv *, jobject, jlong);

/*
 * Class:     jdrlib_JDRBurn
 * Method:    nativeGetProperties
 * Signature: (J)[[Ljava/lang/String;
 */
JNIEXPORT jobjectArray JNICALL Java_jdrlib_JDRBurn_nativeGetProperties
  (JNIEnv *, jobject, jlong);

/*
 * Class:     jdrlib_JDRBurn
 * Method:    nativeSetProperties
 * Signature: (J[[Ljava/lang/String;)V
 */
JNIEXPORT void JNICALL Java_jdrlib_JDRBurn_nativeSetProperties
  (JNIEnv *, jobject, jlong, jobjectArray);

#ifdef __cplusplus
}
#endif
#endif