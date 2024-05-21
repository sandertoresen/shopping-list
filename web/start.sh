#!/bin/bash
API_URL="$1"
API_PORT="$2"

function update_file {
  local file="$1"
  awk -v url="$API_URL" -v port="$API_PORT" '
  BEGIN { FS = ORS = "" }
  { gsub(/PlaceholderEnvVariableOverwrittenAtRuntimeURL/, url) 
    gsub(/PlaceholderEnvVariableOverwrittenAtRuntimePORT/, port) 
  } 1' "$file" > "$file.bak" && mv "$file.bak" "$file"
}

for file in ./dist/assets/*.js; do
  update_file "$file"
done

# Start the application
exec npm run preview