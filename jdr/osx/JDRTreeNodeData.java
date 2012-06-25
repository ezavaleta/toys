//
//  JDRTreeNodeData.java
//  JDRLib
//
//  Created by Eddy Zavaleta on 09/Jun/07.
//  Copyright 2007 Click Sistemas. All rights reserved.
//

package jdrlib;

import java.io.File;

public abstract class JDRTreeNodeData implements Comparable {
    protected String path = null;
    protected String name = null;
    
    public static JDRTreeNodeData nodeDataWithPath(String path) {
        File file = new File(path);

		if ( file.exists() ) {
			if (file.isDirectory()) {
				return new JDRFolderNodeData(file.getAbsolutePath(), file.getName());
			}
			else {
				return new JDRFileNodeData(file.getAbsolutePath(), file.getName());
			}
		}
		
		return null;
    }

    public static JDRTreeNodeData nodeDataWithName(String name) {
        return new JDRFolderNodeData(name);
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getName() {
        return name;
    }

    public abstract boolean isExpandable();
    public abstract boolean isVirtual();
    public abstract String getKind();

    public String makeVirtual() {
        String res = path;
        
        path = null;
        
        return res;
    }

    public int compareTo(Object o) {
        return name.compareToIgnoreCase(o.toString());
    }
}
