#!/bin/sh
javah -classpath "${BUILT_PRODUCTS_DIR}/JDRLibHeaders.jar" -force -o "${BUILT_PRODUCTS_DIR}/JDRLib.h" "jdrlib.JDRLib" "jdrlib.JDRDevice" "jdrlib.JDRBurn"
