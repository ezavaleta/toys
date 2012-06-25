//
//  Main.java
//  JDRTest
//
//  Created by Eddy Zavaleta on 10/Jun/07.
//  Copyright 2007 Click Sistemas. All rights reserved.
//

package jdrtest;

import jdrlib.JDRDevice;
import jdrlib.JDRTreeNode;
import jdrlib.JDRTreeNodeData;

/**
 *
 * @author eddy
 */
public class Main {
    
    /** Creates a new instance of Main */
    public Main() {
    }
    
    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        JDRTreeNode root = JDRTreeNode.treeNodeWithData(
                                JDRTreeNodeData.nodeDataWithName("JDRTest"));
        JDRTreeNode child1 = JDRTreeNode.treeNodeWithData(
                                JDRTreeNodeData.nodeDataWithPath("/Volumes/Datos/eddy/oficial"));
        JDRTreeNode child2 = JDRTreeNode.treeNodeWithData(
                                JDRTreeNodeData.nodeDataWithPath("/Volumes/Datos/eddy/gala-grunch"));
        
        root.addChild(child1);
        child1.addChild(child2);
        
        JDRDevice dev = JDRDevice.deviceFromName("disk1");
        
    }
    
}
