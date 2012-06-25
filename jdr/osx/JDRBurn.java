//
//  JDRBurn.java
//  JDRLib
//
//  Created by Eddy Zavaleta on 09/Jun/07.
//  Copyright 2007 Click Sistemas. All rights reserved.
//

package jdrlib;

import java.util.Properties;

public class JDRBurn {
	public static int MULTISESSION = 0;               /* APPENDABLES CD'S */
	public static int SINGLESESSION = 1;              /* CLOSED CD'S */
	//public static int SINGLESESSION_MULTITRACK = 2; /* LIKE AUDIO CD'S */
	
    private native long nativeCreate(long device);
    private native void nativeRelease(long burn);
    private native void nativeAbort(long burn);
    private native boolean nativeWrite(long burn, long layout);
    private native long nativeGetDevice(long burn);
    private native String[][] nativeGetStatus(long burn);
    private native String[][] nativeGetProperties(long burn);
    private native void nativeSetProperties(long burn, String[][] props);

    private long burn = 0;
    
    public JDRBurn(JDRDevice device) {
        this.burn = nativeCreate(device.getDevice());
    }
    
    public void abort() {
        nativeAbort(burn);
    }
    
    public Properties getInfo() {
        Properties props = new Properties();
        String[][] pairs = nativeGetProperties(burn);
        
        if (pairs != null) {
            for (int i = 0; i < pairs.length; i++) {
                props.setProperty(pairs[i][0], pairs[i][1]);
            }
        }
        
        return props;
    }
    
    public void setProperties(Properties props) {
        
    }
    
    public Properties getStatus() {
        Properties props = new Properties();
        String[][] pairs = nativeGetStatus(burn);
        
        if (pairs != null) {
            for (int i = 0; i < pairs.length; i++) {
                props.setProperty(pairs[i][0], pairs[i][1]);
            }
        }
        
        return props;
    }
    
    public JDRDevice getDevice() {
        long device = nativeGetDevice(burn);
        
        return new JDRDevice(device);
    }
    
    public boolean write(JDRTreeNode tree) {
        return write(tree, MULTISESSION);
    }
	
    public boolean write(JDRTreeNode tree, int mode) {
        
        return true;
    }
    
    public void release() {
        nativeRelease(burn);
        burn = 0;
    }
}
