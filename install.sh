#!/bin/bash

# Install s2pkg locally for current user (no sudo)
# Usage: ./install.sh ./dist/s2pkg-linux/s2pkg

set -e

SOURCE_BIN="$1"
INSTALL_DIR="$HOME/.local/bin"
TARGET="$INSTALL_DIR/s2pkg"

if [[ ! -x "$SOURCE_BIN" ]]; then
    echo "❌ Error: Provide a valid built s2pkg binary as the first argument."
    exit 1
fi

mkdir -p "$INSTALL_DIR"
cp "$SOURCE_BIN" "$TARGET"
chmod +x "$TARGET"

if [[ ":$PATH:" != *":$INSTALL_DIR:"* ]]; then
    echo "⚠️  $INSTALL_DIR is not in your PATH. Consider adding this to your shell profile:"
    echo "    export PATH=\"\$HOME/.local/bin:\$PATH\""
else
    echo "✅ Installed s2pkg to $TARGET"
    echo "Run 's2pkg --help' to get started."
fi
