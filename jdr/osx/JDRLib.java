//
//  JDRLib.java
//  JDRLib
//
//  Created by Eddy Zavaleta on 09/Jun/07.
//  Copyright (c) 2007 Click Sistemas. All rights reserved.
//

package jdrlib;

import java.util.*;

public class JDRLib {

    public static void main (String args[]) {
		JDRDevice dev;
        List lst = JDRDevice.listDevices();
		Iterator it = lst.iterator();
		
		System.out.println("TEST OF listDevices");
		
		for (; it.hasNext();) {
			dev = (JDRDevice)it.next();
			
			System.out.println(dev);
			System.out.println("TEST OF lock");
			System.out.println(dev.lock());
			System.out.println("TEST OF unlock");
			dev.unlock();
			System.out.println("TEST OF reserve");
			dev.reserve();
			System.out.println("TEST OF releaseReservation");
			dev.releaseReservation();
			System.out.println("TEST OF getInfo");
			dev.getInfo().list(System.out);
			
		}
		
		System.out.println("TEST OF deviceFromName");
		dev = JDRDevice.deviceFromName("disk1");
		if ( dev == null ) {
			System.out.println("disk1 not found");
		}
		else {
			System.out.println(dev);
		}
		
        JDRTreeNode root = JDRTreeNode.treeNodeWithData(
                                JDRTreeNodeData.nodeDataWithName("JDR Project"));
        JDRTreeNode childA = JDRTreeNode.treeNodeWithData(
                                JDRTreeNodeData.nodeDataWithPath("/Users/eddy/dev/JDRLib"));
        JDRTreeNode childB = JDRTreeNode.treeNodeWithData(
                                JDRTreeNodeData.nodeDataWithPath("/Users/eddy/dev/JDRTest"));
        
        root.addChild(childA);
        childA.addChild(childB);
        
		dev = JDRDevice.deviceFromName("disk1");
		
		JDRBurn burn = new JDRBurn(dev);
		burn.write(root);
    }
}
