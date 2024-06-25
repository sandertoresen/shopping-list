#!/bin/bash
API_URL="$1"

function update_file {
  local file="$1"
  awk -v url="$API_URL" '
  BEGIN { FS = ORS = "" }
  { gsub(/PlaceholderEnvVariableOverwrittenAtRuntimeURL/, url) 
  } 1' "$file" > "$file.bak" && mv "$file.bak" "$file"
}

for file in ./dist/assets/*.js; do
  update_file "$file"
done

# Start the application
exec npm run preview