//
//  JDRDevice.java
//  JDRLib
//
//  Created by Eddy Zavaleta on 09/Jun/07.
//  Copyright 2007 Click Sistemas. All rights reserved.
//

package jdrlib;

import java.util.ArrayList;
import java.util.List;
import java.util.Properties;

public class JDRDevice {
    private static native long[] nativeGetDevices();
    private static native long nativeGetDeviceFromName(String name);
    private native boolean nativeLock(long device);
    private native void nativeUnlock(long device);
    private native void nativeAcquireReservation(long device);
    private native void nativeReleaseReservation(long device);
    private native String[][] nativeGetInfo(long device);
    private native String[][] nativeGetStatus(long device);
    private native boolean nativeEject(long device);
    private native boolean nativeIsValid(long device);
    private native boolean nativeOpenTray(long device);
    private native boolean nativeCloseTray(long device);
    
    private long device = 0;
    
    static {
        System.loadLibrary( "JDRNativeLib" );
    }
    
    JDRDevice(long device) {
        this.device = device;
    }

    long getDevice() {
        return device;
    }

    public static List listDevices() {
        long[] devs = nativeGetDevices();
        List devList = new ArrayList(devs.length);
        
        for (int i = 0; i < devs.length; i++) {
            devList.add(new JDRDevice(devs[i]));
        }
        
        return devList;
    }
    
    public static JDRDevice deviceFromName(String name) {
        long dev = nativeGetDeviceFromName(name);
        
        if (dev == 0) {
            return null;
        }
        
        return new JDRDevice(dev);
    }
    
    public boolean lock() {
        return nativeLock(device);
    }
    
    public void unlock() {
        nativeUnlock(device);
    }
    
    public void reserve() {
        nativeAcquireReservation(device);
    }
    
    public void releaseReservation() {
        nativeReleaseReservation(device);
    }
    
    public Properties getInfo() {
        Properties props = new Properties();
        String[][] pairs = nativeGetInfo(device);
        
        if (pairs != null) {
            for (int i = 0; i < pairs.length; i++) {
                props.setProperty(pairs[i][0], pairs[i][1]);
            }
        }
        
        return props;
    }
    
    public Properties getStatus() {
        Properties props = new Properties();
        String[][] pairs = nativeGetStatus(device);
        
        if (pairs != null) {
            for (int i = 0; i < pairs.length; i++) {
                props.setProperty(pairs[i][0], pairs[i][1]);
            }
        }
        
        return props;
    }
    
    public boolean eject() {
        return nativeEject(device);
    }
    
    public boolean isValid() {
        return nativeIsValid(device);
    }
    
    public boolean openTray() {
        return nativeOpenTray(device);
    }
    
    public boolean closeTray() {
        return nativeCloseTray(device);
    }
    
    public String toString() {
        String res = "JDRDevice[id:" + device;
        
        return res + "]";
    }
}
