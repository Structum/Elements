#!/bin/bash

# Variables
VERSION="$1"
RELEASE="$2"
CURR_FOLDER="$PWD"

# Colors
RED=`tput setaf 1`
GREEN=`tput setaf 2`
BLUE=`tput setaf 6`
NC=`tput setaf 7`


echo "Structum Elements Build Script"
echo "Copyright (C) $YEAR Structum, Inc. All rights reserved."
echo ""

if [ -z "$VERSION" ]
then
	echo "$RED Please enter the build version. e.g. 3.4.5.6$NC"
    exit 1
fi

if [ -z "$RELEASE" ]
then
	RELEASE="Debug"
fi

# Header
echo "$BLUE *** Building Structum.Elements v$VERSION in $RELEASE mode.$NC"
cd ../src/Elements/
dotnet restore
dotnet build -c $RELEASE /p:version=$VERSION --no-incremental
dotnet pack /p:version=$VERSION
echo ""
cd $CURR_FOLDER

echo "$BLUE *** Building Structum.Elements.Web v$VERSION in $RELEASE mode.$NC"
cd ../src/Elements.Web/
dotnet restore
dotnet build -c $RELEASE /p:version=$VERSION --no-incremental
dotnet pack /p:version=$VERSION
cd $CURR_FOLDER

echo ""
echo "$GREEN Done.$NC"
