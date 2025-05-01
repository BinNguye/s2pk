# Makefile for building and packaging the Sims 2 .s2pk CLI tool

APP_NAME = s2pk
CONFIGURATION = Release
RUNTIME_LINUX = linux-x64
RUNTIME_MAC = osx-x64
OUTPUT_DIR = ./dist
APPIMAGE_DIR = ./AppDir

.PHONY: all clean build-linux build-mac build-appimage package-linux package-mac package-appimage

all: build-linux build-mac package-linux package-mac

clean:
	rm -rf bin obj $(OUTPUT_DIR)

build-linux:
	dotnet publish -c $(CONFIGURATION) -r $(RUNTIME_LINUX) --self-contained true /p:PublishSingleFile=true -o $(OUTPUT_DIR)/$(APP_NAME)-linux

build-appimage:
	dotnet publish -c $(CONFIGURATION) -r $(RUNTIME_LINUX) --self-contained true /p:PublishSingleFile=true -o $(APPIMAGE_DIR)/usr/bin

build-mac:
	dotnet publish -c $(CONFIGURATION) -r $(RUNTIME_MAC) --self-contained true /p:PublishSingleFile=true -o $(OUTPUT_DIR)/$(APP_NAME)-mac

package-linux:
	tar -czvf $(OUTPUT_DIR)/$(APP_NAME)-linux.tar.gz -C $(OUTPUT_DIR)/$(APP_NAME)-linux .

package-mac:
	tar -czvf $(OUTPUT_DIR)/$(APP_NAME)-mac.tar.gz -C $(OUTPUT_DIR)/$(APP_NAME)-mac .

package-appimage:
	appimage-builder
