# Cross-platform Makefile for Chaos project
# Works on Windows (cmd/powershell with make), Ubuntu, and WSL

# Detect OS
ifeq ($(OS),Windows_NT)
    DETECTED_OS := Windows
    RM_DIR := rmdir /s /q
    RM_FILE := del /f /q
    FIND_CMD := dir /s /b
    # Check if running in Git Bash/MSYS/Cygwin
    ifneq ($(MSYSTEM),)
        DETECTED_OS := Unix
        RM_DIR := rm -rf
        RM_FILE := rm -f
        FIND_CMD := find . -type
    endif
else
    DETECTED_OS := Unix
    RM_DIR := rm -rf
    RM_FILE := rm -f
    FIND_CMD := find . -type
endif

.PHONY: help clean clean-node clean-dotnet clean-all install reinstall build

help:
	@echo "Available targets:"
	@echo ""
	@echo "  Node.js:"
	@echo "    clean-node    - Remove all node_modules and lock files"
	@echo "    install       - Install npm dependencies (angular)"
	@echo "    reinstall     - Clean node and reinstall"
	@echo ""
	@echo "  .NET:"
	@echo "    clean-dotnet  - Remove bin/obj folders"
	@echo "    build         - Build .NET solution"
	@echo ""
	@echo "  All:"
	@echo "    clean         - Remove node_modules, lock files, bin, obj"
	@echo "    clean-all     - Clean + dist folders"
	@echo ""
	@echo "Detected OS: $(DETECTED_OS)"

# Node.js cleanup - removes all node_modules and lock files recursively
clean-node:
ifeq ($(DETECTED_OS),Windows)
	@echo Cleaning node_modules...
	@for /d /r %%d in (node_modules) do @if exist "%%d" $(RM_DIR) "%%d"
	@echo Cleaning lock files...
	@for /r %%f in (package-lock.json yarn.lock pnpm-lock.yaml) do @if exist "%%f" $(RM_FILE) "%%f"
	@echo Cleaning Angular cache...
	@if exist angular\.angular $(RM_DIR) angular\.angular
else
	@echo "Cleaning node_modules..."
	@find . -name "node_modules" -type d -prune -exec $(RM_DIR) {} + 2>/dev/null || true
	@echo "Cleaning lock files..."
	@find . -name "package-lock.json" -type f -delete 2>/dev/null || true
	@find . -name "yarn.lock" -type f -delete 2>/dev/null || true
	@find . -name "pnpm-lock.yaml" -type f -delete 2>/dev/null || true
	@echo "Cleaning Angular cache..."
	@$(RM_DIR) angular/.angular 2>/dev/null || true
endif
	@echo "Node cleanup complete"

# .NET cleanup - removes bin and obj folders
clean-dotnet:
ifeq ($(DETECTED_OS),Windows)
	@echo Cleaning bin folders...
	@for /d /r %%d in (bin) do @if exist "%%d" $(RM_DIR) "%%d"
	@echo Cleaning obj folders...
	@for /d /r %%d in (obj) do @if exist "%%d" $(RM_DIR) "%%d"
else
	@echo "Cleaning bin folders..."
	@find . -name "bin" -type d -prune -exec $(RM_DIR) {} + 2>/dev/null || true
	@echo "Cleaning obj folders..."
	@find . -name "obj" -type d -prune -exec $(RM_DIR) {} + 2>/dev/null || true
endif
	@echo ".NET cleanup complete"

# Combined cleanup
clean: clean-node clean-dotnet

# Full cleanup including dist
clean-all: clean
ifeq ($(DETECTED_OS),Windows)
	@if exist angular\dist $(RM_DIR) angular\dist
else
	@$(RM_DIR) angular/dist 2>/dev/null || true
endif
	@echo "Full cleanup complete"

# Install npm dependencies
install:
	@echo "Installing Angular dependencies..."
	cd angular && npm install
	@echo "Installing HttpApi.Host dependencies..."
	cd src/Chaos.HttpApi.Host && npm install

# Clean and reinstall
reinstall: clean-node install
	@echo "Dependencies reinstalled successfully"

# Build .NET solution
build:
	dotnet build Chaos.slnx
