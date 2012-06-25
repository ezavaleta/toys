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
    private static native long[] getDevices();
    private static native long getDeviceFromName(String name);
    private native boolean lock(long device);
    private native void unlock(long device);
    private native void acquireReservation(long device);
    private native void releaseReservation(long device);
    private native String[][] getDeviceInfo(long device);
    private native String[][] getDeviceStatus(long device);
    private native boolean eject(long device);
    private native boolean isValid(long device);
    private native boolean openTray(long device);
    private native boolean closeTray(long device);
    
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
        long[] devs = getDevices();
        List devList = new ArrayList(devs.length);
        
        for (int i = 0; i < devs.length; i++) {
            devList.add(new JDRDevice(devs[i]));
        }
        
        return devList;
    }
    
    public static JDRDevice deviceFromName(String name) {
        long dev = getDeviceFromName(name);
        
        if (dev == 0) {
            return null;
        }
        
        return new JDRDevice(dev);
    }
    
    public boolean lock() {
        return lock(device);
    }
    
    public void unlock() {
        unlock(device);
    }
    
    public void reserve() {
        acquireReservation(device);
    }
    
    public void releaseReservation() {
        releaseReservation(device);
    }
    
    public Properties getInfo() {
        Properties props = new Properties();
        String[][] pairs = getDeviceInfo(device);
        
        if (pairs != null) {
            for (int i = 0; i < pairs.length; i++) {
                props.setProperty(pairs[i][0], pairs[i][1]);
            }
        }
        
        return props;
    }
    
    public Properties getStatus() {
        Properties props = new Properties();
        String[][] pairs = getDeviceStatus(device);
        
        if (pairs != null) {
            for (int i = 0; i < pairs.length; i++) {
                props.setProperty(pairs[i][0], pairs[i][1]);
            }
        }
        
        return props;
    }
    
    public boolean eject() {
        return eject(device);
    }
    
    public boolean isValid() {
        return isValid(device);
    }
    
    public boolean openTray() {
        return openTray(device);
    }
    
    public boolean closeTray() {
        return closeTray(device);
    }
    
    public String toString() {
        String res = "JDRDevice[id:" + device;
        
        return res + "]";
    }
}
