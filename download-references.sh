#!/bin/bash

if ! command -v "depotdownloader" > /dev/null; then
  echo "error: missing depotdownloader binary"
  exit 1
fi

if [[ ! -d ".references" ]]; then
  mkdir .references
fi

cd .references || exit 1

function download_rust() {
  if [[ ! -d "RustDedicated_Data" ]]; then
    cat << EOF > filelist.txt
regex:RustDedicated_Data/Managed/.+\.dll
EOF

    echo "Downloading RustDedicated..."
    depotdownloader -app 258550 -branch staging -dir . -filelist filelist.txt
  else
    echo "Skipped RustDedicated download as RustDedicated_Data already exists."
  fi
}

download_rust
